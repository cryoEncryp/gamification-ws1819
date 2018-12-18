using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionView : MonoBehaviour {

    public int workoutId;
    public UnityEngine.UI.Text header, instruction;
    public InstructionVideoPlayer vp;

    private void OnEnable() {
        instruction.text = "";
        int linecount = 1;
        header.text = GameManager.instance.workouts[workoutId].title;
        foreach (string s in GameManager.instance.workouts[workoutId].instructions) {
            instruction.text += "<b>" + linecount + ".</b> " + s + "\n";
            linecount++;
        }
        vp.StartVideo(workoutId);
    }
}
