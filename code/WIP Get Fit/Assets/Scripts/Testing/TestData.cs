using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class TestData : MonoBehaviour {
	public bool OVERRIDE_FIRSTSTART = false, INJECT_TESTDATA = false;

	void Start () {
		if (OVERRIDE_FIRSTSTART) GameManager.instance.intro.SetActive (true);
		if (INJECT_TESTDATA) {
			GameManager.instance.workoutHistory.Add (DateTime.Parse ("10/12/2018", new CultureInfo ("de-DE", true)), new List<WorkoutSession> (new WorkoutSession[] { new WorkoutSession (21, 360f, 360f, 75) }));

			GameManager.instance.workoutHistory.Add (DateTime.Parse ("9/12/2018", new CultureInfo ("de-DE", true)), new List<WorkoutSession> (new WorkoutSession[] { new WorkoutSession (18, 750f, 750f, 225) }));

			GameManager.instance.workoutHistory.Add (DateTime.Parse ("8/12/2018", new CultureInfo ("de-DE", true)), new List<WorkoutSession> (new WorkoutSession[] { new WorkoutSession (19, 400f, 400f, 125) }));

			GameManager.instance.workoutHistory.Add (DateTime.Parse ("6/12/2018", new CultureInfo ("de-DE", true)), new List<WorkoutSession> (new WorkoutSession[] { new WorkoutSession (18, 100f, 100f, 55) }));
		}
	}
}