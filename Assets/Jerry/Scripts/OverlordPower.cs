using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverlordPower : MonoBehaviour {
	private PlayerLogic pl;
	public float lifeTime;
	// Use this for initialization
	void Start () {
	}
	
	void DestroyMe(){
		pl.lockDirection = false;
		pl.lockMovement = false;
		Destroy (gameObject);
	}

	public void SetPlayerLogic(PlayerLogic plo){
		pl = plo;
		Invoke ("DestroyMe", lifeTime);
	}
	// Update is called once per frame
}
