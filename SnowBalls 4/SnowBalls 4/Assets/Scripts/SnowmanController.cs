using UnityEngine;
using System.Collections;


public class SnowmanController : MonoBehaviour {
	
	public GameObject explosion;
	public int explosionLimit = 1;
	public float health = 100f;

	/**
	public float headShot = 100f;
	public float bodyShot = 50f;
	public float legShot = 25f;*/

	public Jump myJump;

	private int headCount = 0;
	private int bodyCount = 0;
	private int legsCount = 0;

	public float headMultiplier;
	public float bodyMultiplier;
	public float legsMultiplier;

	public Transform head;
	public Transform body;
	public Transform legs;
	
	public ProjectileCounter headCounter;
	public ProjectileCounter bodyCounter;
	public ProjectileCounter legsCounter;

	bool hasLegs = true;

	// Update is called once per frame
	void Update () {

		if (hasLegs && legsCount > 0) {
			//Destroy (legs.gameObject);
			legs.gameObject.SetActive(false);
			myJump.setHeight(.5f);
			myJump.setJumpMagnitude(1.5f);
			myJump.setJumpAngle(0f);
			//head.position -= new Vector3 (0,1,0);
			//body.position -= new Vector3 (0,1,0);

			Instantiate (explosion, transform.position + Vector3.up*.5f, transform.rotation);

			hasLegs = false;
		}


		headCount = headCounter.getHits ();
		bodyCount = bodyCounter.getHits ();
		legsCount = legsCounter.getHits ();

		if ((headMultiplier * headCount + bodyMultiplier * bodyCount + legsMultiplier * legsCount) >= health) {
			health = 0;
		}



		//int children = 0;



		//explode snowman if more than 2 snowballs
		if (health <= 0) {
			//print ("yes");
			Vector3 height = new Vector3(0, 2, 0);
			Instantiate (explosion, transform.position + height, transform.rotation);
			Destroy (gameObject);
			ScoreManager.score += 1;
			//snowmanNew.transform.position = gameObject

		}
	}
}
