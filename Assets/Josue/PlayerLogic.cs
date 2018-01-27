using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MOVE_SET
{
    OFFENSIVE,
    DEFFENSIVE,
    MOVE_SET_MAX
}

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

public class PlayerLogic : MonoBehaviour
{
    public PLAYER m_PlayerNumber;

    MOVE_SET m_ActiveMoveset;
    PlayerMovement playerMovScript;

    bool m_Active;

    public GameObject[] PowerPrefabs;

    void Start()
    {
        Live();
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

    public void LaunchPower(POWER powerToFire)
    {
        //Lanzar PowerPrefab[(int)powerToFire];
    }
}
