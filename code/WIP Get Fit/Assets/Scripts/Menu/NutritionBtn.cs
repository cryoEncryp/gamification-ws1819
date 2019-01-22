using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NutritionBtn : MonoBehaviour {
    public string url;
    private void OnEnable() {
        if (!GameManager.instance.hasUnlockedNutritionTips) {
            this.GetComponent<UnityEngine.UI.Text>().text = "[Gesperrt]";
        } else {
            this.GetComponent<UnityEngine.UI.Text>().text = "Ernährung";
        }
    }

    public void OnClick() {
        if (GameManager.instance.hasUnlockedNutritionTips) {
            Application.OpenURL(url);
        } else {
            return;
        }
    }
}
