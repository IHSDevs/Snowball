using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreManager : MonoBehaviour {
	public static int score;
	public static int wave = 1;
	Text text;

	// Use this for initialization
	void Start () {
		text = GetComponent <Text> ();
		score = 0;
	}
	
	// Update is called once per frame
	void Update () {
		wave = score / 10 + 1;
		text.text = "Wave: " + wave;
	}
}
