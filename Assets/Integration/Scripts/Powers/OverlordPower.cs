using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverlordPower : MonoBehaviour {
	private PlayerInfo playerInfo;
	public float lifeTime;

	void OnDestroy()
    {
		playerInfo.Unlock (PlayerInfo.Locks.Movement, gameObject.GetInstanceID());
		playerInfo.Unlock (PlayerInfo.Locks.Pointing, gameObject.GetInstanceID());
	}

	public void Init(PlayerInfo playerInf, GameObject Summoner)
    {

        Quaternion q = new Quaternion();
		q.SetLookRotation (playerInf.currentPointerDir);
		transform.rotation = q;
		playerInfo = playerInf;
		playerInfo.Lock (PlayerInfo.Locks.Movement, gameObject.GetInstanceID());
		playerInfo.Lock (PlayerInfo.Locks.Pointing, gameObject.GetInstanceID());
		Destroy(this.gameObject, lifeTime);
	}

}
