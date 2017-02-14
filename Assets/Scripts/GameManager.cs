using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public float respawnDelay;
	GameObject player;
	PlayerController playerController;
	PlayerHealth playerHealh;
	public static GameObject winText;
	public static GameObject deathText;
	public static GameObject materialText;
	public static float timeElapsed;
	float respawnTimer = 0f;

	// Use this for initialization
	void Start () {
		timeElapsed = 0f;
		winText = GameObject.Find ("WinText");
		deathText = GameObject.Find ("DeathText");
		materialText = GameObject.Find ("MaterialText");
		winText.SetActive (false);
		deathText.SetActive (false);
		player = GameObject.FindGameObjectWithTag ("Player");
		playerHealh = player.GetComponent<PlayerHealth> ();
		playerController = player.GetComponent<PlayerController> ();
	}
	
	// Update is called once per frame
	void Update () {

		if (!playerHealh.dead && !playerController.atEnd) {
			timeElapsed += Time.deltaTime;
			TimeManager.time = timeElapsed;
		}

		if (playerController.atEnd) {
			materialText.SetActive (false);
			winText.SetActive (true);
			winText.transform.FindChild ("ElapsedTime").gameObject.GetComponentInChildren<Text> ().text = "Time Elapsed: " + timeElapsed.ToString ("F2");
			respawnTimer += Time.deltaTime;
			TimeManager.time = respawnTimer;
			if (respawnTimer > respawnDelay) {
				restartLevel ();
				//Debug.Log ("Respawned");
			}
			//Debug.Log ("Display Win Text");
		} else if (playerHealh.dead) {
			deathText.SetActive (true);
			respawnTimer += Time.deltaTime;
			TimeManager.time = respawnTimer;
			if (respawnTimer > respawnDelay) {
				restartLevel ();
				//Debug.Log ("Respawned");
			}
		}

		/*
		if (!playerHealh.dead) {
			timeElapsed += Time.deltaTime;
			TimeManager.time = timeElapsed;
		} else if (playerController.atEnd && playerHealh.dead) {
			winText.SetActive (true);
			winText.transform.FindChild ("ElapsedTime").gameObject.GetComponentInChildren<Text> ().text = "Time Elapsed: " + timeElapsed.ToString ("F2");
			//Debug.Log ("Display Win Text");
		} else if (!playerController.atEnd && playerHealh.dead) {
			deathText.SetActive (true);
			respawnTimer += Time.deltaTime;
			TimeManager.time = respawnTimer;
			if (respawnTimer > respawnDelay) {
				restartLevel ();
				//Debug.Log ("Respawned");
			}
		}*/
	}

	void restartLevel() {
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
	}
}
