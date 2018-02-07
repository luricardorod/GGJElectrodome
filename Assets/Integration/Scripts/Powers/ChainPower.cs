using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainPower : MonoBehaviour {

	public float speed;
	public float lengthChain;
    public GameObject Ball;
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

    void Destroyed()
    {
        PlayerInfo pI = transformTarget.GetComponent<PlayerInfo>();
        pI.Unlock(PlayerInfo.Locks.MovementControl, GetInstanceID());
        pI.UnlockSpeedBoost(GetInstanceID());
    }

	void Update()
	{
        if (pullFlag)
        {
            PlayerInfo pI = transformTarget.GetComponent<PlayerInfo>();
            pullFlag = false;
            pI.Unlock(PlayerInfo.Locks.MovementControl, GetInstanceID());
            pI.UnlockSpeedBoost(GetInstanceID());
        }

        if (!collisionFlag)
        {
			transform.position = transform.position + (direction * speed * Time.deltaTime);
			if ((transform.position - initPosition).magnitude > lengthChain) {
				Destroy (gameObject);	
			}
		}
		else
        {
			transform.position = transformTarget.position;

			Vector3 dirToCenter = (initPosition - transformTarget.position);

			float diffDistance = dirToCenter.magnitude;

			if (distanceCollision < diffDistance)
            {
				dirToCenter.Normalize();

				PlayerInfo pI = transformTarget.GetComponent<PlayerInfo> ();

                if (pI.transform.position.y < initPosition.y)
                {
                    Destroyed();
                    return;
                }

                pI.currentMovementDir = dirToCenter;
				pI.Lock (PlayerInfo.Locks.MovementControl, GetInstanceID ());
				pI.LockSpeedBoost ((diffDistance - distanceCollision) * 10, GetInstanceID ());
				pullFlag = true;
			}
		}
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            transformTarget = other.transform;

            distanceCollision = (transformTarget.position - initPosition).magnitude;

            GameObject bola = Instantiate(Ball, initPosition, Quaternion.identity);
            bola.transform.localScale = new Vector3(distanceCollision * 2, 2, distanceCollision * 2);

            Invoke("Destroyed", 5.0f);
            Destroy(bola, 5.0f);
            Destroy(this.gameObject, 5.0f);

            GetComponent<SphereCollider>().enabled = false;
            collisionFlag = true;

            AudioManager.GlobalAudioManager.PlaySoundEffect(AudioManager.SOUND_EFFECT.DAMAGE, 0.5f);

        }

        if (other.tag == "Barrier")
        {
            Destroy(this.gameObject);
        }
    }


}
