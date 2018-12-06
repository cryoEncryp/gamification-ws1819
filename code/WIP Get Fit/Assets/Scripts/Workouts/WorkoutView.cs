using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkoutView : MonoBehaviour {
	public UnityEngine.UI.Text workoutTitle;
	public WorkoutVideoPlayer workoutVideoPlayer;
	public UnityEngine.UI.Text countdownLabel, kcalLabel;
	public UnityEngine.UI.Slider progressBar;
	public float durationInSec;
	public int displayMin, displaySec;

	private bool hasAddedWorkoutSessionToHistory = false;

	private void OnEnable() {
			GameManager.instance.isCurrentWorkoutActive = true;
			workoutTitle.text = GameManager.instance.currentWorkoutSession.workout.title;
			hasAddedWorkoutSessionToHistory = false;
			durationInSec = GameManager.instance.currentWorkoutSession.durationSetup;
			progressBar.maxValue = durationInSec;
			progressBar.minValue = 0f;
			progressBar.fillRect.GetComponent<UnityEngine.UI.Image>().color = GameManager.instance.currentReward;
	}

	private void Update() {
			if (durationInSec <= 0f) GameManager.instance.isCurrentWorkoutActive = false;
			if (durationInSec > 0f && GameManager.instance.isCurrentWorkoutActive){
				durationInSec -= Time.deltaTime;
				progressBar.value = progressBar.maxValue - durationInSec;
				displayMin = ( (int)durationInSec) / 60;
				displaySec = ((int)durationInSec) % 60;
				if (displayMin < 10 && displaySec >= 10) {
						countdownLabel.text = "0"+displayMin + ":" + displaySec;
				} else if (displayMin >= 10 && displaySec < 10){
						countdownLabel.text = displayMin + ":" + "0"+displaySec;
				} else if (displayMin < 10 && displaySec < 10) {
						countdownLabel.text = "0"+displayMin + ":" + "0"+displaySec;
				} else countdownLabel.text = displayMin + ":" + displaySec;
				kcalLabel.text = "<i><size=50>"+GameManager.instance.currentWorkoutSession.GetBurnedCaloriesInSession(GameManager.instance.currentWorkoutSession.durationSetup - durationInSec).ToString("0.00")
				+ "</size></i>\n<size=30>kcal burned</size>";
			} else if (!hasAddedWorkoutSessionToHistory){
				GameManager.instance.isCurrentWorkoutActive = false;
				countdownLabel.text = "<size=50>Workout completed!</size>";
				GameManager.instance.currentWorkoutSession.durationCompleted = GameManager.instance.currentWorkoutSession.durationSetup - durationInSec;
				GameManager.instance.currentWorkoutSession.CalculateCalories();
				GameManager.instance.AddWorkoutSessionToHistory(GameManager.instance.currentWorkoutSession);
				hasAddedWorkoutSessionToHistory = true;
			}
	}
}
