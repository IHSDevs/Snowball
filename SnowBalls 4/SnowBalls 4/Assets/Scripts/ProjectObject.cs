using UnityEngine;
using System.Collections;

public class ProjectObject : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Vector3 myVelocity = new Vector3 (Random.Range (-5, 5), Random.Range (6, 12), Random.Range (-5, 5));
		transform.rigidbody.velocity = myVelocity;

		StartCoroutine("deathDelay");
	}

	IEnumerator deathDelay()
	{

		print ("jfd");
		yield return new WaitForSeconds (3f);

		print ("?");


		this.gameObject.SetActive (false);
		//Destroy (this);
	}

	// Update is called once per frame
	void Update () {
		
	}
}
