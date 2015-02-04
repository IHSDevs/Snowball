using UnityEngine;
using System.Collections;

public class Jump : MonoBehaviour {
	
	public float howClose = 1f;
	public float speed = 10f;

	float jumpMagnitude = 5f;
	float timer = 0f;
	
	Vector3 waypoint;
	Vector3 jumpVector = new Vector3(0,0,0);
	Vector3 gravityVector = new Vector3(0,-9.8f,0);
	Vector3 direction;
	Vector3 localLeft; 

	bool grounded;

	LayerMask layerMask = ~2;

	//RaycastHit hit;

	// Use this for initialization
	void Start () {
		waypoint = new Vector3 (0, 0, 0);
		direction = (waypoint - transform.position);
		localLeft = transform.worldToLocalMatrix.MultiplyVector(Vector3.left);
	}
	
	// Update is called once per frame
	void Update () {
		grounded = Physics.Raycast (transform.position + Vector3.up, transform.TransformDirection (Vector3.down), 1f, layerMask);

		direction = (waypoint - transform.position);
		direction.y = 0;

		timer -= Time.deltaTime;
		if (Vector3.Distance (transform.position, waypoint) > howClose && grounded) 
		{
			//float step = speed * Time.deltaTime;
			//transform.position = Vector3.MoveTowards(transform.position, waypoint, step);

			jumpVector = Quaternion.AngleAxis(-60, Vector3.Cross(Vector3.up, direction) ) *direction.normalized*jumpMagnitude ;
			Debug.DrawLine (transform.position, transform.position + jumpVector, Color.green, 5, true);
			print (jumpVector);
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
}
