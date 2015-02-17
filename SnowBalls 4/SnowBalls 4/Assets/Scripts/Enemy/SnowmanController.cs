using UnityEngine;
using System.Collections;


public class SnowmanController : MonoBehaviour {

	public GameObject spurt, explosion;
	
	public Jump jump;

	//my ultimate target location
	public Vector3 myGoal;
	
	//gibs used upon death
	public Transform GibNose, GibEye, GibButton;

	//hit count for individual segments
	private int headCount, bodyCount, legsCount, explosionLimit;

	//damage multiplier for body segments
	public float headMultiplier, bodyMultiplier, legsMultiplier, health, howClose;

	//transforms for body segments
	public Transform head, body, legs;

	//hitboxes of body segments
	public ProjectileCounter headCounter, bodyCounter, legsCounter;

	//the path to be followed
	Vector3[] myPath;
	int pathProgress = 0;

	//controls the spurt effect of snowman decapitation
	GameObject spurtCloneBody, spurtCloneLegs;

	//used to remove limbs
	bool alive, hasLegs, hasHead, jumping;

	//list of animations
	Animation[] animationList;

	//upwards rotaiton
	Quaternion upRotation;


	void Start () {
		jumping = false;

		health = 100f;

		headCount = 0;
		bodyCount = 0;
		legsCount = 0;

		alive = true;
		hasLegs = true;
		hasHead = true;

		animationList = GetComponents <Animation> ();

		//stores the upwards facing rotation
		upRotation = Quaternion.LookRotation (Vector3.up, Vector3.up);
	}

	//This class is a disaster. Needs reorganization
	void Update () {

		if (alive && jump.Grounded && !jumping) {
			StartCoroutine ("JumpAnimationSequencer");
		}




















		//updates hit count of each segment
		headCount = headCounter.getHits ();
		bodyCount = bodyCounter.getHits ();
		legsCount = legsCounter.getHits ();

		//increments pathProgress once waypoint is reached, and sets jump's new waypoint

		if (reachedWaypoint () && pathProgress < myPath.Length - 1) {
			pathProgress ++;
			jump.setWaypoint(myPath[pathProgress]);
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
		if (hasHead && headCount > 0) {
			hasHead = false;
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
				if (Vector3.Distance (head.position, body.position) > .9f) {
					spurtCloneBody = Instantiate (spurt, transform.position + Vector3.up * 1.6f, upRotation) as GameObject;
					spurtCloneBody.transform.parent = body;
				}
			}
			if (!spurtCloneLegs) {
				if (Vector3.Distance (body.position, legs.position) > 1.2f)
				{
					spurtCloneLegs = Instantiate (spurt, transform.position + Vector3.up * .8f, upRotation) as GameObject;
					spurtCloneLegs.transform.parent = legs;
				}
			}
		}
	}

	//handles all the animation stuff involved with jumping
	IEnumerator JumpAnimationSequencer()
	{
		jumping = true;

		animationList[0].Play("SnowManJump1");

		yield return new WaitForSeconds(.2f);

		jump.Launch();

		yield return new WaitForSeconds(.4f);

		animationList[0].Stop("SnowManJump1");

		if (jump.Grounded) {
			animationList[0].Play("SnowManJump1");
		}

		if (!animationList[0].IsPlaying("SnowManJump1")) {
			jumping = false;
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
		Destroy (GetComponent<Jump> ());
		Destroy (GetComponent<Animation> ());
		Destroy (transform.GetComponent<Attack> ());
		ScoreManager.score += 1;
		jump.setJumpMagnitude(0f);

		head.gameObject.AddComponent<Rigidbody> ();
		body.gameObject.AddComponent<Rigidbody> ();
		legs.gameObject.AddComponent<Rigidbody> ();

		body.rigidbody.freezeRotation = true;
		legs.rigidbody.freezeRotation = true;

		head.rigidbody.mass = .0000001f;
		body.rigidbody.mass = .0000001f;
		legs.rigidbody.mass = .0000001f;

		yield return new WaitForSeconds(1f);

		if (spurtCloneBody)
			spurtCloneBody.GetComponent<ParticleSystem>().Stop();
		if (spurtCloneLegs)
			spurtCloneLegs.GetComponent<ParticleSystem>().Stop();
		  

		yield return new WaitForSeconds(4f);
		
		Destroy (spurtCloneBody);

		transform.GetChild (0).gameObject.SetActive (false);

		Instantiate (explosion, transform.position + Vector3.up*2, transform.rotation);

		Destroy (head.gameObject);
		Destroy (body.gameObject);
		Destroy (legs.gameObject);
		Destroy (this.gameObject);

		//this.gameObject.SetActive (false);
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
		jump.setHeight(.5f);
		jump.setJumpMagnitude(3f);
		jump.setJumpAngle(70f);
		
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
