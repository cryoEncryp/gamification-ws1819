using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkoutVideoPlayer : MonoBehaviour {
	// Use this for initialization
  void OnEnable () {
    var videoPlayer = this.gameObject.GetComponent<UnityEngine.Video.VideoPlayer>();
    videoPlayer.clip = GameManager.instance.currentWorkoutSession.workout.vc;
  }
}
