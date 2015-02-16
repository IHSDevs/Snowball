using UnityEngine;
using System.Collections;

public class MenuScript : MonoBehaviour {
	void OnGUI(){
		if (GUI.Button (new Rect (Screen.width / 10, Screen.height*8 / 10, Screen.width*8 / 10, Screen.height / 10), "Play")) {
			Application.LoadLevel (2);
		}
	}
}
