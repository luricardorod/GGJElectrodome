using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slide : MonoBehaviour
{

    public float boostSpeed = 1.5f;
    public float fLiveTime;
    public int IDLanzador;

    void Update()
    {
        fLiveTime -= Time.deltaTime;
        if (fLiveTime < 0)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player")
        {
            if (collider.gameObject.GetInstanceID() != IDLanzador)
            {
                PlayerInfo playerInfo = collider.gameObject.GetComponentInParent<PlayerInfo>();
                playerInfo.Lock(PlayerInfo.Locks.MovementControl, GetInstanceID());
                playerInfo.LockSpeedBoost(2.5f, GetInstanceID());
            }
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.tag == "Player")
        {
            if (collider.gameObject.GetInstanceID() != IDLanzador)
            {

                PlayerInfo playerInfo = collider.gameObject.GetComponentInParent<PlayerInfo>();
                playerInfo.Unlock(PlayerInfo.Locks.MovementControl, GetInstanceID());
                playerInfo.UnlockSpeedBoost(GetInstanceID());
            }
        }
    }
}
