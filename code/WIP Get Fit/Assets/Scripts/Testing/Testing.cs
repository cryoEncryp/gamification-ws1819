using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour {

	public void AddXP () {
		GameManager.instance.AddXP (1);
	}
}