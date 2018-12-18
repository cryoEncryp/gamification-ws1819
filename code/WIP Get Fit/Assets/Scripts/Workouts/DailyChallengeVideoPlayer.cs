using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DailyChallengeVideoPlayer : MonoBehaviour {

    void OnEnable() {
        var videoPlayer = this.gameObject.GetComponent<UnityEngine.Video.VideoPlayer>();
        videoPlayer.Prepare();
        videoPlayer.clip = GameManager.instance.workouts[GameManager.instance.currentWorkoutSession.workoutId].video;
        // videoPlayer.loopPointReached += EndReached;
        videoPlayer.skipOnDrop = true;
        videoPlayer.isLooping = true;
        videoPlayer.Play();
    }

    void OnDisable() {
        this.gameObject.GetComponent<UnityEngine.Video.VideoPlayer>().targetTexture.Release();
    }

    public void PlayVideo(UnityEngine.Video.VideoClip vc) {
        var videoPlayer = this.gameObject.GetComponent<UnityEngine.Video.VideoPlayer>();
        videoPlayer.Prepare();
        videoPlayer.clip = vc;
        // videoPlayer.loopPointReached += EndReached;
        videoPlayer.skipOnDrop = true;
        videoPlayer.isLooping = true;
        videoPlayer.Play();
    }

    public void ClearFrame() {
        var videoPlayer = this.gameObject.GetComponent<UnityEngine.Video.VideoPlayer>();
        videoPlayer.Stop();
        videoPlayer.clip = null;
        videoPlayer.targetTexture.Release();
    }
}