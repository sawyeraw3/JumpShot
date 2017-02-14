using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBetweenPoints : MonoBehaviour {


	public Transform destinationOne;
	public Transform destinationTwo;
	public float moveSpeed;
	bool reverseDir = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.transform.position = Vector3.Lerp (destinationOne.transform.position, destinationTwo.transform.position, (Mathf.Sin (moveSpeed * Time.time) + 1.0f) / 2.0f);
		/*if (reverseDir) {
			gameObject.transform.position = Vector3.Lerp (gameObject.transform.position, destinationTwo.transform.position, 2 * Time.deltaTime);
		} else {
			gameObject.transform.position = Vector3.Lerp (gameObject.transform.position, destinationOne.transform.position, 2 * Time.deltaTime);
		}
		if (gameObject.transform.position == destinationOne.transform.position || gameObject.transform.position == destinationTwo.transform.position) {
			reverseDir = !reverseDir;
		}*/
	}
}
