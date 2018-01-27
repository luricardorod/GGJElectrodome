using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MOVE_SET
{
    OFFENSIVE,
    DEFFENSIVE,
    MOVE_SET_MAX
}

public class PlayerLogic : MonoBehaviour
{
    public PLAYER m_PlayerNumber;

    MOVE_SET m_ActiveMoveset;
    float m_Energy;

    bool m_Active;

    void Start()
    {
        Live();
    }

    void Live()
    {
        m_Energy = 0.0f;
        m_ActiveMoveset = MOVE_SET.OFFENSIVE;
        m_Active = true;
    }

    void Die()
    {
        m_Active = false;
        GameState.GlobalGameState.PlayerKilled(m_PlayerNumber);
    }
}
