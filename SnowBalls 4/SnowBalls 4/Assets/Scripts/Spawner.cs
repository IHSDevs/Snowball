using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {
	
	public Transform defaultEnemy;
	public float timeDelay = 10f;
	
	public WaypointController waypointAggregator;

	Vector3[] myPath;
	int pathLen;
	
	// Use this for initialization
	void Start () {

		waypointAggregator.aggregateWaypoints ();

		InvokeRepeating ("SpawnDefault", 0, timeDelay);

		//stores the length of waypointAggregator's path
		pathLen = waypointAggregator.getPathLength ();

		//sets myPath's length to the length of waypointAggregator's path
		myPath = new Vector3[pathLen];

		//sets myPath to waypointAggregator's path
		myPath = waypointAggregator.getPath();
	}

	//spawns the defaultEnemy
	void SpawnDefault ()
	{
		//spawn position - should probably be moved to a public variable eventually
		Vector3 position = new Vector3 (0, 0.07116175f, -20);

		//instantiates a defaultEnemy named mobClone
		Transform mobClone = Instantiate (defaultEnemy, position, Quaternion.identity) as Transform;

		//gives cloned enemy a path
		mobClone.GetComponent<SnowmanController>().setPath (myPath, pathLen);

	}

	//spawns an enemy of specified type and at specified location
	void SpawnEnemy(GameObject type, Vector3 position)
	{
		//instantiates an enemy of type type at position position
		Transform mobClone = Instantiate (type, position, Quaternion.identity) as Transform;
	}
}