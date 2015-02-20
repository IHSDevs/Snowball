using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreManager : MonoBehaviour {
	public static int w = 0;
	public static float score;
	Text text;

	// Use this for initialization
	void Start () {
		text = GetComponent <Text> ();
	}
	void IncreaseWave(){
		w++;
		text.text = "Wave: " + w;
	}
}
