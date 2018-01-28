using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainPower : MonoBehaviour {

	public float speed;
	public float lengthChain;
	private Vector3 direction;
	private Transform transformTarget;
	private Vector3 initPosition;
	private float distanceCollision;
	private bool collisionFlag;
	private bool pullFlag;


	public void Init(Vector3 dir, Vector3 shootPosition)
	{
		direction = dir;
		initPosition = shootPosition;
		collisionFlag = false;
		pullFlag = false;
	}

	void Update()
	{
		if (!collisionFlag) {
			transform.position = transform.position + (direction * speed * Time.deltaTime);
			if ((transform.position - initPosition).magnitude > lengthChain) {
				Destroy (gameObject);	
			}
		}
		else {
			transform.position = transformTarget.position;
			Vector3 dirToCenter = (initPosition - transformTarget.position);
			float diffDistance = dirToCenter.magnitude;
			if (distanceCollision < diffDistance) {
				dirToCenter.Normalize ();
				PlayerInfo pI = transformTarget.GetComponent<PlayerInfo> ();
				pI.currentMovementDir = dirToCenter;
				pI.Lock (PlayerInfo.Locks.MovementControl, GetInstanceID ());
				pI.LockSpeedBoost ((diffDistance - distanceCollision) * 100, GetInstanceID ());
				pullFlag = true;
			}
		}
		if (pullFlag) {
			PlayerInfo pI = transformTarget.GetComponent<PlayerInfo> ();
			pullFlag = false;
			pI.Unlock (PlayerInfo.Locks.MovementControl, GetInstanceID ());
			pI.UnlockSpeedBoost (GetInstanceID ());
		}

	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player") {
			transformTarget = other.transform;
			distanceCollision = (transformTarget.position - initPosition).magnitude;
			GetComponent<SphereCollider> ().enabled = false;
			collisionFlag = true;
		}
	}


}
