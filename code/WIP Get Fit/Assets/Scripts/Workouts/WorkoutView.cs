using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkoutView : MonoBehaviour {

    public static WorkoutView instance = null;
    public UnityEngine.UI.Text workoutTitle;
    public WorkoutVideoPlayer workoutVideoPlayer;
    public UnityEngine.UI.Text countdownLabel, kcalLabel;
    public UnityEngine.UI.Slider progressBar;
    public float durationInSec;
    public int displayMin, displaySec;
    public UnityEngine.UI.Image pauseBtn;
    public UnityEngine.Sprite sprPause, sprResume;
    private bool hasAddedWorkoutSessionToHistory = false;

    void Awake() {
        if (instance == null) instance = this;
        else if (instance != this) Destroy(gameObject);
    }
    private void OnEnable() {
        GameManager.instance.isCurrentWorkoutActive = true;
        isWorkoutPaused = false;
        workoutTitle.text = GameManager.instance.workouts[GameManager.instance.currentWorkoutSession.workoutId].title;
        hasAddedWorkoutSessionToHistory = false;
        durationInSec = GameManager.instance.currentWorkoutSession.durationSetup;
        progressBar.maxValue = durationInSec;
        progressBar.minValue = 0f;
        progressBar.fillRect.GetComponent<UnityEngine.UI.Image>().color = GameManager.instance.currentReward;

        //switch to infinite mode if durationSetup is over the current limit
        if (GameManager.instance.currentWorkoutSession.isFreeMode) {
            durationInSec = 0f;
            progressBar.value = progressBar.maxValue;
        }
    }

    private void Update() {
        if (!GameManager.instance.currentWorkoutSession.isFreeMode) {
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
            if (GameManager.instance.isCurrentWorkoutActive && !isWorkoutPaused) {
                durationInSec += Time.deltaTime;
                displayMin = ((int)durationInSec) / 60;
                displaySec = ((int)durationInSec) % 60;
                if (displayMin < 10 && displaySec >= 10) {
                    countdownLabel.text = "0" + displayMin + ":" + displaySec;
                } else if (displayMin >= 10 && displaySec < 10) {
                    countdownLabel.text = displayMin + ":" + "0" + displaySec;
                } else if (displayMin < 10 && displaySec < 10) {
                    countdownLabel.text = "0" + displayMin + ":" + "0" + displaySec;
                } else countdownLabel.text = displayMin + ":" + displaySec;
                kcalLabel.text = "<i><size=50>" + GameManager.instance.currentWorkoutSession.GetBurnedCaloriesInSession(durationInSec).ToString("0.00") +
                    "</size></i>\n<size=30>kcal verbrannt</size>";
            } else if (!GameManager.instance.isCurrentWorkoutActive && !hasAddedWorkoutSessionToHistory) {
                FinishWorkout(true);
            }

        }

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
        countdownLabel.text = "<size=50>Workout beendet!</size>";
        if (!isFreeMode) {
            cws.durationCompleted = cws.durationSetup - durationInSec;
        } else {
            cws.durationCompleted = durationInSec;
        }

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
    }

    //Prototype
    public void TEST_SkipWorkout() {
        durationInSec = 5f;
    }
}