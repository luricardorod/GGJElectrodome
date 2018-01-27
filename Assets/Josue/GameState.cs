using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GAME_STATE
{
    MENU,
    PRE_ROUND,
    PLAYING,
    PLAYER_DEATH,
    ROUND_OVER,
    MATCH_OVER
};

public enum PLAYER
{
    PLAYER_1,
    PLAYER_2,
    PLAYER_3,
    PLAYER_4,
    PLAYER_MAX
};

public class GameState : MonoBehaviour
{
    public static GameState GlobalGameState;

    public GAME_STATE m_State;

    public GameObject playerPrefab;
    GameObject[] m_PlayerPool;

    uint[] m_PlayerScore;
    bool[] m_PlayerIsAlive;

    uint m_StartingPlayers;
    uint m_NumberOfPlayersAlive;

    void Awake()
    {
        DontDestroyOnLoad(this);
        GlobalGameState = this;

        m_PlayerScore = new uint[(int)PLAYER.PLAYER_MAX];
        m_PlayerIsAlive = new bool[(int)PLAYER.PLAYER_MAX];

        m_PlayerPool = new GameObject[(int)PLAYER.PLAYER_MAX];

        for (int i = 0; i < (int)PLAYER.PLAYER_MAX; ++i)
        {
            m_PlayerPool[i] = Instantiate<GameObject>(playerPrefab);
            m_PlayerPool[i].GetComponent<PlayerLogic>().m_PlayerNumber = (PLAYER)i;
        }
    }

    public void InitMatch(PLAYER playerNumber)
    {
        m_StartingPlayers = (uint)playerNumber + 1;

        InitRound();
    }

    public void InitRound()
    {
        m_NumberOfPlayersAlive = m_StartingPlayers;
        m_State = GAME_STATE.PRE_ROUND;

        for (int i = 0; i < m_NumberOfPlayersAlive; ++i)
            m_PlayerIsAlive[i] = true;
    }

    void CheckForRoundEnd()
    {
        //If there is just one player left.
        if (m_NumberOfPlayersAlive == 1)
        {
            //Look for the player still alive.
            for (int playerAliveIndex = 0; playerAliveIndex < (int)PLAYER.PLAYER_MAX; ++playerAliveIndex)
            {
                if (m_PlayerIsAlive[playerAliveIndex])
                {
                    //Increase his score.
                    ++m_PlayerScore[playerAliveIndex];

                    //Change the state to round over.
                    m_State = GAME_STATE.ROUND_OVER;

                    return;
                }
            }
        }
    }

    /// <summary>
    /// A function to call when a player gets killed. Receives the player killed.
    /// </summary>
    /// <param name="playerKilled"></param>
    public void PlayerKilled(PLAYER playerKilled)
    {
        //Set the playerIsAlive bool to false
        m_PlayerIsAlive[(int)playerKilled] = false;

        //Decrease the number of players alive.
        --m_NumberOfPlayersAlive;

        //Check to see if there is just one player left and the round should end.
        CheckForRoundEnd();
    }
}
