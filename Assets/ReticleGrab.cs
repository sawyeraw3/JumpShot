﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class ReticleGrab : MonoBehaviour {

	ReticleSelectManager reticleSelectManager;
	public string playerID;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (CrossPlatformInputManager.GetButtonDown (playerID + "Fire4")) {
		}
	}
}
