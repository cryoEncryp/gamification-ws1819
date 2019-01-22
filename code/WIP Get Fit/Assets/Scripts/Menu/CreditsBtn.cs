using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsBtn : MonoBehaviour {

    public GameObject creditsMenu;


    public void OnClick() {
        creditsMenu.SetActive(true);
    }
}
