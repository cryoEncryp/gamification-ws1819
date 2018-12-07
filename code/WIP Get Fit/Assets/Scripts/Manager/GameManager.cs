using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;

    public GameObject intro;
    //prototype presentation
    public bool OVERRIDE_FIRSTSTART = false;

    //threshold float values - necessary workout in seconds per day to get low/med/high reward
    public float medThresh, highThresh;
    public Color lowReward, medReward, highReward, currentReward;

    public List<WorkoutGroup> workoutGroups;
    public List<Workout> workouts;
    public User user = new User ("m", 0, 0, 0);

    public WorkoutSession currentWorkoutSession;
    public bool isCurrentWorkoutActive = false;

    //stores all completed workouts
    public Dictionary<DateTime, List<WorkoutSession>> workoutHistory = new Dictionary<DateTime, List<WorkoutSession>> ();

    void OnApplicationPause () {
        SavePrefs ();
    }

    void OnApplicationQuit () {
        SavePrefs ();
    }

    void Awake () {
        // singleton instance
        if (instance == null) instance = this;
        else if (instance != this) Destroy (gameObject);
        DontDestroyOnLoad (gameObject);
        LoadPrefs ();
        // Prototype
        if (OVERRIDE_FIRSTSTART) intro.SetActive (true);
    }

    public void AddWorkoutSessionToHistory (WorkoutSession ws) {
        if (workoutHistory.ContainsKey (DateTime.Now.Date)) {
            workoutHistory[DateTime.Now.Date].Add (ws);
        } else {
            List<WorkoutSession> lws = new List<WorkoutSession> ();
            lws.Add (ws);
            workoutHistory.Add (DateTime.Now.Date, lws);
        }
    }

    public int GetCurrentStreak () {
        int counter = 0;
        // if there's no workoutHistory for yesterday, return a streak of 0
        if (!workoutHistory.ContainsKey (DateTime.Now.Date.AddDays (-1)) && !workoutHistory.ContainsKey (DateTime.Now.Date)) {
            return 0;
        } else {
            for (int i = 0; i <= workoutHistory.Keys.Count; i++) {
                if (workoutHistory.ContainsKey (DateTime.Now.Date.AddDays (-1 * i))) {
                    counter++;
                } else {
                    if (i != 0) break;
                }
            }
        }
        return counter;
    }

    public double GetTotalCalories () {
        double result = 0;
        foreach (KeyValuePair<DateTime, List<WorkoutSession>> entry in workoutHistory) {
            foreach (var workoutSession in entry.Value) {
                result += workoutSession.kcal;
            }
        }
        return result;
    }

    public float GetTotalWorkoutSeconds () {
        float result = 0;
        foreach (KeyValuePair<DateTime, List<WorkoutSession>> entry in workoutHistory) {
            foreach (var workoutSession in entry.Value) {
                result += workoutSession.durationCompleted;
            }
        }
        return result;
    }

    public int GetTotalWorkoutMinutes () {
        return (int) (GetTotalWorkoutSeconds () / 60f);
    }

    public float GetTotalWorkoutSecondsForDate (DateTime date) {
        float result = 0f;
        List<WorkoutSession> lws = workoutHistory[date];
        foreach (WorkoutSession ws in lws) {
            result += ws.durationCompleted;
        }
        return result;
    }

    // saves all necessary userdata into a savestate [playerprefs]
    public void SavePrefs () {
        PlayerPrefs.SetInt ("firststart", 0);
        PlayerPrefs.SetString ("user_gender", user.gender);
        PlayerPrefs.SetInt ("user_age", user.age);
        PlayerPrefs.SetInt ("user_height", user.height);
        PlayerPrefs.SetInt ("user_weight", user.weight);
        //serialize workoutHistory-dict into json string and save it into the PlayerPrefs
        var sWorkoutHistory = JsonConvert.SerializeObject (workoutHistory);
        PlayerPrefs.SetString ("workoutHistory", sWorkoutHistory);
    }

    // restores all savestate data and replaces instance vars accordingly
    public void LoadPrefs () {
        if (PlayerPrefs.HasKey ("firststart")) {
            intro.SetActive (false);
            user = new User (PlayerPrefs.GetString ("user_gender"), PlayerPrefs.GetInt ("user_age"), PlayerPrefs.GetInt ("user_height"), PlayerPrefs.GetInt ("user_weight"));
            //deserialize json-string into workoutHistory-dict and restore it
            var dWorkoutHistory = JsonConvert.DeserializeObject<Dictionary<DateTime, List<WorkoutSession>>> (PlayerPrefs.GetString ("workoutHistory"));
            workoutHistory = dWorkoutHistory;
        } else {
            intro.SetActive (true);
        }
    }
}