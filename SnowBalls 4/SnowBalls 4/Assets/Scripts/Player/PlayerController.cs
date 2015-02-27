using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	
	public bool isMobile;
	public float mouseSensitivity, lookRange, snowballVelocity, icicleVelocity, shovelVelocity, playerHeight;
	public Transform snowball, icicle, propIcicle;
	public Transform[] openCbow, closedCbow;
	public GameObject mitten, icicleLauncher, shovel;
	public Camera mainCamera;
	public int maxBalls = 20;

	private Animation animationList;

	//Projectile Queue
	private Queue SnowballQueue;
	private int queueLen, activeWeapon;
	private float verticalRotation = 0;

	//Can fire weapon?
	private bool canFire = true;

	private Vector3 transformShift;

	// Use this for initialization
	void Start () {

		queueLen = 0;

		//3 = mittens, 1 = icicle launcher, 2 = shovel
		activeWeapon = 3;

		Screen.lockCursor = true;
		animationList = GetComponentInChildren <Animation> ();
		SnowballQueue = new Queue ();

		//Determines initial y position
		RaycastHit hit;

		if (Physics.Raycast (transform.position + Vector3.up * 10f, Vector3.down, out hit)) {
			transform.position -= Vector3.down * (10 - hit.distance + playerHeight);
		}
	}

	//Controls player rotation
	void Rotate () {

		float rotateX = 0;

		if (isMobile) {
			if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved) {
				
				float mSense=mouseSensitivity/2;
				Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
				rotateX = touchDeltaPosition.x * mSense;
				verticalRotation -= touchDeltaPosition.y * mSense;
				
				this.transform.Rotate (0, rotateX, 0);//Rotates Player
				
				verticalRotation = Mathf.Clamp (verticalRotation, -lookRange, lookRange);
				
				Camera.main.transform.localRotation = Quaternion.Euler (verticalRotation, 0, 0);
			}
		}
		else {
			
			rotateX = Input.GetAxis ("Mouse X") * mouseSensitivity;
			verticalRotation -= Input.GetAxis ("Mouse Y") * mouseSensitivity;
			
			this.transform.Rotate (0, rotateX, 0);//Rotates Player
			
			verticalRotation = Mathf.Clamp (verticalRotation, -lookRange, lookRange);
			
			Camera.main.transform.localRotation = Quaternion.Euler (verticalRotation, 0, 0);
		}
	}

	//Checks if player is currently trying to fire
	void CheckFire () {
		if (isMobile) {
			if (Input.touchCount > 1) {
				animationList.animation.Play ("ThrowBall");
				StartCoroutine ("icicleFire");
			}
		}
		else {
			if (Input.GetButtonDown ("Fire1")) {
				if (activeWeapon == 3) {
					animationList.animation.Play ("ThrowBall");
					StartCoroutine ("mittenFire");
				}
				else if (activeWeapon == 1) {
					animationList.animation.Play ("ThrowBall");
					StartCoroutine ("icicleFire");
				}
				else if (activeWeapon == 2) {
					animationList.animation.Play ("ThrowBall");
					StartCoroutine ("shovelFire");
				}
			}
		}
	}

	//Updates Player
	void Update () {

		if (activeWeapon == 3 && !(animationList.animation.IsPlaying("ThrowBall"))) {
			animationList.animation.Rewind("ThrowBall");
		}

		Rotate();
		CheckWeapon();
		CheckFire();

	}

	//checks which weapon is currently drawn
	void CheckWeapon () {

		if (isMobile) {

		}
		else {
			if (Input.GetKeyDown ("3") ) {//mitten
				activeWeapon = 3;

				DisableWeapons();
				mitten.SetActive(true);


			}
			else if (Input.GetKeyDown ("1") ) {//icicle
				activeWeapon = 1;

				DisableWeapons();
				icicleLauncher.SetActive(true);


			}
			else if (Input.GetKeyDown ("2") ) {//shovel
				activeWeapon = 2;

				DisableWeapons();
				shovel.SetActive(true);


			}
		}

	}

	//Disables all weapon GameObjects
	void DisableWeapons() {
		mitten.SetActive(false);
		icicleLauncher.SetActive(false);
		shovel.SetActive(false);
	}

	void fireProjectile(Transform projectile, float velocity, int quantity, bool variance) {

		//adjusts the projectile
		transformShift = Vector3.zero;//mainCamera.transform.forward + mainCamera.transform.up + mainCamera.transform.right*.5f;

		for (;quantity > 0; quantity --) {

			if (variance) {
				transformShift = transform.right*Random.Range (.3f,1) + transform.up*Random.Range (0,.7f) + transform.forward * Random.Range (.3f,1);
			}
			else {
				if (activeWeapon == 1) {
					transformShift = transform.right * .3f + transform.up * -.3f;
				}
				else {
					transformShift = transform.right * .3f;
				}
			}

			Transform t = Instantiate(projectile, mainCamera.transform.position + transformShift, Camera.main.transform.rotation) as Transform;

			GameObject objectClone = t.gameObject;
			objectClone.rigidbody.velocity = mainCamera.transform.forward*velocity;
			
			SnowballQueue.Enqueue (objectClone);
			queueLen ++;
		}

	}

	IEnumerator icicleFire(){


		
		if (!canFire) {
			return false;
		}
		
		canFire = false;

		foreach (Transform t in openCbow) {
			t.gameObject.SetActive(true);
		}
		foreach (Transform t in closedCbow) {
			t.gameObject.SetActive(false);
		}

		propIcicle.gameObject.SetActive(false);
		
		fireProjectile(icicle, icicleVelocity, 1, false);
		
		yield return new WaitForSeconds(2f);
		
		canFire = true;

		foreach (Transform t in openCbow) {
			t.gameObject.SetActive(false);
		}
		foreach (Transform t in closedCbow) {
			t.gameObject.SetActive(true);
		}

		propIcicle.gameObject.SetActive(true);

		while (queueLen >= maxBalls) {
			GameObject trash = (GameObject)SnowballQueue.Dequeue();
			Destroy(trash);
			queueLen --;
		}

	}

	IEnumerator shovelFire(){


		
		if (!canFire) {
			return false;
		}
		
		canFire = false;

		yield return new WaitForSeconds(.3f);

		fireProjectile(snowball, shovelVelocity, 5, true);
		
		yield return new WaitForSeconds(1.3f);
		
		canFire = true;

		while (queueLen >= maxBalls) {
			GameObject trash = (GameObject)SnowballQueue.Dequeue();
			Destroy(trash);
			queueLen --;
		}

	}

	public void EnqueueProjectile (GameObject g) {
		SnowballQueue.Enqueue (g);
		queueLen ++;
	}

	IEnumerator mittenFire(){


		if (!canFire) {
			return false;
		}

		canFire = false;

		yield return new WaitForSeconds(.2f);

		fireProjectile(snowball, snowballVelocity, 1, false);

		yield return new WaitForSeconds(.8f);

		canFire = true;

		while (queueLen >= maxBalls) {
			GameObject trash = (GameObject)SnowballQueue.Dequeue();
			Destroy(trash);
			queueLen --;
		}


	}
}
