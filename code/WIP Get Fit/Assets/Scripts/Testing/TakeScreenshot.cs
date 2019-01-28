#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TakeScreenshot : MonoBehaviour
{

	[MenuItem("Tools/Take Screenshot")]
	static public void OnTakeScreenshot()
	{

		ScreenCapture.CaptureScreenshot(EditorUtility.SaveFilePanel("Save Screenshot As", "", "", "png"));
	}

}
#endif
