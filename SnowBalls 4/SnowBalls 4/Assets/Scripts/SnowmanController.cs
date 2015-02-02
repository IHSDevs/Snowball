using UnityEngine;
using System.Collections;


public class SnowmanController : MonoBehaviour {
	
	public GameObject explosion;
	public int explosionLimit = 1;
	public float health = 100f;
	public float headShot = 100f;
	public float bodyShot = 50f;
	public float legShot = 25f;
	private int headCount = 0;
	private int bodyCount = 0;
	private int legCount = 0;
	

	// Update is called once per frame
	void Update () {
		int children = 0;
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
