using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionSubcell : MonoBehaviour {

    public int workoutId;
    public InstructionView iv;

    public void OnClick() {
        iv.workoutId = workoutId;
        SVManager.instance.ChangeInstructionSV(SVManager.instance.instructionView);
    }

}
