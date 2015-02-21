using UnityEngine;
using System.Collections;

public class WeaponController : MonoBehaviour {

	public Transform projectile, mainCamera;
	public GameObject[] beforeFire, afterFire;
	public int damage, volley;
	public float velocity, fireDelay, reloadDelay;
	public Animation animations;
	public PlayerController pc;
	public Vector3 transformShift;

	private bool active;

	// Use this for initialization
	void Start () {
		
		active = false;

	}

	//toggles the bool activ
	public void toggleActive () {

		active = !active;

	}

	public void fireWeapon () { 

		StartCoroutine ("weaponSequencer");

	}

	IEnumerator weaponSequencer () {

		yield return new WaitForSeconds (fireDelay);

		launchProjectile ();

		foreach (GameObject g in beforeFire) {
			g.SetActive (false);
		}
		foreach (GameObject g in afterFire) {
			g.SetActive (true);
		}

		yield return new WaitForSeconds (reloadDelay);

		resetWeapon ();

	}

	void launchProjectile () {

		Transform t = Instantiate(projectile, mainCamera.transform.position + transformShift, Camera.main.transform.rotation) as Transform;

		pc.EnqueueProjectile (t.gameObject);

	}

	//resets animations
	void resetWeapon () {

		foreach (GameObject g in beforeFire) {
			g.SetActive (true);
		}
		foreach (GameObject g in afterFire) {
			g.SetActive (false);
		}

	}

	public bool Active {
		get {
			return active;
		}
		set {
			active = value;
		}
	}
}
