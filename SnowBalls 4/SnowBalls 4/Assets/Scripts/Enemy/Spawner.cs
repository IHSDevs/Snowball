using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Spawner : MonoBehaviour {

	public Vector3 defaultPos;
	public Transform defaultEnemy;
	public float timeDelay, spawnDistance;
	
	public JulianPathing pathFinder;
	public Grid pathGrid;

	public GameObject scoreManager;

	private Vector3[] myPath;
	private int pathLen;
	private RaycastHit hit;
	private int wave;
	float secondsIntoGame=0;
	Text text;
	// Use this for initialization
	void Start () {

		InvokeRepeating ("SpawnManager", 0, timeDelay);
		//myPath = pathFinder.GetPathFromPos(defaultPos);

	}
	float scoreManagerWave = 1;
	void SpawnManager(){
		secondsIntoGame++;
		float wave = secondsIntoGame / 10;
		wave = Mathf.Floor (wave);
		wave = wave + 1;
		float intoWave = secondsIntoGame % 10;
		if (intoWave <5 && wave!=1) {//Break between waves
			Debug.Log ("Break");
		} else {//Activly Spawning Snowmen
			for(int i = 0; i<wave; i++)
				SpawnDefault();
		}
		if (scoreManagerWave <= wave) {
			scoreManagerWave++;
			scoreManager.SendMessage ("IncreaseWave");

		}
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