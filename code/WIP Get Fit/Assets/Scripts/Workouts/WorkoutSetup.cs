using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkoutSetup : MonoBehaviour {

    public UnityEngine.UI.Image icon;
    public UnityEngine.UI.Text title, minuteLabel;
    public UnityEngine.UI.Slider slider;

    private void OnEnable () {
        icon.sprite = GameManager.instance.workouts[GameManager.instance.currentWorkoutSession.workoutId].icon;
        title.text = GameManager.instance.workouts[GameManager.instance.currentWorkoutSession.workoutId].title;
    }

    public void OnSliderValueChange () {
        int val = (int) slider.value;
        if (val == 1) minuteLabel.text = val + " Minute";
        else minuteLabel.text = val + " Minutes";
        GameManager.instance.currentWorkoutSession.isFreeMode = false;
        if (val >= (int) slider.minValue && val <= (int) GameManager.instance.medThresh / 60f) {
            minuteLabel.color = GameManager.instance.lowReward;
            GameManager.instance.currentReward = minuteLabel.color;
        } else if (val > (int) GameManager.instance.medThresh / 60f && val <= (int) GameManager.instance.highThresh / 60f) {
            minuteLabel.color = GameManager.instance.medReward;
            GameManager.instance.currentReward = minuteLabel.color;
        } else if (val > (int) GameManager.instance.highThresh / 60f && val <= (int) slider.maxValue - 1) {
            minuteLabel.color = GameManager.instance.highReward;
            GameManager.instance.currentReward = minuteLabel.color;
        } else if (val == slider.maxValue) {
            minuteLabel.text = "Free Mode";
            GameManager.instance.currentReward = Color.black;
            minuteLabel.color = Color.black;
        }
        slider.handleRect.GetComponent<UnityEngine.UI.Image> ().color = GameManager.instance.currentReward;
    }

    public void StartWorkout () {
        if (slider.value == slider.maxValue) {
            GameManager.instance.currentWorkoutSession.isFreeMode = true;
        }
        GameManager.instance.currentWorkoutSession.durationSetup = slider.value * 60;
        SVManager.instance.ChangeWorkoutSV (SVManager.instance.workoutView);
    }
}