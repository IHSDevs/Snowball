using UnityEngine;
using System.Collections;

public class GameOverManager : MonoBehaviour {

	public HealthPlayer health;
	public float delay = 5f;
	
	float restartTimer;
	Animator anim;
	// Use this for initialization
	void Awake () {
		anim = GetComponent <Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (health.currentHealth <= 0)
		{
			anim.SetTrigger("GameOver");
			restartTimer += Time.deltaTime;
			if (restartTimer > delay)
			{
				Application.LoadLevel(Application.loadedLevel);
			}
		}
	
	}
}
