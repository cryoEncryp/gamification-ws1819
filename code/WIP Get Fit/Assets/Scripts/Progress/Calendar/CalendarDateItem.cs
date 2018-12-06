using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;


public class CalendarDateItem : MonoBehaviour {

    public DateTime datetime;

    public void OnDateItemClick() {
        //CalendarController._calendarInstance.OnDateItemClick(gameObject.GetComponentInChildren<Text>().text);
    }

    private void Start() {
        CheckDate();
    }

    public void CheckDate() {
        datetime = CalendarController._calendarInstance.GetDateTimeFromItem(gameObject.GetComponentInChildren<Text>().text);

        foreach (KeyValuePair<DateTime, List<WorkoutSession>> entry in GameManager.instance.workoutHistory) {
            if (entry.Key == datetime) this.GetComponent<UnityEngine.UI.Image>().color = Color.red;
            else this.GetComponent<UnityEngine.UI.Image>().color = Color.white;
        }
    }


}
