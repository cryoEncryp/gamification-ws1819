using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DailyChallengeSetup : MonoBehaviour {
    public GameObject workoutInfo, challengeStartBtn;
    public float posX, posY;
    public float spacing = 100f;

    void OnEnable() {
        int counter = 0;

        CleanInfoClones();

        foreach (WorkoutSession ws in GameManager.instance.todaysChallenge.challenges) {
            //instantiate clone and setup its position
            GameObject workoutInfoClone = Instantiate(workoutInfo);
            workoutInfoClone.transform.SetParent(this.transform);
            workoutInfoClone.transform.localPosition = new Vector2(posX, posY - (counter * spacing));
            workoutInfoClone.tag = "workoutInfoClone";
            counter++;
            //change labels to match WorkoutSession data
            var labels = workoutInfoClone.GetComponentsInChildren<UnityEngine.UI.Text>();
            labels[0].text = counter + ".";
            labels[1].text = GameManager.instance.workouts[ws.workoutId].title;
            labels[2].text = (ws.durationSetup / 60f).ToString("0.00") + " min";
            labels[3].text = ws.GetBurnedCaloriesInSession(ws.durationSetup).ToString("0.00") + " kcal";

            var imgs = workoutInfoClone.GetComponentsInChildren<UnityEngine.UI.Image>();
            imgs[1].sprite = GameManager.instance.workouts[ws.workoutId].icon;

            //activate clone
            workoutInfoClone.SetActive(true);
        }
        challengeStartBtn.transform.localPosition = new Vector2(0f, posY - (counter * spacing));
    }

    public void StartDailyChallenge() {
        SVManager.instance.ChangeWorkoutSV(SVManager.instance.dailyChallengeView);
    }

    public void CleanInfoClones() {
        GameObject[] gos = GameObject.FindGameObjectsWithTag("workoutInfoClone");
        foreach (GameObject go in gos) {
            Destroy(go);
        }
    }
}