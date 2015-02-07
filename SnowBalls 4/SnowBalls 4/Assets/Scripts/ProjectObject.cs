using UnityEngine;
using System.Collections;

public class ProjectObject : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Vector3 myVelocity = new Vector3 (Random.Range (-2, 2), Random.Range (6, 10), Random.Range (-2, 2));
		transform.rigidbody.velocity = myVelocity;

		StartCoroutine("deathDelay");
	}

	IEnumerator deathDelay()
	{

		yield return new WaitForSeconds (3f);


		this.gameObject.SetActive (false);
		//Destroy (this);
	}

	// Update is called once per frame
	void Update () {
		
	}
}
