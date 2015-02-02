﻿using UnityEngine;
using System.Collections;

public class SnowballController : MonoBehaviour {

	public Transform myTransform;
	public bool start = true;
	public float headShot = 100f;
	public float bodyShot = 50f;
	public float legShot = 40f;
	public bool collision = false;
	private Collider hitObj;


	Vector3 myVelocity;
	Vector3 fwdRot;
	RaycastHit hit;


	void Start()
	{
		resolveCollision ();
	}

	void Update()
	{
		resolveCollision ();
	}


	void OnTriggerEnter(Collider other)
	{
		if (!collision) {
						start = false;
						myTransform.parent = other.transform;
						Destroy (myTransform.rigidbody);
						Destroy (myTransform.collider);
						//checks if collider is a snowman
						if (other.transform.parent != null && (other.transform.parent.name.Equals ("SnowMan") || 
								other.transform.parent.name.Equals ("SnowMan(Clone)"))) {
								print ("contact");
								GameObject snowman = other.transform.parent.gameObject;
								SnowmanController otherScript = snowman.GetComponent<SnowmanController> ();
								//catches the case when the ball scims the snowman and the raycast misses the collison
								if (hitObj == null) {
										hitObj = other;
								}
								//decrements health based on hit location
								if (hitObj.transform.name.Equals ("Head")) {
										otherScript.health -= headShot;
								} else if (hitObj.transform.name.Equals ("Thorax")) {
										otherScript.health -= bodyShot;
								} else if (hitObj.transform.name.Equals ("Abdomen")) {
										otherScript.health -= legShot;
								}
						}
						collision = true;
				}
		//Destroy (myTransform.GetComponent<Collider> ());
		//Destroy (myTransform.GetComponent<Rigidbody> ());
		//myTransform.position = other.ClosestPointOnBounds (myTransform.position) - transform.forward * .1f;
	}

	//Use raycast to detect upcoming collison
	void resolveCollision()
	{
		if (start) {
			myVelocity = myTransform.rigidbody.velocity;

			//orients towards myTransform position during next frame
			Vector3 targetRotation = (myTransform.position + myVelocity * Time.deltaTime) - transform.position;

			Debug.DrawLine (myTransform.position, (myTransform.position + myVelocity * Time.deltaTime) + myVelocity.normalized*.35f, Color.green, 5, true);

			//myTransform.rigidbody.velocity.magnitude * Time.deltaTime * 1.35f - adjusts line length depending on velocity. 1.35f corrects for Time.deltaTime glitch.
			if (Physics.Raycast (transform.position, targetRotation, out hit, myVelocity.magnitude * Time.deltaTime * 1.35f)) {
		
				try {
					if (hit.collider != null) {
						//Moves snowball to surface of hit.collider
						//myTransform.position = hit.collider.ClosestPointOnBounds(myTransform.position) + myTransform.rigidbody.velocity.normalized*.1f;
						myTransform.position = hit.collider.ClosestPointOnBounds(myTransform.position + myVelocity.normalized*hit.distance) + myVelocity.normalized*.07f;
						//Determines where the ball will hit so that the collider doesn't collide with multiple body parts
						hitObj = hit.collider;

						//prevents snowball from moving before OnTriggerEnter is called
						myTransform.rigidbody.velocity *= 0;
					}

				} catch (MissingComponentException) {
					start = true;
				}
			}
		}
	}
}
