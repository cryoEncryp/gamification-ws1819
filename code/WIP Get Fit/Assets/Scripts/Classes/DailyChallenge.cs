using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DailyChallenge : MonoBehaviour {

	private List<WorkoutSession> challenges;

	public DailyChallenge (List<WorkoutSession> _challenges) {
		challenges = _challenges;
	}

	public List<WorkoutSession> GetWorkoutSessionList () {
		return challenges;
	}

	public float GetTotalDurationInSeconds () {
		float counter = 0f;
		foreach (WorkoutSession ws in challenges) {
			counter += ws.durationSetup;
		}
		return counter;
	}

	public int GetTotalDurationInMinutes () {
		return (int) (GetTotalDurationInSeconds () / 60f);
	}

	public void AddChallengeToWorkoutHistory () {
		foreach (WorkoutSession ws in challenges) {
			GameManager.instance.AddWorkoutSessionToHistory (ws);
		}
	}
}