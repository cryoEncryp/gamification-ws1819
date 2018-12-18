using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelInfos : MonoBehaviour {

    public UnityEngine.UI.Text currentLevel, neededXP, unlockWithNextLevel;

    private void OnEnable() {
        currentLevel.text = "<b><i>Level " + GameManager.instance.user.lvl + "</i></b>";

        float expNext = (GameManager.instance.inverseCalcFactor * (GameManager.instance.user.lvl + 1) * (GameManager.instance.user.lvl + 1));
        float diff = expNext - GameManager.instance.user.totalXP;
        neededXP.text = "<b><i>" + diff + " XP</i></b>\n<size=30>bis Level " + (GameManager.instance.user.lvl + 1) + "</size>";

        unlockWithNextLevel.text = "<b><i>" + LevelRewards.levelRewardStrings[GameManager.instance.user.lvl + 1] + "</i></b>\n<size=30>werden durch Level " + (GameManager.instance.user.lvl + 1) + " freigeschaltet</size>";
    }
}
