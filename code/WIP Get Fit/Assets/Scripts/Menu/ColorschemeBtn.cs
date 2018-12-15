using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorschemeBtn : MonoBehaviour {

    public int colorId;
    public Color mainColor, bgColor;

    private bool isUnlocked = false;
    private void OnEnable() {
        if (GameManager.instance.unlockedColors.IndexOf(colorId) != -1) {
            ChangeColor(this.GetComponent<UnityEngine.UI.Button>(), mainColor);
            this.GetComponentInChildren<UnityEngine.UI.Text>().text = "";
            isUnlocked = true;
        } else {
            ChangeColor(this.GetComponent<UnityEngine.UI.Button>(), Color.gray);
            this.GetComponentInChildren<UnityEngine.UI.Text>().text = "?";
            isUnlocked = false;
        }
    }

    public void OnClick() {
        if (isUnlocked) {
            GameObject.FindGameObjectWithTag("Camera").GetComponent<Camera>().backgroundColor = bgColor;
            GameObject[] bgos = GameObject.FindGameObjectsWithTag("BGColor");
            foreach (GameObject go in bgos) {
                go.GetComponent<UnityEngine.UI.Image>().color = bgColor;
            }
            GameObject[] gos = GameObject.FindGameObjectsWithTag("Header");
            foreach (GameObject go in gos) {
                go.GetComponent<UnityEngine.UI.Image>().color = mainColor;
            }

            GameManager.instance.mainCol = mainColor; GameManager.instance.bgCol = bgColor;
        }
    }

    private void ChangeColor(UnityEngine.UI.Button btn, Color color) {
        UnityEngine.UI.ColorBlock cb = btn.colors;
        cb.normalColor = color;
        btn.colors = cb;
    }

}
