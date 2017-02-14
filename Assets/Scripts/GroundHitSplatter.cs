using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundHitSplatter : MonoBehaviour {

	public GameObject splatterObject;
	public string groundTag;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == groundTag) {
			if (other.gameObject.transform.up == new Vector3(1.0f, 0.0f, 0.0f)) {
				Vector3 spawnPos = new Vector3 (gameObject.transform.position.x - 1.5f, gameObject.transform.position.y, gameObject.transform.position.z);
				spawnPlatform (spawnPos);
			} else if (other.gameObject.transform.up == new Vector3(0.0f, 1.0f, 0.0f)) {
				Vector3 spawnPos = new Vector3 (gameObject.transform.position.x, .5f, gameObject.transform.position.z);
				spawnPlatform (spawnPos);
			} else if (other.gameObject.transform.up == new Vector3(-1.0f, 0.0f, 0.0f)) {
				Vector3 spawnPos = new Vector3 (gameObject.transform.position.x + 1.5f, gameObject.transform.position.y, gameObject.transform.position.z);
				spawnPlatform (spawnPos);
			}
		}
	}

	void spawnPlatform(Vector3 pos) {
		GameObject splatter;
		splatter = Instantiate (splatterObject, pos, Quaternion.identity);
		Destroy (gameObject);
	}
}
