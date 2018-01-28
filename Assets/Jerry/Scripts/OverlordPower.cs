using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverlordPower : MonoBehaviour {
	private PlayerInfo playerInfo;
	public float lifeTime;

	void DestroyMe(){
		playerInfo.Unlock (PlayerInfo.Locks.Movement, gameObject.GetInstanceID());
		playerInfo.Unlock (PlayerInfo.Locks.Pointing, gameObject.GetInstanceID());
		Destroy (gameObject);
	}

	public void Init(PlayerInfo playerInf){
		Quaternion q = new Quaternion();
		q.SetLookRotation (playerInf.currentPointerDir);
		transform.rotation = q;
		playerInfo = playerInf;
		playerInfo.Lock (PlayerInfo.Locks.Movement, gameObject.GetInstanceID());
		playerInfo.Lock (PlayerInfo.Locks.Pointing, gameObject.GetInstanceID());
		Invoke ("DestroyMe", lifeTime);
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.tag == "Player") {
			Destroy (col.transform.parent.gameObject);
		}
	}

}
