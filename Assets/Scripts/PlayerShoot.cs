using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerShoot : MonoBehaviour {

	public GameObject projectile;
	public PhysicMaterial[] pMaterials;
	int curMaterial = 0;
	public int projectileSpeed;
	public int projectileLifetime;
	public float timeBetweenShoot;
	public int clipSize;
	public bool chargedShot;
	public bool firstPerson;
	public int shotsFired = 0;
	GameObject barrel;
	public string playerID;
	float shotTimer;
	float chargeTimer;


	// Use this for initialization
	void Start () {
		curMaterial = 0;
		projectile.gameObject.GetComponent<GroundHitSplatter> ().splatterObject.GetComponent<MeshCollider> ().material = pMaterials [curMaterial];
		shotsFired = 0;
		if (firstPerson) {
			barrel = Camera.main.gameObject;
		} else {
			barrel = transform.FindChild ("Body/Gun/Barrel").gameObject;
		}
		shotTimer = timeBetweenShoot;
		AmmoManager.clipSize = clipSize;
		AmmoManager.ammoLeft = clipSize;
	}
	
	// Update is called once per frame
	void Update () {
		shotTimer += Time.deltaTime;
		if (shotsFired < clipSize) {
			if (chargedShot) {
				if (CrossPlatformInputManager.GetButton (playerID + "Fire2") && shotTimer > timeBetweenShoot) {
					chargeTimer += .5f;
				} else if (CrossPlatformInputManager.GetButtonUp (playerID + "Fire2")) {
					//Debug.Log ("Fire");
					shotsFired += 1;
					AmmoManager.ammoLeft -= 1;
					shotTimer = 0;
					GameObject proj;
					proj = Instantiate (projectile, barrel.transform.position, barrel.transform.rotation);
					proj.GetComponent<Rigidbody> ().velocity = Camera.main.transform.forward * (projectileSpeed + chargeTimer);
					Destroy (proj, projectileLifetime);
					chargeTimer = 0;
				}
			} else {
				if (CrossPlatformInputManager.GetButton (playerID + "Fire2") && shotTimer > timeBetweenShoot) {
					//Debug.Log ("Fire");
					shotsFired += 1;
					AmmoManager.ammoLeft -= 1;
					shotTimer = 0;
					GameObject proj;
					proj = Instantiate (projectile, barrel.transform.position, barrel.transform.rotation);
					proj.GetComponent<Rigidbody> ().velocity = Camera.main.transform.forward * (projectileSpeed * 5);
					Destroy (proj, projectileLifetime);
				}
			}
		}
		if (CrossPlatformInputManager.GetButtonDown (playerID + "Fire3")) {
			//Debug.Log ("Switch Material");
			changeMaterial ();
			Debug.Log(pMaterials[curMaterial]);
		}
	}

	public void reset() {
		shotsFired = 0;
	}

	void changeMaterial() {
		if (curMaterial < pMaterials.Length - 1) {
			curMaterial += 1;
		} else {
			curMaterial = 0;
		}
		projectile.gameObject.GetComponent<GroundHitSplatter> ().splatterObject.GetComponent<MeshCollider> ().material = pMaterials [curMaterial];
	}

	public PhysicMaterial getCurrentMaterial() {
		return pMaterials [curMaterial];
	}
}
