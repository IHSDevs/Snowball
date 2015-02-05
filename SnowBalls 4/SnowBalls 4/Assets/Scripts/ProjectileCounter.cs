using UnityEngine;
using System.Collections;

public class ProjectileCounter : MonoBehaviour {

	int hits = 0;


	void Start()
	{
	}

	public int getHits()
	{
		return hits;
	}

	public void addHit()
	{
		hits ++;
	}
}
