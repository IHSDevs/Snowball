using UnityEngine;
using System.Collections;

public class ProjectObject : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Vector3 myVelocity = new Vector3 (Random.Range (-4, 4), Random.Range (-4, 4), Random.Range (-4, 4));
		transform.rigidbody.velocity = myVelocity;

		StartCoroutine("deathDelay");
	}

	IEnumerator deathDelay()
	{

		print ("jfd");
		yield return new WaitForSeconds (1f);

		print ("?");


		this.gameObject.SetActive (false);
		//Destroy (this);
	}

	// Update is called once per frame
	void Update () {
		
	}
}
