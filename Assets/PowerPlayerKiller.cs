using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerPlayerKiller : MonoBehaviour
{
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Player")
        {
            GameState.GlobalGameState.PlayerKilled(col.gameObject.GetComponent<PlayerInfo>().number);
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            GameState.GlobalGameState.PlayerKilled(col.gameObject.GetComponent<PlayerInfo>().number);
        }
    }

}
