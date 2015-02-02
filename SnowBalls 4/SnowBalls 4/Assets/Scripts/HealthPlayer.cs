using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthPlayer : MonoBehaviour {
	 
	public int startHealth = 100;
	public int currentHealth;
	public Slider healthSlider;
	public Image damageImage;
	public float flashSpeed = 5f;
	public Color flashColor = new Color(1f, 0f, 0f, .1f);
	PlayerController player;
	bool isDead;
	bool damaged;
	// Use this for initialization
	void Awake () {
		currentHealth = startHealth;
		player = GetComponent <PlayerController> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (damaged) {
						damageImage.color = flashColor;
				} else {
						damageImage.color = Color.Lerp (damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
				}
		damaged = false;
	}

	public void takeDamage(int damage)
	{
			damaged = true;
			currentHealth -= damage;
			healthSlider.value = currentHealth;
			if (currentHealth <= 0 && !isDead)
			{
					Dead ();
			}
	}
	
	void Dead()
	{
		isDead = true;
		player.enabled = false;
		//GUI.Label(Rect(50,50,100,25), "You Lose!!!");
	}
		

}
