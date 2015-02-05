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

	public Transform GibNose;
	public Transform GibEye;
	public Transform GibButton;

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


	bool hasHead = true;
	bool hasLegs = true;
	
	void Update () {

		if (hasLegs && legsCount > 0) {

			legs.gameObject.SetActive(false);
			myJump.setHeight(.5f);
			myJump.setJumpMagnitude(3f);
			myJump.setJumpAngle(70f);

			Instantiate (explosion, transform.position + Vector3.up*.5f, transform.rotation);

			hasLegs = false;

			Instantiate(GibNose, transform.position, transform.rotation);
		}


		headCount = headCounter.getHits ();
		bodyCount = bodyCounter.getHits ();
		legsCount = legsCounter.getHits ();

		if ((headMultiplier * headCount + bodyMultiplier * bodyCount + legsMultiplier * legsCount) >= health) {
			health = 0;
		}

		if (hasHead && headCount > 0) {

			hasHead = false;

			head.gameObject.SetActive(false);

			Instantiate(GibNose, transform.position, transform.rotation);
			Instantiate(GibEye, transform.position, transform.rotation);
			Instantiate(GibButton, transform.position, transform.rotation);
			Instantiate(GibButton, transform.position, transform.rotation);
			Instantiate(GibButton, transform.position, transform.rotation);

			//Destroy(myJump);
			myJump.setJumpMagnitude(0f);

			Instantiate (explosion, transform.position + Vector3.up*3, transform.rotation);

			StartCoroutine("destroySnowman");
		}

		/**
		if (health <= 0) {
			Vector3 height = new Vector3(0, 2, 0);
			Instantiate (explosion, transform.position + height, transform.rotation);
			Destroy (gameObject);
			ScoreManager.score += 1;


		}*/
	}

	public void setHealth(float newHealth)
	{
		health = newHealth;
	}

	IEnumerator destroySnowman()
	{
		yield return new WaitForSeconds(2f);

		this.gameObject.SetActive (false);
	}
}
