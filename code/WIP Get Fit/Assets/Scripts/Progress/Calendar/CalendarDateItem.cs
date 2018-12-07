using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CalendarDateItem : MonoBehaviour {

    public DateTime datetime;

    public void OnDateItemClick () {

    }

    private void OnEnable () {
        CheckDate ();
    }

    public void CheckDate () {
        Color cellColor = Color.white;
        datetime = CalendarController._calendarInstance.GetDateTimeFromItem (gameObject.GetComponentInChildren<Text> ().text);
        if (GameManager.instance.workoutHistory.ContainsKey (datetime)) {
            if (GameManager.instance.GetTotalWorkoutSecondsForDate (datetime) < GameManager.instance.medThresh) {
                cellColor = GameManager.instance.lowReward;
            } else if (GameManager.instance.GetTotalWorkoutSecondsForDate (datetime) >= GameManager.instance.medThresh && GameManager.instance.GetTotalWorkoutSecondsForDate (datetime) < GameManager.instance.highThresh) {
                cellColor = GameManager.instance.medReward;
            } else if (GameManager.instance.GetTotalWorkoutSecondsForDate (datetime) >= GameManager.instance.highThresh) {
                cellColor = GameManager.instance.highReward;
            }
        } else {
            cellColor = Color.white;
        }
        this.GetComponent<UnityEngine.UI.Image> ().color = cellColor;
    }

}