using UnityEngine;
using System;

public class ProjectileController : MonoBehaviour {

	public Transform myTransform;
	public bool start, penetrative;

	public int damage;

	public bool collision = false;
	//private Collider hitObj;


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

	void collisionAction(Transform other)
	{
		if (!((other.transform.gameObject.layer == 12) && penetrative)) {

			start = false;
			myTransform.position = hit.collider.ClosestPointOnBounds(myTransform.position + myVelocity.normalized*hit.distance);
			myTransform.parent = other.parent;
			Destroy (myTransform.GetComponent<Rigidbody>());
			Destroy (myTransform.GetComponent<Collider>());

		}


		try 
		{
			other.parent.GetComponent<ProjectileCounter>().Damage(damage);
		}
		catch (NullReferenceException)
		{

		}
	}

	//Use raycast to detect upcoming collison
	void resolveCollision()
	{
		if (start) {
			myVelocity = myTransform.GetComponent<Rigidbody>().velocity;

			//orients towards myTransform position during next frame
			Vector3 targetRotation = myVelocity * Time.deltaTime;

			Debug.DrawLine (myTransform.position, (myTransform.position + myVelocity * Time.deltaTime) + myVelocity.normalized, Color.green, 5, true);

			//myTransform.rigidbody.velocity.magnitude * Time.deltaTime * 1.35f - adjusts line length depending on velocity. 1.35f corrects for Time.deltaTime glitch.
			if (Physics.Raycast (transform.position, targetRotation, out hit, myVelocity.magnitude * Time.deltaTime * 1.35f)) {
		
				try {

					if (hit.collider != null) {

						collisionAction(hit.collider.transform);

					}

				} catch (MissingComponentException) {
					start = true;
				}
			}
		}
	}
}
