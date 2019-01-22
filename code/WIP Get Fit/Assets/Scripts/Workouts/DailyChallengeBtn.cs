using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DailyChallengeBtn : MonoBehaviour {
    public UnityEngine.UI.Text label;
    public UnityEngine.UI.Image icon;
    public int groupId;

    private void Start() {
        WorkoutGroup wg = GameManager.instance.workoutGroups[groupId];
        label.text = wg.groupName;
        icon.sprite = wg.icon;

    }

    private void OnEnable() {
        if (!GameManager.instance.hasUnlockedDailyChallenge) {
            icon.sprite = GameManager.instance.sprLock;
        } else {
            icon.sprite = GameManager.instance.workoutGroups[groupId].icon;
        }
    }
    public void OnClick() {
        if (GameManager.instance.hasUnlockedDailyChallenge) {
            SVManager.instance.ChangeWorkoutSV(SVManager.instance.dailyChallengeSetup);
            GameManager.instance.GenerateDailyChallenge();
        }
    }
}