using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunPower : MonoBehaviour {

	public float stunDuration;
	public float speed;

    public GameObject StunEffect;

	private Vector3 direction;
	private PlayerInfo playerInfoTarget;


	public void Init(Vector3 dir)
	{
		direction = dir;
	}

	void Update()
	{
		transform.position = transform.position + (direction * speed * Time.deltaTime);
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
        {
			playerInfoTarget = other.transform.GetComponent<PlayerInfo> ();
			playerInfoTarget.Lock (PlayerInfo.Locks.Movement, GetInstanceID ());
			GetComponent<SphereCollider> ().enabled = false;
			GetComponent<MeshRenderer> ().enabled = false;
            GetComponentInChildren<TrailRenderer>().enabled = false;
			Invoke ("freePlayer", stunDuration);
            Instantiate(StunEffect, other.transform.position, Quaternion.Euler(-90, 0, 0));
            AudioManager.GlobalAudioManager.PlaySoundEffect(AudioManager.SOUND_EFFECT.DAMAGE, 0.5f);

        }

        if (other.tag == "Barrier")
        {
            GetComponent<SphereCollider>().enabled = false;
            GetComponent<MeshRenderer>().enabled = false;
            GetComponentInChildren<TrailRenderer>().enabled = false;
            Instantiate(StunEffect, other.transform.position, Quaternion.Euler(-90, 0, 0));
            AudioManager.GlobalAudioManager.PlaySoundEffect(AudioManager.SOUND_EFFECT.DAMAGE, 0.5f);

        }
    }

	void freePlayer () {
		playerInfoTarget.Unlock(PlayerInfo.Locks.Movement, GetInstanceID ());
		Destroy(gameObject);
	}
}
