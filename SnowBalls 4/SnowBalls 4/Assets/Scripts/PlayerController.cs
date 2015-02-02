using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	public float mouseSensitivity = 2.0f;
	public float lookRange = 60.0f;
	public Transform snowball;
	public Camera mainCamera;
	public float snowballVelocity = 40;
	public int maxBalls = 20;

	Animation animationList;

	Transform myTransform;

	Queue SnowballQueue;
	int queueLen = 0;

	float verticalRotation = 0;
	Vector3 currRot = new Vector3 (0, 0, 0);

	bool canFire = true;

	// Use this for initialization
	void Start () {
		Screen.lockCursor = true;
		myTransform = GetComponent<Transform> ();
		animationList = GetComponentInChildren <Animation> ();
		SnowballQueue = new Queue ();
	}
	
	// Update is called once per frame
	void Update () {

		float rotateX = Input.GetAxis ("Mouse X") * mouseSensitivity;
		
		this.transform.Rotate (0, rotateX, 0);//Rotates Player
		
		verticalRotation -= Input.GetAxis ("Mouse Y") * mouseSensitivity;
		
		verticalRotation = Mathf.Clamp (verticalRotation, -lookRange, lookRange);
		
		Camera.main.transform.localRotation = Quaternion.Euler (verticalRotation, 0, 0);

		//USE FOR MOVEMENT
		currRot = Camera.main.transform.rotation.ToEulerAngles ();

		//Animations
		if (!animationList.animation.IsPlaying("ThrowBall")) {
			animationList.animation.Rewind("ThrowBall");
		}
		if (Input.GetButtonDown ("Fire1")) {
			animationList.animation.Play("ThrowBall");
			StartCoroutine("fireDelay");
			/**
			Transform t = Instantiate(snowball, myTransform.position + mainCamera.transform.forward + mainCamera.transform.up + mainCamera.transform.right*.5f, Camera.main.transform.rotation) as Transform;
			GameObject objectClone = t.gameObject;
			objectClone.rigidbody.velocity = mainCamera.transform.forward*15;
			*/
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
