using UnityEngine;
using System.Collections;

public class Attack : MonoBehaviour {

	public Transform snowmanLocation;
	GameObject player;
	public float timeBetweenAttack = 0.5f;
	public int attackDamage = 10;
	public float range = 4f;
	HealthPlayer health;
	bool playerInRange;
	float timer;
	// Use this for initialization
	void Awake () {
		player = GameObject.FindGameObjectWithTag("Player");
		health = player.GetComponent <HealthPlayer> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		timer += Time.deltaTime;
		if (timer >= timeBetweenAttack && Vector3.Distance(snowmanLocation.position, player.transform.position) <= range)
		{
			Attacker();
		}
	}
	
	void Attacker ()
	{
		timer = 0;
		if (health.currentHealth > 0)
		{
			health.takeDamage(attackDamage);
		}
	}
}
