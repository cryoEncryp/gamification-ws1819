using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuBtn : MonoBehaviour {
    public GameObject menu, creditsMenu;

    public void Open() {
        menu.SetActive(true);
    }

    public void Close() {
        menu.SetActive(false);
        creditsMenu.SetActive(false);
    }
}
