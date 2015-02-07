using UnityEngine;
using System.Collections;


public class SnowmanController : MonoBehaviour {

	Quaternion upRotation;

	public float howClose;

	public GameObject spurt;
	public GameObject explosion;
	public int explosionLimit = 1;
	public float health = 100f;
	
	/**
	public float headShot = 100f;
	public float bodyShot = 50f;
	public float legShot = 25f;*/
	
	public Jump myJump;

	//my ultimate target location
	public Vector3 myGoal;
	
	//gibs used upon death
	public Transform GibNose;
	public Transform GibEye;
	public Transform GibButton;

	//hit count for individual segments
	private int headCount = 0;
	private int bodyCount = 0;
	private int legsCount = 0;

	//damage multiplier for body segments
	public float headMultiplier;
	public float bodyMultiplier;
	public float legsMultiplier;

	//transforms for body segments
	public Transform head;
	public Transform body;
	public Transform legs;

	//hitboxes of body segments
	public ProjectileCounter headCounter;
	public ProjectileCounter bodyCounter;
	public ProjectileCounter legsCounter;

	//the path to be followed
	Vector3[] myPath;
	int pathProgress = 0;

	//controls the spurt effect of snowman decapitation
	GameObject spurtCloneBody;
	GameObject spurtCloneLegs;

	//used to remove limbs
	bool alive = true;
	bool hasLegs = true;


	void Start () {
		//stores the upwards facing rotation
		upRotation = Quaternion.LookRotation (Vector3.up, Vector3.up);
	}
	
	void Update () {
		//updates hit count of each segment
		headCount = headCounter.getHits ();
		bodyCount = bodyCounter.getHits ();
		legsCount = legsCounter.getHits ();

		//increments pathProgress once waypoint is reached, and sets myJump's new waypoint

		if (reachedWaypoint () && pathProgress < myPath.Length - 1) {
			pathProgress ++;
			myJump.setWaypoint(myPath[pathProgress]);
		}

		//destroys legs if legs are still alive and damaged
		if (hasLegs && legsCount > 0) {
			hasLegs = false;
			destroyLegs();
		}

		//calculates health, might want to change later
		if ((headMultiplier * headCount + bodyMultiplier * bodyCount + legsMultiplier * legsCount) >= health) {
			health = 0;
		}

		//destroys head if it has a head and is alive
		if (alive && headCount > 0) {
			alive = false;
			destroyHead();
		}

		//destroys snowman once its health reaches zero
		if (health <= 0 && alive) {
			alive = false;
			StartCoroutine("destroySnowman");
		}

		if (!alive) {
			if (!spurtCloneBody) {
				if (Vector3.Distance (head.position, body.position) > 1f) {
					spurtCloneBody = Instantiate (spurt, transform.position + Vector3.up * 2, upRotation) as GameObject;
					spurtCloneBody.transform.parent = body;
				}
			}
			if (!spurtCloneLegs) {
				if (Vector3.Distance (body.position, legs.position) > 1.3f)
				{
					spurtCloneLegs = Instantiate (spurt, transform.position + Vector3.up * 1, upRotation) as GameObject;
					spurtCloneLegs.transform.parent = legs;
				}
			}
		}

		
	}
	
	//sets the health of snowman
	public void setHealth(float newHealth)
	{
		health = newHealth;
	}

	//destroys the entire snowman with cute effects
	IEnumerator destroySnowman()
	{
		Destroy (transform.GetComponent<Attack> ());
		ScoreManager.score += 1;
		myJump.setJumpMagnitude(0f);

		head.gameObject.AddComponent<Rigidbody> ();
		body.gameObject.AddComponent<Rigidbody> ();
		legs.gameObject.AddComponent<Rigidbody> ();

		body.rigidbody.freezeRotation = true;

		yield return new WaitForSeconds(2f);
		
		Destroy (spurtCloneBody);

		transform.GetChild (0).gameObject.SetActive (false);

		Instantiate (explosion, transform.position + Vector3.up*2, transform.rotation);

		Destroy (head.gameObject);
		Destroy (body.gameObject);
		Destroy (legs.gameObject);

		this.gameObject.SetActive (false);
	}

	//sets the path of the snowman
	public void setPath (Vector3[] path, int len){
		myPath = new Vector3[len];
		myPath = path;
	}

	//destroys the snowman's head using cute effects
	void destroyHead() {
		head.gameObject.SetActive(false);
		
		Instantiate(GibNose, transform.position + Vector3.up*2, transform.rotation);
		Instantiate(GibEye, transform.position + Vector3.up*2, transform.rotation);
		Instantiate(GibButton, transform.position + Vector3.up*2, transform.rotation);
		Instantiate(GibButton, transform.position + Vector3.up*2, transform.rotation);
		Instantiate(GibButton, transform.position + Vector3.up*2, transform.rotation);

		Instantiate (explosion, transform.position + Vector3.up*2, transform.rotation);

		spurtCloneBody = Instantiate (spurt, transform.position + Vector3.up*2, upRotation) as GameObject;
		spurtCloneBody.transform.parent = body;

		StartCoroutine("destroySnowman");
	}

	//destroys the snowman's legs using cute effects
	void destroyLegs() {
		legs.gameObject.SetActive(false);
		myJump.setHeight(.5f);
		myJump.setJumpMagnitude(3f);
		myJump.setJumpAngle(70f);
		
		Instantiate (explosion, transform.position + Vector3.up*.5f, transform.rotation);

		Instantiate(GibNose, transform.position, transform.rotation);
	}

	//returns whether or not waypoint has been reached
	bool reachedWaypoint() {
		if (Vector3.Distance (transform.position, myPath[pathProgress]) < 5) {
			return true;
		}
		return false;
	}
}
