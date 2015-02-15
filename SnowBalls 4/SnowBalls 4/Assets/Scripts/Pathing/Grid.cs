using UnityEngine;
using System.Collections;

public class Grid : MonoBehaviour {

	public LayerMask obstacleLayer;
	public int length;//number of nodes per side of grid
	public float size;//physical size of grid

	Node[,] grid;
	Vector3 pos;

	float nodeRadius;
	float nodeDiameter;

	int obstacles;

	//instantiates an array of Nodes and returns number of obstacle Nodes
	public void CreateGrid ()
	{
		pos = transform.position;
		grid = new Node[length, length];
		nodeRadius = size / (length*2);
		nodeDiameter = size / length;
		float currentX, currentZ;
		Vector3 currNodePos;
		bool traversible;

		obstacles = 0;

		for (int i = 0; i < length; i ++) {
			for (int j = 0; j < length; j ++) {

				currentX = (pos.x - size/2) + nodeDiameter*i + nodeRadius;
				currentZ = (pos.y - size/2) + nodeDiameter*j + nodeRadius;

				currNodePos = new Vector3(currentX, 0, currentZ);

				traversible = !Physics.CheckSphere(currNodePos, nodeRadius, obstacleLayer);

				obstacles = (traversible) ? obstacles : obstacles + 1;//increments obstacles if not traversible

				grid[i,j] = new Node(traversible, currNodePos, i, j);
			}
		}
	}

	//Draws nodes as rectangles
	void OnDrawGizmos() 
	{
		if (grid != null) 
		{
			foreach (Node n in grid) 
			{
				Gizmos.color = !(n.Traversible)? Color.red: (n.testColor)? Color.blue : Color.white;
				Gizmos.DrawCube(n.WorldPos, Vector3.one * (nodeDiameter - .1f) - Vector3.up * nodeRadius * .9f);
			}
		}
	}

	//Takes a pos and returns the correpsonding node, if possible
	public Node NodeFromPos(Vector3 position)
	{
		Vector3 posDiff = position + pos + Vector3.one * size/2;//difference in position
		posDiff /= nodeDiameter;

		int i = ClampToInt (posDiff.x, 0, length - 1);
		int j = ClampToInt (posDiff.z, 0, length - 1);

		return grid[i,j];
	}

	//Checks if OOB
	public bool IsOOB(int _x, int _y)
	{
		return !(_x > 0 && _x < length && _y > 0 && _y < length);
	}

	//Clamps an int to specified value
	int ClampToInt(int i, int min, int max)
	{
		return (i < min) ? min : (i > max) ? max : i;
	}

	//Clamps float to specified value
	int ClampToInt(float f, int min, int max)
	{
		int i = (int)f;
		return (i < min) ? min : (i > max) ? max : i;
	}

	//Accessor for Obstacles
	public int Obstacles
	{
		get {
			return obstacles;
		}
	}

	//Accessor for grid
	public Node NodeFromCoord(int x, int y)
	{
		return grid[x,y];
	}
}
