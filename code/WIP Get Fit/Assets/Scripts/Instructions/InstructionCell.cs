using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionCell : MonoBehaviour {

    public UnityEngine.UI.Text header;
    public GameObject workout1, workout2, workout3;
    public int groupId;

    private void OnEnable() {
        header.text = GameManager.instance.workoutGroups[groupId].groupName;

        workout1.GetComponent<UnityEngine.UI.Text>().text = GameManager.instance.workouts[groupId * 3].title;
        workout2.GetComponent<UnityEngine.UI.Text>().text = GameManager.instance.workouts[groupId * 3 + 1].title;
        workout3.GetComponent<UnityEngine.UI.Text>().text = GameManager.instance.workouts[groupId * 3 + 2].title;

        workout1.GetComponent<InstructionSubcell>().workoutId = GameManager.instance.workouts[groupId * 3].workoutId;
        workout2.GetComponent<InstructionSubcell>().workoutId = GameManager.instance.workouts[groupId * 3 + 1].workoutId;
        workout3.GetComponent<InstructionSubcell>().workoutId = GameManager.instance.workouts[groupId * 3 + 2].workoutId;
        //workout1.text = GameManager.instance.workouts[groupId * 3].title;
        //workout2.text = GameManager.instance.workouts[groupId * 3 + 1].title;
        //workout3.text = GameManager.instance.workouts[groupId * 3 + 2].title;
    }
}
