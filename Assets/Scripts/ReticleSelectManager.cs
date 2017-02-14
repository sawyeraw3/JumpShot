using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class ReticleSelectManager : MonoBehaviour {

	public GameObject worldObject;
	public string playerID;
	public int maxSelectDistance;
	public int grabSpeed;
	GameObject grabPoint;

	Camera cam;
	RaycastHit hitInfo;

	// Use this for initialization
	void Start () {
		cam = Camera.main;
		grabPoint = GameObject.Find ("GrabPoint");
	}
	
	// Update is called once per frame
	void Update () {
		if (!worldObject && CrossPlatformInputManager.GetButtonDown (playerID + "Fire4")) {
			getSelectableObject ();
		} else if (worldObject && CrossPlatformInputManager.GetButtonDown (playerID + "Fire4")) {
			worldObject.GetComponent<BoxCollider> ().enabled = true;
			worldObject = null;
		}

		if (worldObject) {
			Vector3 destPos = grabPoint.transform.position;
			worldObject.transform.position = Vector3.Lerp (worldObject.transform.position, destPos, grabSpeed * Time.deltaTime);
		}
	}

	public void getSelectableObject() {
		if (Physics.Raycast (cam.transform.position, cam.transform.forward, out hitInfo, maxSelectDistance)) {
			if (hitInfo.collider.tag == "Movable") {
				worldObject = hitInfo.collider.gameObject;
				worldObject.GetComponent<BoxCollider> ().enabled = false;
			}
		}
	}
}
