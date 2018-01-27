using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MOVE_SET
{
    OFFENSIVE,
    DEFFENSIVE,
    MOVE_SET_MAX
}

<<<<<<< HEAD
public enum POWER
{
    DASH,
    PARRY,
    BARRIER,
    STUN,
    CHAINED,
    BOMB,
    OVERLORD,
    SLIDE,
    POWER_MAX
}
=======
>>>>>>> 9204ccc05337f8fd311fa855b2b1c532763dca53

public class PlayerLogic : MonoBehaviour
{
    public PLAYER m_PlayerNumber;
    public GameObject TrailPrefab; 

    MOVE_SET m_ActiveMoveset;
    PlayerMovement playerMovScript;

    bool m_Active;

    public GameObject[] PowerPrefabs;

    void Start()
    {
        Live();
<<<<<<< HEAD
        playerMovScript = GetComponent<PlayerMovement>();
    }

    void CheckInput(PLAYER playerInput)
    {
        //TODO
        //Disparar Cosas. Llamar LaunchPower
        //playerMovScript.fEnergy para sacar el Energy float.
    }

    void Update()
    {
        CheckInput(m_PlayerNumber);
=======
        SetColor();
>>>>>>> 9204ccc05337f8fd311fa855b2b1c532763dca53
    }

    void Live()
    {
        playerMovScript.fEnergy = 0.0f;
        m_ActiveMoveset = MOVE_SET.OFFENSIVE;
        m_Active = true;
    }

    void Die()
    {
        m_Active = false;
        GameState.GlobalGameState.PlayerKilled(m_PlayerNumber);
    }

<<<<<<< HEAD
    public void LaunchPower(POWER powerToFire)
    {
        //Lanzar PowerPrefab[(int)powerToFire];
=======
    void SetColor() {
        var player = gameObject.GetComponent<PlayerLogic>().m_PlayerNumber;
        Debug.Log(player);
        switch (player) {
            case PLAYER.PLAYER_1:
                TrailPrefab.GetComponent<TrailRenderer>().materials[0].color = Color.red;
                break;
            case PLAYER.PLAYER_2:
                TrailPrefab.GetComponent<TrailRenderer>().materials[0].color = Color.blue;
                break;
            case PLAYER.PLAYER_3:
                TrailPrefab.GetComponent<TrailRenderer>().materials[0].color = Color.yellow;
                break;
            case PLAYER.PLAYER_4:
                TrailPrefab.GetComponent<TrailRenderer>().materials[0].color = Color.green;
                break;
            default:
                break;
        }
>>>>>>> 9204ccc05337f8fd311fa855b2b1c532763dca53
    }
}
