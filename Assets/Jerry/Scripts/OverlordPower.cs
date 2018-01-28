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

	public void Init(PlayerInfo playerInf, GameObject Summoner){
        Debug.Log("Overlord summoned by: " + Summoner);
        Summoner.GetComponent<AudioManager>().PlaySoundEffect(AudioManager.SOUND_EFFECT.LASER, 3.1f);
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
            playerInfo.isAlive = false;
			//Destroy (col.transform.parent.gameObject);
		}
	}

}
