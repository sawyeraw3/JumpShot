using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {

	PlayerController playerController;
	Vector3 spawnPoint;
	float wSpeed;
	public bool dead = false;

	// Use this for initialization
	void Start () {
		dead = false;
		spawnPoint = GameObject.Find ("SpawnPoint").transform.position;
		playerController = gameObject.GetComponent<PlayerController> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.y < 0){ // -1 || !playerController.grounded || playerController.atEnd) {
			//Debug.Log ("Dead");
			playerController.enabled = false;
			gameObject.GetComponent<PlayerShoot> ().enabled = false;
			dead = true;
		}
	}

	void destroyAllPlatforms() {
		GameObject[] platforms = GameObject.FindGameObjectsWithTag ("Platform");
		foreach (GameObject go in platforms) {
			Destroy (go);
		}
		//Debug.Log ("Platforms destroyed");
	}

}
