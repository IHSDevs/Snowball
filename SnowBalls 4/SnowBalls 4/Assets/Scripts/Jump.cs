using UnityEngine;
using System.Collections;

public class Jump : MonoBehaviour {

	public float speed = 10f;

	public float jumpMagnitude = 5f;
	public float jumpAngle;

	float timer = 0f;

	float height = 1;
	
	Vector3 waypoint;
	Vector3 jumpVector = new Vector3(0,0,0);
	Vector3 gravityVector = new Vector3(0,-9.8f,0);
	Vector3 direction;

	bool grounded;

	LayerMask layerMask;


	// Use this for initialization
	void Start () {
		waypoint = new Vector3 (0, 0, 0);
		direction = (waypoint - transform.position);

		layerMask = 1 << 12;
		layerMask = ~layerMask;
	}
	
	// Update is called once per frame
	void Update () {

		Vector3 transitionDirection = Vector3.RotateTowards(transform.forward, direction, Mathf.PI*Time.deltaTime, 0f);

		transform.rotation = Quaternion.LookRotation(transitionDirection);



		grounded = Physics.Raycast (transform.position + Vector3.up*2, transform.TransformDirection (Vector3.down), height*2, layerMask);

		Debug.DrawLine (transform.position + Vector3.up*2, transform.position, Color.green, .1f, true);

		direction = (waypoint - transform.position);
		direction.y = 0;

		timer -= Time.deltaTime;

		if (grounded) 
		{
			//float step = speed * Time.deltaTime;
			//transform.position = Vector3.MoveTowards(transform.position, waypoint, step);


			transform.rigidbody.velocity = Vector3.zero;

			jumpVector = Quaternion.AngleAxis(-(jumpAngle), Vector3.Cross(Vector3.up, direction) ) *direction.normalized*jumpMagnitude ;

			timer = 5f;



		}

		transform.position += jumpVector*Time.deltaTime;
		jumpVector += gravityVector * Time.deltaTime;
	}
	
	public Vector3 getPosition()
	{
		return transform.position;
	}
	
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
