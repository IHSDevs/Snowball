using UnityEngine;
using System.Collections;

public class Grid : MonoBehaviour {

	public LayerMask obstacleLayer;
	public int length;//number of nodes per side of grid
	public float size;//physical size of grid

	Node[,] grid;
	Vector3 pos;

	float nodeRadius;

	//instantiates an array of Nodes
	public void CreateGrid ()
	{
		pos = transform.position;
		grid = new Node[length, length];
		nodeRadius = size / (length*2);
		float currentX, currentZ;
		Vector3 currNodePos;
		bool traversible;

		for (int i = 0; i < length; i ++) {
			for (int j = 0; j < length; j ++) {
				currentX = (pos.x - size/2) + 2*nodeRadius*i + nodeRadius;
				currentZ = (pos.y - size/2) + 2*nodeRadius*j + nodeRadius;
				currNodePos = new Vector3(currentX, 0, currentZ);

				traversible = !Physics.CheckSphere(currNodePos, nodeRadius, obstacleLayer);

				grid[i,j] = new Node(traversible, currNodePos);
			}
		}
	}

	void OnDrawGizmos() 
	{
		if (grid != null) 
		{
			foreach (Node n in grid) 
			{
				Gizmos.color = (n.Traversible)?Color.white:Color.red;
				Gizmos.DrawCube(n.WorldPos, Vector3.one * (nodeRadius*2 - .2f) - Vector3.up * nodeRadius * .9f);
			}
		}
	}

	//Takes a pos and returns the correpsonding node, if possible
	public Node NodeFromPos(Vector3 position)
	{
		Vector3 posDiff = position - pos;//difference in position
		float greatestDiff = (position.x - pos.x) > (position.y - pos.y) ? 
			(position.x-pos.x) 
			: (position.y-pos.y);//greatest difference between floats of positiona nd pos
		return (Mathf.Abs (greatestDiff) < size / 2) ? 
			grid [(int)(posDiff.x / size), (int)(posDiff.y / size)] 
			: null;//returns Node, else null
	}
}
