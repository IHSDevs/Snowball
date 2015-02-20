using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

	public Vector3 defaultPos;
	public Transform defaultEnemy;
	public float timeDelay, spawnDistance;
	public int wave;

	public JulianPathing pathFinder;
	public Grid pathGrid;

	private Vector3[] myPath;
	private int pathLen;
	private RaycastHit hit;
	
	// Use this for initialization
	void Start () {

		InvokeRepeating ("SpawnDefault", 0, timeDelay);

		//myPath = pathFinder.GetPathFromPos(defaultPos);

	}

	//spawns the defaultEnemy
	void SpawnDefault ()
	{
		Vector3 spawnPos;
		bool validPos = false;
		do {

			float randomTheta = Random.Range (0, Mathf.PI/2);//0 to 90 degrees
			float xCoord, yCoord, zCoord;

			xCoord = spawnDistance * Mathf.Sin (randomTheta);
			zCoord = spawnDistance * Mathf.Cos (randomTheta);

			if (Physics.Raycast(new Vector3(xCoord, 10f, zCoord), Vector3.down, out hit)) {
				yCoord = 10 - hit.distance;
			}
			else {
				Debug.Log ("error: no collider present at spawn location");
				yCoord = 0;
			}

			//print ("Theta " + randomTheta + " xCoord " + xCoord + " yCoord " + yCoord + " zCoord " + zCoord);

			spawnPos = new Vector3(xCoord, yCoord, zCoord);

			validPos = pathGrid.NodeFromPos (spawnPos).Traversible;

		}
		while (!validPos);




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