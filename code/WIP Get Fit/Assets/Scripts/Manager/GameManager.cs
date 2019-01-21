using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;

    public GameObject intro;

    //threshold float values - necessary workout in seconds per day to get low/med/high reward
    public float medThresh, highThresh;
    public Color lowReward, medReward, highReward, currentReward;
    public Color mainCol, bgCol;

    public List<WorkoutGroup> workoutGroups;
    public List<Workout> workouts;
    public User user = new User("m", 0, 0, 0);
    // User XP calculation factors
    public float expInc = 1f; // https://www.wolframalpha.com/input/?i=plot+floor(1+*+sqrt(x))+for+x+from+0+to+500
    public float inverseCalcFactor = 1f;
    public UnityEngine.UI.Slider lvlProgressBar;
    public UnityEngine.UI.Text lvlLabel;

    public WorkoutSession currentWorkoutSession;
    public bool isCurrentWorkoutActive = false;

    //stores all completed workouts
    public Dictionary<DateTime, List<WorkoutSession>> workoutHistory = new Dictionary<DateTime, List<WorkoutSession>>();
    public DailyChallenge todaysChallenge;

    public List<int> unlockedWorkouts = new List<int>();
    public List<int> unlockedColors = new List<int>();

    void OnApplicationPause() {
        SavePrefs();
    }

    void OnApplicationQuit() {
        SavePrefs();
    }

    void Awake() {
        // singleton instance
        if (instance == null) instance = this;
        else if (instance != this) Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
        LoadPrefs();
    }

    void Start() {
        GenerateDailyChallenge();
    }

    public void AddWorkoutSessionToHistory(WorkoutSession ws) {
        if (workoutHistory.ContainsKey(DateTime.Now.Date)) {
            workoutHistory[DateTime.Now.Date].Add(ws);
        } else {
            List<WorkoutSession> lws = new List<WorkoutSession>();
            lws.Add(ws);
            workoutHistory.Add(DateTime.Now.Date, lws);
        }
    }

    public int GetCurrentStreak() {
        int counter = 0;
        // if there's no workoutHistory for yesterday, return a streak of 0
        if (!workoutHistory.ContainsKey(DateTime.Now.Date.AddDays(-1)) && !workoutHistory.ContainsKey(DateTime.Now.Date)) {
            return 0;
        } else {
            for (int i = 0; i <= workoutHistory.Keys.Count; i++) {
                if (workoutHistory.ContainsKey(DateTime.Now.Date.AddDays(-1 * i))) {
                    counter++;
                } else {
                    if (i != 0) break;
                }
            }
        }
        return counter;
    }

    public double GetTotalCalories() {
        double result = 0;
        foreach (KeyValuePair<DateTime, List<WorkoutSession>> entry in workoutHistory) {
            foreach (var workoutSession in entry.Value) {
                result += workoutSession.kcal;
            }
        }
        return result;
    }

    public float GetTotalWorkoutSeconds() {
        float result = 0;
        foreach (KeyValuePair<DateTime, List<WorkoutSession>> entry in workoutHistory) {
            foreach (var workoutSession in entry.Value) {
                result += workoutSession.durationCompleted;
            }
        }
        return result;
    }

    public int GetTotalWorkoutMinutes() {
        return (int)(GetTotalWorkoutSeconds() / 60f);
    }

    public float GetTotalWorkoutSecondsForDate(DateTime date) {
        float result = 0f;
        List<WorkoutSession> lws = workoutHistory[date];
        foreach (WorkoutSession ws in lws) {
            result += ws.durationCompleted;
        }
        return result;
    }

    // saves all necessary userdata into a savestate [playerprefs]
    public void SavePrefs() {
        PlayerPrefs.SetInt("firststart", 0);
        PlayerPrefs.SetString("user_gender", user.gender);
        PlayerPrefs.SetInt("user_age", user.age);
        PlayerPrefs.SetInt("user_height", user.height);
        PlayerPrefs.SetInt("user_weight", user.weight);
        PlayerPrefs.SetInt("user_lvl", user.lvl);
        PlayerPrefs.SetInt("user_totalXP", user.totalXP);
        //serialize workoutHistory-dict into json string and save it into the PlayerPrefs
        var sWorkoutHistory = JsonConvert.SerializeObject(workoutHistory);
        PlayerPrefs.SetString("workoutHistory", sWorkoutHistory);

        PlayerPrefsX.SetColor("mainColor", mainCol);
        PlayerPrefsX.SetColor("bgColor", bgCol);

        PlayerPrefs.SetInt("notification_h", NotificationTimer.h);
        PlayerPrefs.SetInt("notification_m", NotificationTimer.m);
    }

    // restores all savestate data and replaces instance vars accordingly
    public void LoadPrefs() {
        if (PlayerPrefs.HasKey("firststart")) {
            intro.SetActive(false);
            user = new User(PlayerPrefs.GetString("user_gender"), PlayerPrefs.GetInt("user_age"),
                PlayerPrefs.GetInt("user_height"), PlayerPrefs.GetInt("user_weight"),
                PlayerPrefs.GetInt("user_lvl"), PlayerPrefs.GetInt("user_totalXP"));
            AddXP(0); //add 0 XP to restore progressbar position
            lvlLabel.text = "[Level " + user.lvl + "]";
            //deserialize json-string into workoutHistory-dict and restore it
            var dWorkoutHistory = JsonConvert.DeserializeObject<Dictionary<DateTime, List<WorkoutSession>>>(PlayerPrefs.GetString("workoutHistory"));
            workoutHistory = dWorkoutHistory;

            Color mainColor = PlayerPrefsX.GetColor("mainColor");
            Color bgColor = PlayerPrefsX.GetColor("bgColor");

            GameObject.FindGameObjectWithTag("Camera").GetComponent<Camera>().backgroundColor = bgColor;
            GameObject[] bgos = GameObject.FindGameObjectsWithTag("BGColor");
            foreach (GameObject go in bgos) {
                go.GetComponent<UnityEngine.UI.Image>().color = bgColor;
            }
            GameObject[] gos = GameObject.FindGameObjectsWithTag("Header");
            foreach (GameObject go in gos) {
                go.GetComponent<UnityEngine.UI.Image>().color = mainColor;
            }
            mainCol = mainColor; bgCol = bgColor;

            NotificationTimer.h = PlayerPrefs.GetInt("notification_h");
            NotificationTimer.m = PlayerPrefs.GetInt("notification_m");
            NotificationTimer.SetupLocalNotificationsOnReboot(NotificationTimer.h, NotificationTimer.m, 7);
            //unlock all currently unlocked rewards again
            LevelRewards.UnlockLevelRewards();
        } else {
            intro.SetActive(true);
        }
    }

    public void GenerateDailyChallenge() {
        int w0id, w1id, w2id;
        //inefficient, but simple
        do {
            w0id = UnityEngine.Random.Range(0, 21);
            w1id = UnityEngine.Random.Range(0, 21);
            w2id = UnityEngine.Random.Range(0, 21);
        } while ((w0id == w1id) || (w1id == w2id) || (w0id == w2id));
        DailyChallenge tmp = new DailyChallenge(new List<WorkoutSession>(new WorkoutSession[] { new WorkoutSession(w0id, 300), new WorkoutSession(w1id, 300), new WorkoutSession(w2id, 300) }));
        todaysChallenge = tmp;
    }

    public void AddXP(int exp) {
        user.totalXP += exp;
        int tempLVL = Mathf.FloorToInt(expInc * Mathf.Sqrt(user.totalXP));
        // LEVEL UP
        if (tempLVL > user.lvl) {
            // set new user level
            user.lvl = tempLVL;
            // unlock level specific rewards
            LevelRewards.UnlockLevelRewards();
            // label manipulation
            lvlLabel.text = "[Level " + user.lvl + "]";
            // show level up banner
            LevelUpBanner.instance.ShowBanner();

        }
        float expNext = (inverseCalcFactor * (user.lvl + 1) * (user.lvl + 1));
        float expLast = (inverseCalcFactor * user.lvl * user.lvl);
        float diff = expNext - user.totalXP;

        // progressbar manipulation
        lvlProgressBar.maxValue = expNext;
        lvlProgressBar.minValue = expLast;
        lvlProgressBar.value = (expNext - diff);
    }

    // Prototype
    public void TEST_ClearPrefs() {
        PlayerPrefs.DeleteAll();
        workoutHistory = new Dictionary<DateTime, List<WorkoutSession>>();
    }
}