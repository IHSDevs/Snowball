using UnityEngine;
using System.Collections;

public class Node {
	int distance;

	int direction;
	//instructions for directions
	//3  6  9
	//2  5  8
	//1  4  7
	
	bool traversible;

	public bool testColor;

	bool diagonal;
	Node parent;
	int x, y;
	Vector3 worldPos;

	//Constructor
	public Node(bool _traversible, Vector3 _worldPos, int _x, int _y)
	{
		testColor = false;
		traversible = _traversible;
		worldPos = _worldPos;

		x = _x;
		y = _y;

		distance = 999;

		diagonal = false;
	}

	//Accessor for DistanceFromTarget
	public int Distance
	{
		get {
			return distance;
		}
		set {
			distance = value;
		}
	}

	//Accessor for worldPos
	public Vector3 WorldPos {
		get {
			return worldPos;
		}
		set {
			worldPos = value;
		}
	}

	//Accessor for parentNode
	public Node Parent {
		get {
			return parent;
		}
		set {
			parent = value;
		}
	}

	//Accessor for traversible
	public bool Traversible {
		get {
			return traversible;
		}
		set {
			traversible = value;
		}
	}

	//Accessor for diagonal
	public bool Diagonal {
		get {
			return diagonal;
		}
		set {
			diagonal = value;
		}
	}

	//Accessor for x
	public int X{
		get {
			return x;
		}
	}

	//Accessor for y
	public int Y{
		get{
			return y;
		}
	}

	//Accessor for direction
	public int Direction{
		get{
			return direction;
		}
		set {
			direction = value;
		}
	}
}
