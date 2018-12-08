using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkoutVideoPlayer : MonoBehaviour {

  void OnEnable () {
    var videoPlayer = this.gameObject.GetComponent<UnityEngine.Video.VideoPlayer> ();
    videoPlayer.Prepare ();
    videoPlayer.clip = GameManager.instance.workouts[GameManager.instance.currentWorkoutSession.workoutId].video;
    // videoPlayer.loopPointReached += EndReached;
    videoPlayer.skipOnDrop = true;
    videoPlayer.isLooping = true;
    videoPlayer.Play ();
  }

  void OnDisable () {
    this.gameObject.GetComponent<UnityEngine.Video.VideoPlayer> ().targetTexture.Release ();
  }

  // void EndReached (UnityEngine.Video.VideoPlayer videoPlayer) {
  //   videoPlayer.clip = GameManager.instance.workouts[GameManager.instance.currentWorkoutSession.workoutId].video;
  //   videoPlayer.Prepare ();
  //   videoPlayer.Play ();
  // }
}