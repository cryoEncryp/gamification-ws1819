using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelRewards : MonoBehaviour {

	public static void UnlockLevelRewards () {
		int lvl = GameManager.instance.user.lvl;

		if (lvl >= 1) {
			GameManager.instance.unlockedWorkouts.Add (19);
		}
		if (lvl >= 2) {
			GameManager.instance.unlockedWorkouts.Add (20);
		}
		if (lvl >= 3) {

		}
		if (lvl >= 4) {

		}
		if (lvl >= 5) {

		}
		if (lvl >= 6) {

		}
		if (lvl >= 7) {

		}
		if (lvl >= 8) {

		}
		if (lvl >= 9) {

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
		GameManager.instance.SavePrefs ();
	}
}