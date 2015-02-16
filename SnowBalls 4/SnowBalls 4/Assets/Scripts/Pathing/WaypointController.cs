using UnityEngine;
using System.Collections;


//This class still needs work. I didn't write very efficient code here.
public class WaypointController: MonoBehaviour {
	//GameObject[] waypoints;

	Vector3[] path;
	ArrayList travellers;
	
	//puts all child WayPoint positions into  array  path
	//!note - not in start method because spawner's start will bug out
	public void aggregateWaypoints()
	{
		path = new Vector3[transform.childCount];
		
		int i = 0;
		foreach (Transform child in transform) {
			path[i] = child.position;
			i++;
		}
	}

	//returns Vector3[] path
	public Vector3[] getPath()
	{
		return path;
	}

	//returns path's length
	public int getPathLength()
	{
		return path.Length;
	}
}