using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

	public Vector3 defaultPos;
	public Transform defaultEnemy;
	public float timeDelay;
	public int wave;

	public JulianPathing pathFinder;

	Vector3[] myPath;
	int pathLen;
	
	// Use this for initialization
	void Start () {

		pathFinder = GetComponent<JulianPathing>();

		InvokeRepeating ("SpawnDefault", 0, timeDelay);

		myPath = pathFinder.GetPathFromPos(defaultPos);
	}

	//spawns the defaultEnemy
	void SpawnDefault ()
	{
		Vector3 spawnPos = new Vector3(defaultPos.x + Random.Range(-10,10), defaultPos.y, defaultPos.z + Random.Range(-10,10));

		//instantiates a defaultEnemy named mobClone
		Transform mobClone = Instantiate (defaultEnemy, spawnPos, Quaternion.identity) as Transform;

		myPath = pathFinder.GetPathFromPos(spawnPos); 

		//gives cloned enemy a path
		mobClone.GetComponent<SnowmanController>().setPath (myPath);

	}

	//spawns an enemy of specified type and at specified location
	void SpawnEnemy(GameObject type, Vector3 position)
	{
		//instantiates an enemy of type type at position position
		Transform mobClone = Instantiate (type, position, Quaternion.identity) as Transform;
	}
}