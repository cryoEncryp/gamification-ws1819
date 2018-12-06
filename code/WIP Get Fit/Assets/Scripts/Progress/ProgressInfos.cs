using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressInfos : MonoBehaviour {
	public UnityEngine.UI.Text kcalLabel, minLabel, streakLabel;
	public int fsSmall = 30;
	public int fsBig = 50;

	void OnEnable() {
		kcalLabel.text = "<i><size="+fsBig+">"+GameManager.instance.GetTotalCalories().ToString("0.00")+"</size></i>\n<size="+fsSmall+">kcal</size>";
		minLabel.text = "<i><size="+fsBig+">"+GameManager.instance.GetTotalWorkoutMinutes()+"</size></i>\n<size="+fsSmall+">min</size>";
		streakLabel.text = "<i><size="+fsBig+">10</size></i>\n<size="+fsSmall+">days</size>";
	}


}
