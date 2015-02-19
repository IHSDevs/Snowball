using UnityEngine;
using System.Collections;

public class NodeBoy : MonoBehaviour {

	float speed = 3f;
	Vector3 position;
	Vector3[] path;
	int index;

	// Use this for initialization
	void Start () {
		index = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (path != null) {
			if (index == path.Length || path[index] == null) {
				Destroy(this);
			}
			position = Vector3.MoveTowards(position, path[index], speed*Time.deltaTime);
			if (position == path[index]) {
				index ++;
			}
		}
	}

	void OnDrawGizmos()
	{
		Gizmos.color = Color.green;
		Gizmos.DrawCube(position, Vector3.one*.15f);
	}

	public Vector3[] Path
	{
		set {
			path = value;
		}
	}

	public Vector3 Position
	{
		get {
			return position;
		}
		set {
			position = value;
		}
	}
}
