using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class WorkoutSession {
    public Workout workout;
    //burned calories
    public double kcal = 0f;
    //duration in seconds; setup = duration that user selected, completed = duration that user actually completed
    public float durationSetup, durationCompleted = 0f;

    public void CalculateCalories() {
      //formula based on http://www.calories-calculator.net/Calculator_Formulars.html
      if (GameManager.instance.user.gender == "m") {
        kcal = ((GameManager.instance.user.age * 0.2017)
        + (GameManager.instance.user.weight * 0.1988)
        + (workout.assumedBPM * 0.6309)
        - 55.0969)
        * ((durationCompleted / 60)
        / 4.184);
      } else {
        kcal = ((GameManager.instance.user.age * 0.074)
        + (GameManager.instance.user.weight * 0.1263)
        + (workout.assumedBPM * 0.4472)
        - 20.4022)
        * ((durationCompleted / 60)
        / 4.184);
      }
    }

    public double GetBurnedCaloriesInSession(float time) {
      //formula based on http://www.calories-calculator.net/Calculator_Formulars.html
      double result = 0;
      if (GameManager.instance.user.gender == "m") {
        result = ((GameManager.instance.user.age * 0.2017)
        + (GameManager.instance.user.weight * 0.1988)
        + (workout.assumedBPM * 0.6309)
        - 55.0969)
        * ((time / 60)
        / 4.184);
      } else {
        result = ((GameManager.instance.user.age * 0.074)
        + (GameManager.instance.user.weight * 0.1263)
        + (workout.assumedBPM * 0.4472)
        - 20.4022)
        * ((time / 60)
        / 4.184);
      }
      return result;
    }
}
