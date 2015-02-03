using UnityEngine;
using System.Collections;


//This class still needs work. I didn't write very efficient code here.
public class WaypointController: MonoBehaviour {
	GameObject[] waypoints;
	ArrayList travellers;
	
	
	// Use this for initialization
	void Start () {
		waypoints = new GameObject[transform.childCount];
		travellers = new ArrayList ();
		int i = 0;
		foreach (Transform child in transform) {
			waypoints[i] = child.gameObject;
			i++;
		}
		
		//print (1);
	}
	
	// Update is called once per frame
	void Update () {
		
		for (int i = 0; i < waypoints.Length; i ++) {
			for (int j = 0; j < travellers.Count; j ++) {
				if (!(GameObject)travellers[j])
				{
					travellers.RemoveAt(j);
					j--;
				}
				else
				{
					GameObject obj = ((GameObject)travellers[j]);
					Vector3 position = obj.GetComponent<Movement>().getPosition();
					if (Vector3.Distance (waypoints [i].transform.position, position) < waypoints [i].GetComponent<Waypoint> ().radius)
						incrementWaypoint(i, obj);
				}
			}
		}
	}
	
	public void addTraveller(GameObject traveller)
	{
		traveller.GetComponent<Movement>().setWaypoint(waypoints[0].transform.position);
		travellers.Add (traveller);
	}
	
	void incrementWaypoint(int waypointNum, GameObject obj)
	{
		if (waypointNum == waypoints.Length - 1)
			waypointNum -= 1;
		obj.GetComponent<Movement> ().setWaypoint (waypoints [waypointNum + 1].transform.position);
	}
	
}