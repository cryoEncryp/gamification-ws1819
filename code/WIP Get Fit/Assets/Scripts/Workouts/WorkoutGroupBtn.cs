using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkoutGroupBtn : MonoBehaviour {

    public UnityEngine.UI.Text label;
    public UnityEngine.UI.Image icon;
    public int groupId;

    public WorkoutBtn wb0, wb1, wb2;

    private void Start () {
        WorkoutGroup wg = GameManager.instance.workoutGroups[groupId];
        label.text = wg.groupName;
        icon.sprite = wg.icon;
    }

    public void OnClick () {
        wb0.workoutId = groupId * 3;
        wb1.workoutId = (groupId * 3) + 1;
        wb2.workoutId = (groupId * 3) + 2;
        SVManager.instance.ChangeWorkoutSV (SVManager.instance.workoutSubs);
    }
}