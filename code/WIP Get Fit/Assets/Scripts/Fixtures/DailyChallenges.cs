using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DailyChallenges : MonoBehaviour {

	void Start () {
		DailyChallenge d1 = new DailyChallenge (new List<WorkoutSession> (new WorkoutSession[] { new WorkoutSession (18, 300), new WorkoutSession (19, 360), new WorkoutSession (20, 420) }));
		GameManager.instance.dailyChallenges.Add (d1);
		// Prototype
		GameManager.instance.todaysChallenge = d1;
	}
}