using System.Collections;
using System.Collections.Generic;
using PaperPlaneTools;
using UnityEngine;

public class SVManager : MonoBehaviour {

    public static SVManager instance = null;
    public GameObject svWorkouts, svProgress, svInstructions, workoutGroups, workoutSubs, workoutSetup, workoutView, dailyChallengeSetup, dailyChallengeView, instructionOverview, instructionView;

    private void Awake() {
        if (instance == null) instance = this;
        else if (instance != this) Destroy(gameObject);
    }

    public void ChangeSV(GameObject sv) {
        if (!GameManager.instance.isCurrentWorkoutActive) {
            svWorkouts.SetActive(false);
            workoutGroups.SetActive(true);
            workoutSubs.SetActive(false);
            workoutSetup.SetActive(false);
            workoutView.SetActive(false);
            dailyChallengeSetup.SetActive(false);
            dailyChallengeView.SetActive(false);
            svProgress.SetActive(false);
            svInstructions.SetActive(false);
            instructionOverview.SetActive(true);
            instructionView.SetActive(false);
            sv.SetActive(true);
            RefreshColor();
            var texts = GameObject.FindGameObjectsWithTag("SVSwitchText");
            foreach (var txt in texts) {
                txt.GetComponent<UnityEngine.UI.Text>().fontStyle = FontStyle.Italic;
                if (txt.name == (sv.name).Substring(0, sv.name.Length - 2)) {
                    txt.GetComponent<UnityEngine.UI.Text>().fontStyle = FontStyle.Bold;
                }
            }
        } else {
            WorkoutView.instance.TriggerWorkoutSession();
            new Alert("Warning", "You're in the middle of a workout.\nDo you still want to exit?")
                .SetPositiveButton("Yes", () => { WorkoutView.instance.ExitWorkout(); ChangeSV(sv); })
                .SetNegativeButton("No", () => { WorkoutView.instance.TriggerWorkoutSession(); })
                .Show();
        }

    }

    public void ChangeWorkoutSV(GameObject sv) {
        svWorkouts.SetActive(true);
        workoutGroups.SetActive(false);
        workoutSubs.SetActive(false);
        workoutSetup.SetActive(false);
        workoutView.SetActive(false);
        dailyChallengeSetup.SetActive(false);
        dailyChallengeView.SetActive(false);
        sv.SetActive(true);
        RefreshColor();
    }

    public void ChangeInstructionSV(GameObject sv) {
        svInstructions.SetActive(true);
        instructionOverview.SetActive(false);
        instructionView.SetActive(false);
        sv.SetActive(true);
        RefreshColor();
    }

    public void RefreshColor() {
        GameObject.FindGameObjectWithTag("Camera").GetComponent<Camera>().backgroundColor = GameManager.instance.bgCol;
        GameObject[] bgos = GameObject.FindGameObjectsWithTag("BGColor");
        foreach (GameObject go in bgos) {
            go.GetComponent<UnityEngine.UI.Image>().color = GameManager.instance.bgCol;
        }
        GameObject[] gos = GameObject.FindGameObjectsWithTag("Header");
        foreach (GameObject go in gos) {
            go.GetComponent<UnityEngine.UI.Image>().color = GameManager.instance.mainCol;
        }
    }
}