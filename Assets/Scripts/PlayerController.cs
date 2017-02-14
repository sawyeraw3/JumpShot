using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour {
	
	// public vars
	public float mouseSensitivityX = 1;
	public float mouseSensitivityY = 1;
	public float minDistanceToGround = 1;
	public float walkSpeed = 6;
	public float jumpForce;
	public string playerID;
	//public string groundTag;
	public bool firstPerson;
	public bool grounded;
	public bool atEnd;
	//bool jumping = true;
	GameObject body;
	
	// System vars
	//bool flying = false;
	Vector3 moveAmount;
	
	//Vector3 targetFlyAmount;
	Rigidbody rigidbody;
	Vector3 smoothMoveVelocity;
	
	void Awake() {
		atEnd = false;
		if (firstPerson)
			body = gameObject;
		else
			body = gameObject.transform.FindChild ("Body").gameObject;
		rigidbody = GetComponent<Rigidbody> ();
		grounded = false;
	}
	
	void Update() {

		if (Time.timeScale == 0)
			return;

		//Jump condition
		if (!grounded) {
			isPlayerOnTop ();
		}
		if (grounded && CrossPlatformInputManager.GetButtonDown (playerID + "Fire1")) {
			rigidbody.velocity = (transform.up * jumpForce);
			grounded = false;
		}

		// Look rotation:
		if (firstPerson) {
			body.transform.Rotate (Vector3.up * CrossPlatformInputManager.GetAxis (playerID + "HorizontalLook") * mouseSensitivityX);
			Camera.main.transform.Rotate (Vector3.left * CrossPlatformInputManager.GetAxis (playerID + "VerticalLook") * mouseSensitivityY);
		} else {
			body.transform.Rotate (Vector3.up * CrossPlatformInputManager.GetAxis (playerID + "HorizontalLook") * mouseSensitivityX);
		}

		// Calculate movement:
		float inputX = CrossPlatformInputManager.GetAxisRaw(playerID + "Horizontal");
		float inputY = CrossPlatformInputManager.GetAxisRaw(playerID + "Vertical");
		
		Vector3 moveDir = new Vector3(inputX,0, inputY);
		if (moveDir.magnitude > 1)
			moveDir = moveDir.normalized;

		Vector3 targetMoveAmount = Vector3.zero;
		/*if(CrossPlatformInputManager.GetButton("LButton")){
			targetMoveAmount = moveDir * runSpeed;
			if(anim)
				anim.SetFloat("Speed", moveDir.magnitude * runSpeed);
		} else {
			targetMoveAmount = moveDir * walkSpeed;
			if(anim)
				anim.SetFloat("Speed", moveDir.magnitude);
		}*/
		targetMoveAmount = moveDir * walkSpeed;
		moveAmount = Vector3.SmoothDamp(moveAmount,targetMoveAmount,ref smoothMoveVelocity,.15f);

		// Apply movement to rigidbody
		Vector3 localMove = transform.TransformDirection(moveAmount) * Time.fixedDeltaTime;
		rigidbody.MovePosition(rigidbody.position + localMove);
	}

	void OnTriggerEnter(Collider other) {
		isPlayerOnTop ();
	}

	void isPlayerOnTop() {
		Ray ray = new Ray(body.transform.position, -body.transform.up);
		RaycastHit hitInfo;

		if (Physics.Raycast (ray, out hitInfo, minDistanceToGround)) {
			grounded = true;
			if (hitInfo.collider.gameObject.tag == "End") {
				atEnd = true;
			}
		}
	}

	/*void OnTriggerEnter(Collider other) {
		//isPlayerGrounded ();
		string otherTag = other.transform.gameObject.tag;
		if (otherTag == groundTag || otherTag == "Start" || otherTag == "End" || otherTag == "Movable") {
			isPlayerOnTop ();
		} else {
			grounded = false;
		}
	}

	void OnTriggerExit(Collider other) {
		jumping = true;
	}

	void isPlayerOnTop() {
		Ray ray = new Ray(body.transform.position, -body.transform.up);
		RaycastHit hitInfo;

		if (Physics.Raycast (ray, out hitInfo, minDistanceToGround)) {
			string otherTag = hitInfo.transform.gameObject.tag;
			if (otherTag == groundTag || otherTag == "Start" || otherTag == "Movable") {
				jumping = false;
				grounded = true;
			} else if (otherTag == "End") {
				//Debug.Log ("On end platform");
				jumping = false;
				grounded = true;
				atEnd = true;
			}
		} else {
			jumping = true;
			grounded = true;
		}
	}*/
}
