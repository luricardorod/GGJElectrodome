using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slide : MonoBehaviour {

	public float boostSpeed = 1.5f;
	public float fLiveTime;
	PlayerLogic pl;
	float maxSpeed;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		fLiveTime -= Time.deltaTime;
		if (fLiveTime < 0) {
			Destroy (gameObject);
		}
	}

	void OnTriggerEnter(Collider collider) {
		if (collider.tag == "Player") {
			if (!pl) {
				pl = collider.gameObject.GetComponentInParent<PlayerLogic> ();
			}

			maxSpeed = pl.fMaxSpeed;
			pl.fMaxSpeed *= boostSpeed;
			pl.lockDirection = true;
		}
	}

	void OnTriggerExit(Collider collider) {
		if (collider.tag == "Player") {
			pl.fMaxSpeed = maxSpeed;
			pl.lockDirection = false;
		}
	}
}
