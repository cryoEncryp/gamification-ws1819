using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkoutBtn : MonoBehaviour {

    public int baseId;
    public int workoutId;
    public UnityEngine.UI.Text label;
    public UnityEngine.UI.Image icon;
    public Workout workout;

    private void OnEnable() {
        workout = GameManager.instance.workouts[workoutId];
        label.text = workout.title;
        icon.sprite = workout.icon;
    }

    public void OnClick() {
        GameManager.instance.currentWorkoutSession.workout = workout;
        SVManager.instance.ChangeWorkoutSV(SVManager.instance.workoutSetup);
    }

}
