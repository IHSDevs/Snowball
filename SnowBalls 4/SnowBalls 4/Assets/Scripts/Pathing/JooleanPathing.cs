using UnityEngine;
using System.Collections;

public class JooleanPathing : MonoBehaviour {

	public Grid grid;
	public Transform start;
	public Transform finish;

	void Start ()
	{
		grid.CreateGrid ();
	}


}
