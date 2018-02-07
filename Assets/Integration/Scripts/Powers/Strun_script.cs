using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Strun_script : MonoBehaviour
{
    public float stunDuration;
    public float speed;
	private Vector3 direction;
	private PlayerInfo playerInfoTarget;
    // Use this for initialization
    void Start()
    {
		direction = Vector3.zero;
    }

	void Init(Vector3 dir)
	{
		direction = dir;
	}

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
		if (other.tag == "Player") {
			playerInfoTarget = other.transform.GetComponent<PlayerInfo> ();
			playerInfoTarget.Lock (PlayerInfo.Locks.Movement, GetInstanceID ());
            //Invoke (freePlayer, stunDuration);
            AudioManager.GlobalAudioManager.PlaySoundEffect(AudioManager.SOUND_EFFECT.STUN_HIT, 0.5f);

        }
    }

	void freePlayer () {
		playerInfoTarget.Unlock(PlayerInfo.Locks.Movement, GetInstanceID ());
		Destroy(gameObject);
	}
		
}
