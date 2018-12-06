using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserStoreMetrics : MonoBehaviour {

    public UnityEngine.UI.Dropdown gender;
    public UnityEngine.UI.InputField age, height, weight;

    public void SaveMetrics() {
        string genderStr;
        if (gender.value == 0) { genderStr = "m"; } else genderStr = "w";

        User user = new User(genderStr, int.Parse(age.text), int.Parse(height.text), int.Parse(weight.text));
        GameManager.instance.user = user;
        GameManager.instance.SavePrefs();
        GameManager.instance.intro.SetActive(false);
    }
}
