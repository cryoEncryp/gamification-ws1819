using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DailyChallengeView : MonoBehaviour {

    public static DailyChallengeView instance = null;

    public UnityEngine.UI.Text workoutNumber, workoutTitle;

    public UnityEngine.UI.Text countdownLabel, kcalLabel;
    public UnityEngine.UI.Slider progressBar;
    public float durationInSec;
    public int displayMin, displaySec;
    public UnityEngine.UI.Image pauseBtn;
    public UnityEngine.Sprite sprPause, sprResume;
    private bool hasAddedWorkoutSessionToHistory = false;
    public float cooldownTimer;
    public Color cooldownColor;
    public WorkoutSession currentWS;
    public int workoutCounter;
    private bool isCooldown = false;
    public DailyChallengeVideoPlayer vp;

    void Awake() {
        if (instance == null) instance = this;
        else if (instance != this) Destroy(gameObject);
    }

    private void OnEnable() {
        workoutCounter = 0;
        GameManager.instance.currentWorkoutSession = GameManager.instance.todaysChallenge.challenges[workoutCounter];
        workoutNumber.text = (workoutCounter + 1) + " / " + GameManager.instance.todaysChallenge.challenges.Count;
        GameManager.instance.isCurrentWorkoutActive = true;
        isWorkoutPaused = false;
        workoutTitle.text = GameManager.instance.workouts[GameManager.instance.currentWorkoutSession.workoutId].title;
        hasAddedWorkoutSessionToHistory = false;
        durationInSec = GameManager.instance.currentWorkoutSession.durationSetup;
        progressBar.maxValue = durationInSec;
        progressBar.minValue = 0f;
        progressBar.fillRect.GetComponent<UnityEngine.UI.Image>().color = GameManager.instance.currentReward;


    }

    private void Update() {
        if (!isCooldown) {
            if (durationInSec <= 0f) GameManager.instance.isCurrentWorkoutActive = false;
            if (durationInSec > 0f && GameManager.instance.isCurrentWorkoutActive && !isWorkoutPaused) {
                durationInSec -= Time.deltaTime;
                progressBar.value = progressBar.maxValue - durationInSec;
                displayMin = ((int)durationInSec) / 60;
                displaySec = ((int)durationInSec) % 60;
                if (displayMin < 10 && displaySec >= 10) {
                    countdownLabel.text = "0" + displayMin + ":" + displaySec;
                } else if (displayMin >= 10 && displaySec < 10) {
                    countdownLabel.text = displayMin + ":" + "0" + displaySec;
                } else if (displayMin < 10 && displaySec < 10) {
                    countdownLabel.text = "0" + displayMin + ":" + "0" + displaySec;
                } else countdownLabel.text = displayMin + ":" + displaySec;
                kcalLabel.text = "<i><size=50>" + GameManager.instance.currentWorkoutSession.GetBurnedCaloriesInSession(GameManager.instance.currentWorkoutSession.durationSetup - durationInSec).ToString("0.00") +
                    "</size></i>\n<size=30>kcal verbrannt</size>";
            } else if (durationInSec <= 0f && !hasAddedWorkoutSessionToHistory) {
                FinishWorkout();
            }
        } else {
            workoutTitle.text = "Cooldown";
            progressBar.fillRect.GetComponent<UnityEngine.UI.Image>().color = cooldownColor;
            if (durationInSec <= 0f) StartNextWorkout();

            if (durationInSec > 0f) {
                durationInSec -= Time.deltaTime;
                progressBar.value = progressBar.maxValue - durationInSec;
                displayMin = ((int)durationInSec) / 60;
                displaySec = ((int)durationInSec) % 60;
                if (displayMin < 10 && displaySec >= 10) {
                    countdownLabel.text = "0" + displayMin + ":" + displaySec;
                } else if (displayMin >= 10 && displaySec < 10) {
                    countdownLabel.text = displayMin + ":" + "0" + displaySec;
                } else if (displayMin < 10 && displaySec < 10) {
                    countdownLabel.text = "0" + displayMin + ":" + "0" + displaySec;
                } else countdownLabel.text = displayMin + ":" + displaySec;
                kcalLabel.text = "<size=30>Nächstes Workout:</size>\n<i>" + GameManager.instance.workouts[GameManager.instance.todaysChallenge.challenges[workoutCounter].workoutId].title + "</i>";
            }
        }



    }


    public void StartNextWorkout() {
        isCooldown = false;
        vp.PlayVideo(GameManager.instance.workouts[GameManager.instance.todaysChallenge.challenges[workoutCounter].workoutId].video);
        GameManager.instance.currentWorkoutSession = GameManager.instance.todaysChallenge.challenges[workoutCounter];
        GameManager.instance.isCurrentWorkoutActive = true;
        workoutNumber.text = (workoutCounter + 1) + " / " + GameManager.instance.todaysChallenge.challenges.Count;
        isWorkoutPaused = false;
        workoutTitle.text = GameManager.instance.workouts[GameManager.instance.currentWorkoutSession.workoutId].title;
        hasAddedWorkoutSessionToHistory = false;
        durationInSec = GameManager.instance.currentWorkoutSession.durationSetup;
        progressBar.maxValue = durationInSec;
        progressBar.minValue = 0f;
        progressBar.fillRect.GetComponent<UnityEngine.UI.Image>().color = GameManager.instance.currentReward;

    }

    private bool isWorkoutPaused = false;
    public void TriggerWorkoutSession() {
        isWorkoutPaused = !isWorkoutPaused;
        if (isWorkoutPaused) {
            pauseBtn.sprite = sprResume;
        } else {
            pauseBtn.sprite = sprPause;
        }
    }

    public void ExitWorkout() {
        FinishWorkout(GameManager.instance.currentWorkoutSession.isFreeMode);
    }

    public void FinishWorkout(bool isFreeMode = false) {
        var cws = GameManager.instance.currentWorkoutSession;
        GameManager.instance.isCurrentWorkoutActive = false;
        countdownLabel.text = "<size=50>Workout " + (workoutCounter + 1) + " / " + GameManager.instance.todaysChallenge.challenges.Count + " beendet!</size>";

        cws.durationCompleted = cws.durationSetup - durationInSec;

        cws.CalculateCalories();
        //clone workoutSession
        WorkoutSession ws = new WorkoutSession();
        ws.workoutId = cws.workoutId;
        ws.kcal = cws.kcal;
        ws.durationSetup = cws.durationSetup;
        ws.durationCompleted = cws.durationCompleted;
        ws.isFreeMode = cws.isFreeMode;
        GameManager.instance.AddWorkoutSessionToHistory(ws);
        cws.Clear();
        hasAddedWorkoutSessionToHistory = true;
        // add 1 XP per 1 Minute of completed workout
        GameManager.instance.AddXP((int)(ws.durationCompleted / 60));


        workoutCounter++;
        if (workoutCounter < GameManager.instance.todaysChallenge.challenges.Count) {
            StartCoroutine(WorkoutCooldown(5f));
        } else {
            StartCoroutine(DailyChallengeComplete(2f));
        }



    }

    private IEnumerator WorkoutCooldown(float waitTime) {
        yield return new WaitForSeconds(2);
        countdownLabel.text = "<size=40>Cooldown für Workout " + (workoutCounter + 1) + " / " + GameManager.instance.todaysChallenge.challenges.Count + "...</size>";
        yield return new WaitForSeconds(3);

        durationInSec = cooldownTimer;
        progressBar.maxValue = durationInSec;
        progressBar.minValue = 0f;
        isCooldown = true;
        vp.ClearFrame();
    }

    private IEnumerator DailyChallengeComplete(float waitTime) {
        yield return new WaitForSeconds(waitTime);
        workoutNumber.text = "<i>Tages-Challenge erfolgreich beendet!</i>";
        GameManager.instance.isCurrentWorkoutActive = false;
        vp.ClearFrame();
    }

    //Prototype
    public void TEST_SkipWorkout() {
        durationInSec = 5f;
    }

}
