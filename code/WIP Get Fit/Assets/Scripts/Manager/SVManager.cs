using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SVManager : MonoBehaviour {

    public static SVManager instance = null;
    public GameObject svWorkouts, svProgress, svInstructions, workoutGroups, workoutSubs, workoutSetup, workoutView;

    private void Awake() {
        if (instance == null) instance = this;
        else if (instance != this) Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    public void ChangeSV(GameObject sv){
        svWorkouts.SetActive(false);
        workoutGroups.SetActive(true); workoutSubs.SetActive(false); workoutSetup.SetActive(false); workoutView.SetActive(false);
        svProgress.SetActive(false);
        svInstructions.SetActive(false);
        sv.SetActive(true);
        var texts = GameObject.FindGameObjectsWithTag("SVSwitchText");
        foreach (var txt in texts){
            txt.GetComponent<UnityEngine.UI.Text>().fontStyle = FontStyle.Italic;
            if (txt.name == (sv.name).Substring(0, sv.name.Length - 2)){
                txt.GetComponent<UnityEngine.UI.Text>().fontStyle = FontStyle.Bold;
            }
        }
    }

    public void ChangeWorkoutSV(GameObject sv){
      svWorkouts.SetActive(true);
      workoutGroups.SetActive(false); workoutSubs.SetActive(false); workoutSetup.SetActive(false); workoutView.SetActive(false);
      sv.SetActive(true);
    }
}
