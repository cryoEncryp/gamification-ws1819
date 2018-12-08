using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DailyChallengeBtn : MonoBehaviour {
	public UnityEngine.UI.Text label;
	public UnityEngine.UI.Image icon;
	public int groupId;

	private void Start () {
		WorkoutGroup wg = GameManager.instance.workoutGroups[groupId];
		label.text = wg.groupName;
		icon.sprite = wg.icon;
	}
	public void OnClick () {
		//pick (semi-)random dailyChallenge here in the future?
		SVManager.instance.ChangeWorkoutSV (SVManager.instance.dailyChallengeSetup);
	}
}