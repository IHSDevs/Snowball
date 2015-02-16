using UnityEngine;
using System.Collections;

public class Node {
	int distance;

	int degreesSeperation;//degrees of seperation from universal parent node

	int direction;
	//instructions for directions
	//3  6  9
	//2  5  8
	//1  4  7
	
	bool traversible;

	public bool testColor;

	Node parent;
	int x, y;
	Vector3 position;

	//Constructor
	public Node(bool _traversible, Vector3 _position, int _x, int _y)
	{
		testColor = false;
		traversible = _traversible;
		position = _position;

		x = _x;
		y = _y;

		distance = -1;
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

	//Accessor for position
	public Vector3 Position {
		get {
			return position;
		}
		set {
			position = value;
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

	//Accessor for degreesSeperation
	public int DegreesSeperation
	{
		get{
			return degreesSeperation;
		}
		set {
			degreesSeperation = value;
		}
	}
}
