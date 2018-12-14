using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkoutBtn : MonoBehaviour {

    public int baseId;
    public int workoutId;
    public UnityEngine.UI.Text label;
    public UnityEngine.UI.Image icon;
    public Workout workout;

    private bool isUnlocked;
    private void OnEnable () {
        workout = GameManager.instance.workouts[workoutId];
        label.text = workout.title;
        icon.sprite = workout.icon;

        if (GameManager.instance.unlockedWorkouts.IndexOf (workoutId) != -1) {
            isUnlocked = true;
            icon.color = Color.white;
        } else {
            isUnlocked = false;
            icon.color = Color.gray;
            label.text += "\n[LOCKED]";
        }
    }

    public void OnClick () {
        if (isUnlocked) {
            GameManager.instance.currentWorkoutSession.workoutId = workoutId;
            SVManager.instance.ChangeWorkoutSV (SVManager.instance.workoutSetup);
        }
    }

}