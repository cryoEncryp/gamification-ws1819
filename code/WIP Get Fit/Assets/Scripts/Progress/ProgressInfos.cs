using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressInfos : MonoBehaviour {
	public UnityEngine.UI.Text kcalLabel, minLabel, streakLabel;
	public int fsSmall = 30;
	public int fsBig = 50;

	void OnEnable () {
		kcalLabel.text = "<i><size=" + fsSmall + ">Burned</size></i>\n<size=" + fsBig + ">" + GameManager.instance.GetTotalCalories ().ToString ("0.00") + "</size>\n<i><size=" + fsSmall + ">kcal</size></i>";
		minLabel.text = "<i><size=" + fsSmall + ">Duration</size></i>\n<size=" + fsBig + ">" + (GameManager.instance.GetTotalWorkoutSeconds () / 60f).ToString ("0.00") + "</size>\n<i><size=" + fsSmall + ">min</size></i>";
		streakLabel.text = "<i><size=" + fsSmall + ">Streak</size></i>\n<size=" + fsBig + ">" + GameManager.instance.GetCurrentStreak () + "</size>\n<i><size=" + fsSmall + ">days</size></i>";
	}

}