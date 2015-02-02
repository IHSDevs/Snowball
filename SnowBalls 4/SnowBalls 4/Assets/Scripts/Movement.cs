using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {

	public float howClose = 2.5f;
	public Transform player;
	public float speed = 10f;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		Vector3 playerPosition = new Vector3 (player.position.x,transform.position.y,player.position.z);
		if (Vector3.Distance (transform.position, player.position) > howClose) 
		{
			float step = speed * Time.deltaTime;
			transform.position = Vector3.MoveTowards(transform.position, playerPosition, step);
			
		}
	}
}
