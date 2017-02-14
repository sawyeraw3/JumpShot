using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour {

	public static float time = 0;

	Text timeText;

	// Use this for initialization
	void Awake () {
		timeText = GameObject.Find ("TimeText").GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		timeText.text = "Time: " + time.ToString ("F2");
	}
}
