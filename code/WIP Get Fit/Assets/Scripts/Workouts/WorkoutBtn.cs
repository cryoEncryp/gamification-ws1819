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
    private void OnEnable() {
        workout = GameManager.instance.workouts[workoutId];
        label.text = workout.title;
        icon.sprite = workout.icon;
        UnityEngine.UI.Image btnBG = this.GetComponent<UnityEngine.UI.Button>().image;

        if (GameManager.instance.unlockedWorkouts.IndexOf(workoutId) != -1) {
            isUnlocked = true;
            btnBG.color = Color.white;
        } else {
            isUnlocked = false;
            btnBG.color = Color.gray;
            label.text += "\n[LOCKED]";
        }
    }

    public void OnClick() {
        if (isUnlocked) {
            GameManager.instance.currentWorkoutSession.workoutId = workoutId;
            SVManager.instance.ChangeWorkoutSV(SVManager.instance.workoutSetup);
        }
    }

}