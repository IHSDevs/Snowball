using UnityEngine;
using System.Collections;

public class ProjectileCounter : MonoBehaviour {

	public SnowmanController mySnowmanController;

	int accruedDamage = 0;

	public void Damage(int dmg) {
		accruedDamage += dmg;
		print (dmg + " " + accruedDamage + " D:");
	}

	public int GetDamage() {
		int dmg = accruedDamage;
		accruedDamage = 0;
		return dmg;
	}

}
