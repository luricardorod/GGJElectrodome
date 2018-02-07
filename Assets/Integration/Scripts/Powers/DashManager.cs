using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashManager : MonoBehaviour
{
    public float fVecinity = 6f;
    private Vector3 MovementDir;
    public float fTimeHandicap = .25f;
    private float fTimePassed = 0f;
    private bool StartHandicap = false;
    private List<GameObject> PushedPlayers=new List<GameObject>() ;

    public void Dash(float DashDistance)
    {
        MovementDir = GetComponent<PlayerInfo>().currentPointerDir;
        transform.position += MovementDir * DashDistance;
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY;
        StartHandicap = true;

        GameObject[] OtherPlayers = GameObject.FindGameObjectsWithTag("PlayerWrapper");
        foreach (GameObject Player in OtherPlayers)
        {
            if (Player != transform.parent.gameObject)
            {
                if ((Player.transform.GetChild(0).position - transform.position).magnitude < fVecinity)
                {
                    PushedPlayers.Add(Player);
                    Player.transform.GetChild(0).GetComponent<PlayerInfo>().Lock(PlayerInfo.Locks.MovementControl, gameObject.GetInstanceID());
                    Player.transform.GetChild(0).GetComponent<PlayerInfo>().currentMovementDir = MovementDir;
                    Player.transform.GetChild(0).GetComponent<PlayerInfo>().LockSpeedBoost(6.0f, gameObject.GetInstanceID());
                    Invoke("UnlockPPlayers", .25f);
                }
            }

        }
    }
    private void UnlockPPlayers()
    {
        foreach(GameObject Player in PushedPlayers)
        {
            Player.transform.GetChild(0).GetComponent<PlayerInfo>().Unlock(PlayerInfo.Locks.MovementControl, gameObject.GetInstanceID());
            Player.transform.GetChild(0).GetComponent<PlayerInfo>().UnlockSpeedBoost(gameObject.GetInstanceID());
        }
        PushedPlayers.Clear();
    }

    private void Update()
    {
        if(StartHandicap)
        {
            fTimePassed += Time.deltaTime;
            if(fTimePassed>fTimeHandicap)
            {
                GetComponent<Rigidbody>().constraints &= ~RigidbodyConstraints.FreezePositionY;
                Destroy(this);
            }
        }
    }
}
