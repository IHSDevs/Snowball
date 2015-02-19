using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	public float mouseSensitivity = 2.0f;
	public float lookRange = 60.0f;
	public Transform snowball;
	public Camera mainCamera;
	public float snowballVelocity = 40;
	public int maxBalls = 20;
	public bool isMobile;


	Animation animationList;

	Transform myTransform;

	Queue SnowballQueue;
	int queueLen = 0;
	float verticalRotation = 0;


	bool canFire = true;

	// Use this for initialization
	void Start () {
		Screen.lockCursor = true;
		myTransform = GetComponent<Transform> ();
		animationList = GetComponentInChildren <Animation> ();
		SnowballQueue = new Queue ();
	}
	

	void Update () {
	float rotateX = 0;

		if (isMobile) {
			if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved) {
				float mSense=mouseSensitivity/2;
				Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
				rotateX = touchDeltaPosition.x * mSense;
				verticalRotation -= touchDeltaPosition.y * mSense;
			}
		} else {
			rotateX = Input.GetAxis ("Mouse X") * mouseSensitivity;
			verticalRotation -= Input.GetAxis ("Mouse Y") * mouseSensitivity;

		}
		this.transform.Rotate (0, rotateX, 0);//Rotates Player
		verticalRotation = Mathf.Clamp (verticalRotation, -lookRange, lookRange);
		Camera.main.transform.localRotation = Quaternion.Euler (verticalRotation, 0, 0);
		//USE FOR MOVEMENT
		//currRot = Camera.main.transform.rotation.ToEulerAngles ();

		//Animations
		if (!animationList.animation.IsPlaying("ThrowBall")) {
			animationList.animation.Rewind("ThrowBall");
		}
		if (isMobile) {
			if (Input.touchCount > 1) {
				animationList.animation.Play ("ThrowBall");
				StartCoroutine ("fireDelay");
			}
		} else {
			if (Input.GetButtonDown ("Fire1")) {
				animationList.animation.Play ("ThrowBall");
				StartCoroutine ("fireDelay");
			}
		}
	}

	IEnumerator fireDelay(){
		if (queueLen == maxBalls) {
			GameObject trash = (GameObject)SnowballQueue.Dequeue();
			Destroy(trash);
			queueLen --;
		}

		if (!canFire) {
			return false;
		}

		canFire = false;
		yield return new WaitForSeconds(.2f);

		Transform t = Instantiate(snowball, myTransform.position + mainCamera.transform.forward + mainCamera.transform.up + mainCamera.transform.right*.5f, Camera.main.transform.rotation) as Transform;

		GameObject objectClone = t.gameObject;
		objectClone.rigidbody.velocity = mainCamera.transform.forward*snowballVelocity;

		SnowballQueue.Enqueue (objectClone);
		queueLen ++;
		yield return new WaitForSeconds(.8f);
		canFire = true;
	}
}
