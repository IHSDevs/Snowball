using UnityEngine;
using System.Collections;

public class Spawn : MonoBehaviour {

	public GameObject enemy;
	public float timeDelay = 10f;
	public float distance = -15f;
	// Use this for initialization
	void Start () {
		/*for (int i = 0; i < 10; i++) {
						Vector3 position = new Vector3 (Random.Range (-10, 10), 0.07116175f, Random.Range (-4, -20));
						Instantiate (enemy, position, Quaternion.identity);
				}*/
		InvokeRepeating ("SpawnIn", timeDelay, timeDelay);
	
	}

	void SpawnIn ()
	{
		Vector3 position = new Vector3 (Random.Range (-10, 10), 0.07116175f, distance);
		Instantiate (enemy, position, Quaternion.identity);
	}
}
