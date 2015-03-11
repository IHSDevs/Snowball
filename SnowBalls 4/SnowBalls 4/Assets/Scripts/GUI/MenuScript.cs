using UnityEngine;
using System.Collections;

public class MenuScript : MonoBehaviour {
	void OnGUI(){
		//Highscores Button
		if (GUI.Button (new Rect (Screen.width / 10, Screen.height*6.5f / 10, Screen.width*8 / 10, Screen.height / 10), "Highscores")) {

		}
		//Play Button
		if (GUI.Button (new Rect (Screen.width / 10, Screen.height*8 / 10, Screen.width*8 / 10, Screen.height / 10), "Play")) {
			Application.LoadLevel (2);
		}


	}
}
