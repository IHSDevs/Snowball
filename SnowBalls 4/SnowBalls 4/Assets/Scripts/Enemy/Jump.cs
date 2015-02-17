using UnityEngine;
using System.Collections;
using System.Diagnostics;

public class Jump : MonoBehaviour {

	public float speed, jumpMagnitude, jumpAngle;

	float height;
	
	Vector3 waypoint, jumpVector, gravityVector, jumpDirection;

	bool grounded, flipFlopper;

	LayerMask layerMask;

	//Stopwatch timer;

	// Use this for initialization
	void Awake () {
		flipFlopper = true;

		grounded = true;
		//timer = new Stopwatch();

		height = 1;
		
		waypoint = new Vector3 (0, 0, 0);
		jumpVector = new Vector3(0,0,0);
		gravityVector = new Vector3(0,-9.8f,0);
		jumpDirection = (waypoint - transform.position);

		//All but ignore raycast layer
		layerMask = 1 << 12;
		layerMask = ~layerMask;

	}

	/**
	void Update()
	{
		if (grounded && !flipFlopper) {
			timer.Stop();
			flipFlopper = true;
			print (timer.ElapsedMilliseconds);
			timer.Reset();
		}
		if (grounded && flipFlopper) {
			timer.Start();
			flipFlopper = false;
		}


	}*/

	void Update()
	{
		grounded = Physics.Raycast (transform.position + Vector3.up*2, transform.TransformDirection (Vector3.down), height*2, layerMask);
		if (!(jumpVector.y < 0 && grounded)) {
			transform.position += jumpVector*Time.deltaTime;
			jumpVector += gravityVector * Time.deltaTime;
		}
		Vector3 transitionDirection = Vector3.RotateTowards(transform.forward, jumpDirection, Mathf.PI*Time.deltaTime, 0f);
		
		transform.rotation = Quaternion.LookRotation(transitionDirection);
	}
	
	// Tries to jump. if succeeds, return true; else, return false
	public void Launch () {

		//Debug.DrawLine (transform.position + Vector3.up*2, transform.position, Color.green, .1f, true);

		jumpDirection = (waypoint - transform.position);
		jumpDirection.y = 0;

		transform.rigidbody.velocity = Vector3.zero;

		jumpVector = Quaternion.AngleAxis(-(jumpAngle), Vector3.Cross(Vector3.up, jumpDirection) ) *jumpDirection.normalized*jumpMagnitude;
	}


	public bool Grounded {
		get {
			return grounded;
		}
		set {
			grounded = value;
		}
	}

	//returns position
	public Vector3 getPosition()
	{
		return transform.position;
	}


	//sets waypoint
	public void setWaypoint(Vector3 pos)
	{
		waypoint = pos;
	}

	public void setHeight(float newHeight)
	{
		height = newHeight;
	}

	public void setJumpMagnitude(float mag)
	{
		jumpMagnitude = mag;
	}

	public void setJumpAngle(float angle)
	{
		jumpAngle = angle;
	}
}
