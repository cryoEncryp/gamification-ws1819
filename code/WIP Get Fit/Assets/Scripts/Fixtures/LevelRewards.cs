using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelRewards : MonoBehaviour {

    public static void UnlockLevelRewards() {
        int lvl = GameManager.instance.user.lvl;
        if (lvl >= 0) {
            GameManager.instance.unlockedWorkouts.Add(18);
            GameManager.instance.unlockedColors.Add(0);
        }
        if (lvl >= 1) {
            GameManager.instance.unlockedWorkouts.Add(19);
            GameManager.instance.unlockedColors.Add(1);
        }
        if (lvl >= 2) {
            GameManager.instance.unlockedWorkouts.Add(20);
            GameManager.instance.unlockedColors.Add(2);
        }
        if (lvl >= 3) {
            GameManager.instance.unlockedColors.Add(3);
        }
        if (lvl >= 4) {
            GameManager.instance.unlockedColors.Add(4);
        }
        if (lvl >= 5) {
            GameManager.instance.unlockedColors.Add(5);
        }
        if (lvl >= 6) {
            GameManager.instance.unlockedColors.Add(6);
        }
        if (lvl >= 7) {
            GameManager.instance.unlockedColors.Add(7);
        }
        if (lvl >= 8) {
            GameManager.instance.unlockedColors.Add(8);
        }
        if (lvl >= 9) {
            GameManager.instance.unlockedColors.Add(9);
        }
        if (lvl >= 10) {

        }
        if (lvl >= 11) {

        }
        if (lvl >= 12) {

        }
        if (lvl >= 13) {

        }
        if (lvl >= 14) {

        }
        if (lvl >= 15) {

        }
        if (lvl >= 16) {

        }
        if (lvl >= 17) {

        }
        if (lvl >= 18) {

        }
        if (lvl >= 19) {

        }
        if (lvl >= 20) {

        }
        GameManager.instance.SavePrefs();
    }

    public static List<string> levelRewardStrings = new List<string> {
        "", // Level 0 rewards, aka none
        "2 neue Workouts, Ernährungstipps", //1
        "1 neues Workouts, Muskelaufbautipps", //2
        "", //3
        "", //4
        "", //5
        "", //6
        "", //7
        "", //8
        "", //9
        "Level 10 rewards", //10
        "1 neues Workout, 2 neue Designs", //11
        "", //12
        "", //13
        "", //14
        "", //15
        "", //16
        "", //17
        "", //18
        "", //19
        "", //20
    };

}