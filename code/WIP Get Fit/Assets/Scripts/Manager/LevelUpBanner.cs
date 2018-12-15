using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpBanner : MonoBehaviour {
    public static LevelUpBanner instance;

    public GameObject banner;
    public UnityEngine.UI.Text levelLabel, unlockedLabel;
    public float fadeInOutTimer = 4.5f;
    public float onscreenY, offscreenY;

    void Awake() {
        if (instance == null) instance = this;
        else if (instance != this) Destroy(gameObject);
    }

    public void ShowBanner() {
        levelLabel.text = "[Level " + GameManager.instance.user.lvl + "]";
        unlockedLabel.text = LevelRewards.levelRewardStrings[GameManager.instance.user.lvl];
        StartCoroutine(TriggerBanner(fadeInOutTimer));
    }

    IEnumerator TriggerBanner(float time) {
        banner.transform.localPosition = new Vector3(banner.transform.localPosition.x, onscreenY, banner.transform.localPosition.z);
        yield return new WaitForSeconds(time);
        banner.transform.localPosition = new Vector3(banner.transform.localPosition.x, offscreenY, banner.transform.localPosition.z);
    }
}