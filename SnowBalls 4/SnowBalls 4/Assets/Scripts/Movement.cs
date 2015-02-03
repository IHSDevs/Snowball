using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {
	
	public float howClose = 1f;
	public float speed = 10f;
	
	Vector3 waypoint;
	// Use this for initialization
	void Start () {
		waypoint = new Vector3 (0, 0, 0);
	}
	
	// Update is called once per frame
	void Update () {
		if (Vector3.Distance (transform.position, waypoint) > howClose) 
		{
			float step = speed * Time.deltaTime;
			transform.position = Vector3.MoveTowards(transform.position, waypoint, step);
		}
	}
	
	public Vector3 getPosition()
	{
		print ("transform reset");
		return transform.position;
	}
	
	public void setWaypoint(Vector3 pos)
	{
		waypoint = pos;
	}
}
