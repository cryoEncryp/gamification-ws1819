using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelRewards : MonoBehaviour {

    public static void UnlockLevelRewards() {
        int lvl = GameManager.instance.user.lvl;
        if (lvl >= 0) {
            GameManager.instance.unlockedWorkouts.Add(2);
            GameManager.instance.unlockedColors.Add(0);
        }
        if (lvl >= 1) {
            GameManager.instance.hasUnlockedNotifications = true;
            GameManager.instance.unlockedWorkouts.Add(4);
            GameManager.instance.unlockedColors.Add(1);
        }
        if (lvl >= 2) {
            GameManager.instance.unlockedWorkouts.Add(5);
            GameManager.instance.unlockedWorkouts.Add(8);
            GameManager.instance.unlockedColors.Add(2);
        }
        if (lvl >= 3) {
            GameManager.instance.unlockedWorkouts.Add(12);
            GameManager.instance.hasUnlockedDailyChallenge = true;
            GameManager.instance.unlockedColors.Add(3);
        }
        if (lvl >= 4) {
            GameManager.instance.unlockedWorkouts.Add(13);
            GameManager.instance.unlockedColors.Add(4);
        }
        if (lvl >= 5) {
            GameManager.instance.hasUnlockedNutritionTips = true;
            GameManager.instance.unlockedWorkouts.Add(20);
            GameManager.instance.unlockedColors.Add(5);
        }
        if (lvl >= 6) {
            GameManager.instance.unlockedWorkouts.Add(21);
            GameManager.instance.unlockedColors.Add(6);
        }
        if (lvl >= 7) {
            GameManager.instance.unlockedWorkouts.Add(1);
            GameManager.instance.unlockedColors.Add(7);
        }
        if (lvl >= 8) {
            GameManager.instance.hasUnlockedMuscleTips = true;
            GameManager.instance.unlockedWorkouts.Add(9);
            GameManager.instance.unlockedColors.Add(8);
        }
        if (lvl >= 9) {
            GameManager.instance.unlockedWorkouts.Add(15);
            GameManager.instance.unlockedColors.Add(9);
        }
        if (lvl >= 10) {
            GameManager.instance.unlockedWorkouts.Add(16);
        }
        if (lvl >= 11) {
            GameManager.instance.unlockedWorkouts.Add(19);
        }
        if (lvl >= 12) {
            GameManager.instance.unlockedWorkouts.Add(3);
        }
        if (lvl >= 13) {
            GameManager.instance.unlockedWorkouts.Add(7);
        }
        if (lvl >= 14) {
            GameManager.instance.unlockedWorkouts.Add(6);
        }
        if (lvl >= 15) {
            GameManager.instance.unlockedWorkouts.Add(0);
        }
        if (lvl >= 16) {
            GameManager.instance.unlockedWorkouts.Add(18);
        }
        if (lvl >= 17) {
            GameManager.instance.unlockedWorkouts.Add(10);
        }
        if (lvl >= 18) {
            GameManager.instance.unlockedWorkouts.Add(11);
        }
        if (lvl >= 19) {
            GameManager.instance.unlockedWorkouts.Add(14);
        }
        if (lvl >= 20) {
            GameManager.instance.unlockedWorkouts.Add(17);
        }
        GameManager.instance.SavePrefs();
    }

    public static List<string> levelRewardStrings = new List<string> {
        "", // Level 0 rewards, aka none
        "Notifications, 1 neues Workout, 1 neues Farbschema ", //1
        "2 neue Workouts, 1 neues Farbschema", //2
        "Tages-Challenges, 1 neues Workout, 1 neues Farbschema", //3
        "1 neues Workout, 1 neues Farbschema", //4
        "Ernährungstipps, 1 neues Workout, 1 neues Farbschema", //5
        "1 neues Workout, 1 neues Farbschema", //6
        "1 neues Workout, 1 neues Farbschema", //7
        "Muskelaufbau-Tipps, 1 neues Workout, 1 neues Farbschema", //8
        "1 neues Workout, 1 neues Farbschema", //9
        "1 neues Workout", //10
        "1 neues Workout", //11
        "1 neues Workout", //12
        "1 neues Workout", //13
        "1 neues Workout", //14
        "1 neues Workout", //15
        "1 neues Workout", //16
        "1 neues Workout", //17
        "1 neues Workout", //18
        "1 neues Workout", //19
        "1 neues Workout", //20
    };

}