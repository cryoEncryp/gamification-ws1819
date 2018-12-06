using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Globalization;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;

    public GameObject intro;

    public Color lowReward, medReward, highReward, currentReward;

    public UnityEngine.UI.Image test;
    public List<WorkoutGroup> workoutGroups;
    public List<Workout> workouts;
    public User user = new User("m", 0, 0, 0);

    public WorkoutSession currentWorkoutSession;
    public bool isCurrentWorkoutActive = false;

    //stores all completed workouts
    public Dictionary<DateTime, List<WorkoutSession>> workoutHistory = new Dictionary<DateTime, List<WorkoutSession>>();

    public void AddWorkoutSessionToHistory(WorkoutSession ws){
        if (workoutHistory.ContainsKey(DateTime.Now.Date)){
          workoutHistory[DateTime.Now.Date].Add(ws);
        } else {
          List<WorkoutSession> lws = new List<WorkoutSession>();
          lws.Add(ws);
          workoutHistory.Add(DateTime.Now.Date, lws);
        }
    }

    // saves all necessary userdata into a savestate [playerprefs]
    public void SavePrefs() {
        PlayerPrefs.SetInt("firststart", 0);
        PlayerPrefs.SetString("user_gender", user.gender);
        PlayerPrefs.SetInt("user_age", user.age);
        PlayerPrefs.SetInt("user_height", user.height);
        PlayerPrefs.SetInt("user_weight", user.weight);
    }

    // restores all savestate data and replaces instance vars accordingly
    public void LoadPrefs() {
        if (PlayerPrefs.HasKey("firststart")) {
            intro.SetActive(false);
            user = new User(PlayerPrefs.GetString("user_gender"), PlayerPrefs.GetInt("user_age"), PlayerPrefs.GetInt("user_height"), PlayerPrefs.GetInt("user_weight"));
        } else {
            intro.SetActive(true);
        }
    }

    // singleton instance
    void Awake() {
        if (instance == null) instance = this;
        else if (instance != this) Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
        LoadPrefs();
        print(user.age);
        workoutHistory.Add(DateTime.Parse("10/12/2018", new CultureInfo("de-DE", true)), new List<WorkoutSession>());
    }

    //TODO
    public int GetCurrentStreak(){

      return 0;
    }

    public double GetTotalCalories(){
      double result = 0;
      foreach(KeyValuePair<DateTime, List<WorkoutSession>> entry in workoutHistory){
        foreach (var workoutSession in entry.Value){
          result += workoutSession.kcal;
        }
      }
      return result;
    }

    public float GetTotalWorkoutSeconds(){
      float result = 0;
      foreach(KeyValuePair<DateTime, List<WorkoutSession>> entry in workoutHistory){
        foreach (var workoutSession in entry.Value){
          result += workoutSession.durationCompleted;
          print(workoutSession.durationCompleted);
        }
      }
      return result;
    }

    public int GetTotalWorkoutMinutes(){
      return (int) (GetTotalWorkoutSeconds()/60f);
    }
}
