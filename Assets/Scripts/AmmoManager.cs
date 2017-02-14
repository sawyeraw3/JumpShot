using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoManager : MonoBehaviour {

	public static int ammoLeft = 0;
	public static int clipSize = 0;

	GameObject player;
	PhysicMaterial pMaterial;
	Text ammoText;
	Text matText;

	// Use this for initialization
	void Awake () {
		player = GameObject.FindGameObjectWithTag ("Player");
		pMaterial = player.GetComponent<PlayerShoot> ().getCurrentMaterial ();
		ammoText = GameObject.Find ("AmmoText").GetComponent<Text>();
		matText = GameObject.Find ("MaterialText").GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		pMaterial = player.GetComponent<PlayerShoot> ().getCurrentMaterial ();
		ammoText.text = "Shots: " + ammoLeft;
		matText.text = "Material " + pMaterial.name;
	}
}
