using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkoutSetup : MonoBehaviour {

    public UnityEngine.UI.Image icon;
    public UnityEngine.UI.Text title, minuteLabel;
    public UnityEngine.UI.Slider slider;

    private void OnEnable() {
        icon.sprite = GameManager.instance.currentWorkoutSession.workout.icon;
        title.text = GameManager.instance.currentWorkoutSession.workout.title;
    }

    public void OnSliderValueChange() {
        int val = (int)slider.value;
        if (val == 1) minuteLabel.text = val + " Minute";
        else minuteLabel.text = val + " Minuten";

        if (val >= 1 && val <= 5) {
            minuteLabel.color = GameManager.instance.lowReward;
            GameManager.instance.currentReward = minuteLabel.color;
        } else if (val >= 6 && val <= 10) {
            minuteLabel.color = GameManager.instance.medReward;
            GameManager.instance.currentReward = minuteLabel.color;
        } else if (val >= 11 && val <= 15) {
            minuteLabel.color = GameManager.instance.highReward;
            GameManager.instance.currentReward = minuteLabel.color;
        } else {
            minuteLabel.color = Color.black;
        }

        if (val == slider.maxValue) {
            minuteLabel.text = "Freier Modus";
        }
    }

    public void StartWorkout() {
        GameManager.instance.currentWorkoutSession.durationSetup = slider.value * 60;
        SVManager.instance.ChangeWorkoutSV(SVManager.instance.workoutView);
    }
}
