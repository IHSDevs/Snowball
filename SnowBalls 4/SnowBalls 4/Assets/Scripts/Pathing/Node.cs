using UnityEngine;
using System.Collections;

public class Node {
	int distanceFromTarget;
	bool traversible;
	Node parent;
	Vector3 worldPos;

	//Constructor
	public Node(bool _traversible, Vector3 _worldPos)
	{
		traversible = _traversible;
		worldPos = _worldPos;
	}

	//Accessor for DistanceFromTarget
	public int DistanceFromTarget
	{
		get {
			return distanceFromTarget;
		}
		set {
			distanceFromTarget = value;
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
}
