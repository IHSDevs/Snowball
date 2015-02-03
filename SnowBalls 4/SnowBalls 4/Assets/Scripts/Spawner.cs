using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {
	
	public Transform defaultEnemy;
	public float timeDelay = 10f;
	
	public WaypointController myPath;
	
	// Use this for initialization
	void Start () {
		
		/*for (int i = 0; i < 10; i++) {
						Vector3 position = new Vector3 (Random.Range (-10, 10), 0.07116175f, Random.Range (-4, -20));
						Instantiate (enemy, position, Quaternion.identity);
				}*/
		InvokeRepeating ("SpawnDefault", 0, timeDelay);
		
	}
	
	void SpawnDefault ()
	{
		Vector3 position = new Vector3 (0, 0.07116175f, -20);
		//Instantiate (defaultEnemy, position, Quaternion.identity);// as Transform;
		Transform t = Instantiate (defaultEnemy, position, Quaternion.identity) as Transform;
		//print (t);
		GameObject objClone = t.gameObject;
		//print (objClone);
		myPath.addTraveller (objClone);
	}
	
	void SpawnEnemy(GameObject type, Vector3 position)
	{
		Instantiate (type, position, Quaternion.identity);
	}
}