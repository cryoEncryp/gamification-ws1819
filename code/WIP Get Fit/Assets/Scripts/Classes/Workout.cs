using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

[System.Serializable]
public class Workout {
    // title - e.g. "Liegestütze"; group - e.g. "Arme"; videoFile - e.g. "../Videos/Liegestütze.mp4"
    // icon is a reference sprite to use throughout the app
    public string title, group;
    public int workoutId;
    public Sprite icon;
    public VideoClip video;

    // used for calories calculation
    // based on http://fitnowtraining.com/2012/01/formula-for-calories-burned/
    public int assumedBPM;

    public string[] instructions;
}