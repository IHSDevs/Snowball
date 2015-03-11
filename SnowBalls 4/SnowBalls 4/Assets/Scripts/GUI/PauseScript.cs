using UnityEngine;
using System.Collections;

public class PauseScript : MonoBehaviour {
	bool isPaused = false;
	void Update(){
		if (Input.GetKeyDown (KeyCode.Escape)) {
			isPaused = !isPaused;
		}
		if (isPaused) {
			Time.timeScale = 0;
		} else {
			Time.timeScale = 1;
		}
	}

}
