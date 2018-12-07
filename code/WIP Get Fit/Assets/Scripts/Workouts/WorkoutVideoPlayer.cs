using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkoutVideoPlayer : MonoBehaviour {
  // Use this for initialization
  void OnEnable () {
    var videoPlayer = this.gameObject.GetComponent<UnityEngine.Video.VideoPlayer> ();
    videoPlayer.clip = GameManager.instance.workouts[GameManager.instance.currentWorkoutSession.workoutId].vc;
  }

  void OnDisable () {
    this.gameObject.GetComponent<UnityEngine.Video.VideoPlayer> ().targetTexture.Release ();
  }
}