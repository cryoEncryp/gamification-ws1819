using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionVideoPlayer : MonoBehaviour {

    void OnEnable() {

    }

    void OnDisable() {
        this.gameObject.GetComponent<UnityEngine.Video.VideoPlayer>().targetTexture.Release();
    }

    public void StartVideo(int wid) {
        var videoPlayer = this.gameObject.GetComponent<UnityEngine.Video.VideoPlayer>();
        videoPlayer.clip = GameManager.instance.workouts[wid].video;
        // videoPlayer.loopPointReached += EndReached;
        videoPlayer.skipOnDrop = true;
        videoPlayer.isLooping = true;
        videoPlayer.Play();
    }
}