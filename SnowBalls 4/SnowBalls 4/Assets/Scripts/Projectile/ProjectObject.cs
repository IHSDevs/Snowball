using UnityEngine;
using System.Collections;

public class ProjectObject : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Vector3 myVelocity = new Vector3 (Random.Range (-3, 3), Random.Range (4, 8), Random.Range (-3, 3));
		Vector3 myAngularVelocity = new Vector3 (Random.Range (-3, 3), Random.Range (3, 3), Random.Range (-3, 3));
		transform.rigidbody.velocity = myVelocity;
		transform.rigidbody.angularVelocity = myAngularVelocity;



		StartCoroutine("deathDelay");
	}

	IEnumerator deathDelay()
	{

		yield return new WaitForSeconds (3f);

		Destroy (this.gameObject);
		//this.gameObject.SetActive (false);
		//Destroy (this);
	}

	// Update is called once per frame
	void Update () {
		
	}
}
