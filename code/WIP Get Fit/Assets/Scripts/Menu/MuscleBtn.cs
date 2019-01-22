using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuscleBtn : MonoBehaviour {
    public string url;
    private void OnEnable() {
        if (!GameManager.instance.hasUnlockedMuscleTips) {
            this.GetComponent<UnityEngine.UI.Text>().text = "[Gesperrt]";
        } else {
            this.GetComponent<UnityEngine.UI.Text>().text = "Muskelaufbau";
        }
    }

    public void OnClick() {
        if (GameManager.instance.hasUnlockedMuscleTips) {
            Application.OpenURL(url);
        } else {
            return;
        }
    }
}
