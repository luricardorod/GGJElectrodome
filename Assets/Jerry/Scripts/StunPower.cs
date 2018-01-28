using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunPower : MonoBehaviour {

	public float stunDuration;
	public float speed;

	private Vector3 direction;
	private PlayerInfo playerInfoTarget;

	void Start()
	{
	}

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
		if (other.tag == "Player") {
			playerInfoTarget = other.transform.GetComponent<PlayerInfo> ();
			playerInfoTarget.Lock (PlayerInfo.Locks.Movement, GetInstanceID ());
			GetComponent<SphereCollider> ().enabled = false;
			GetComponent<MeshRenderer> ().enabled = false;
			Invoke ("freePlayer", stunDuration);


		}
	}

	void freePlayer () {
		playerInfoTarget.Unlock(PlayerInfo.Locks.Movement, GetInstanceID ());
		Destroy(gameObject);
	}
}
