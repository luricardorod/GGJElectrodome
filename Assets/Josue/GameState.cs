using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GAME_STATE
{
    MENU = 0,
    PRE_ROUND,
    PLAYING,
    PLAYER_DEATH,
    ROUND_OVER,
    MATCH_OVER
};

public enum GAME_TYPE
{
   DEATH_MATCH = 0,
   PARTY
};

public enum PLAYER
{
    PLAYER_1 = 0,
    PLAYER_2,
    PLAYER_3,
    PLAYER_4,
    PLAYER_MAX
};

public class GameState : MonoBehaviour
{
    Color[] colores = new Color[4];
    private int LastLevel=-1;
    public static GameState GlobalGameState;
    public int MaxScore=5;
    public GAME_STATE m_State;
    private float fTimeToWait;
    public GameObject playerPrefab;
    public GameObject[] m_PlayerPool;
    private GameObject Level;
    public uint[] m_PlayerScore;
    public bool[] m_PlayerIsAlive;
    public GameObject[] m_PlayerPrefab;
    uint m_StartingPlayers;
    public uint m_NumberOfPlayersAlive;

    private void Start()
    {
        colores[0] = Color.red;
        colores[1] = Color.green;
        colores[2] = Color.yellow;
        colores[3] = Color.magenta;
    }
    void Awake()
    {
        DontDestroyOnLoad(this);
        GlobalGameState = this;
        ///
        m_PlayerScore = new uint[(int)PLAYER.PLAYER_MAX];
        m_PlayerIsAlive = new bool[(int)PLAYER.PLAYER_MAX];
        m_PlayerPool = new GameObject[(int)PLAYER.PLAYER_MAX];
        ///
        for (int i = 0; i < (int)PLAYER.PLAYER_MAX; ++i)
        {
            m_PlayerPool[i] = Instantiate<GameObject>(m_PlayerPrefab[i]);
            //m_PlayerPool[i].GetComponent<PlayerLogic>().m_PlayerNumber = (PLAYER)i;
            m_PlayerPool[i].transform.GetChild(0).GetComponent<PlayerInfo>().Init(i, colores[i],30);
            //m_PlayerPool[i].gameObject.SetActive(false);
        }

        //TODO: remove
        InitMatch(PLAYER.PLAYER_MAX);
    }

    public void InitMatch(PLAYER playerNumber)
    {
        m_StartingPlayers = (uint)playerNumber;
        for (int i = 0; i < 4; ++i) m_PlayerScore[i] = 0;
        InitRound();
    }

    public void InitRound()
    {

       m_NumberOfPlayersAlive = m_StartingPlayers;
       m_State = GAME_STATE.PRE_ROUND;
       Camera.main.GetComponent<MusicManager>().PlayMusic(MusicManager.SONG.GAME);
        Debug.Log("Playing Level Game Music");
       for (int i = 0; i < 4; ++i) m_PlayerIsAlive[i] = true;
       int randlevel = Random.Range(0, GetComponent<LevelLoader>().Levels.Count-1);
       while (randlevel == LastLevel)
       {
           randlevel = Random.Range(0, GetComponent<LevelLoader>().Levels.Count - 1);
       }
       LastLevel=randlevel;
       GetComponent<LevelLoader>().SetLevel(randlevel);
       Level = GameObject.FindGameObjectWithTag("Level");
       int spawnindex = 0;
       foreach (GameObject Player in m_PlayerPool)
       {
           Player.transform.position = Level.transform.GetChild(spawnindex).transform.position;
           Player.transform.rotation = Level.transform.GetChild(spawnindex).transform.rotation;
           Player.transform.localScale = Level.transform.GetChild(spawnindex).transform.localScale;
           Player.gameObject.SetActive(true);
           Player.transform.GetChild(0).GetComponent<InputManager>().enabled = false;
           ++spawnindex;
       }
       Invoke("WaitToMove", 3);
    }

    void WaitToMove()
    {
        foreach (GameObject Player in m_PlayerPool)
        {
            Player.transform.GetChild(0).GetComponent<InputManager>().enabled = true;
        }
        m_State = GAME_STATE.PLAYING;
    }

    void Update()
    {
        if (m_State == GAME_STATE.PLAYING)
        {
            CheckForRoundEnd();
        }
        else if (m_State == GAME_STATE.ROUND_OVER)
        {
            foreach(uint Score in m_PlayerScore)
            {
                if(Score>=MaxScore)
                {
                    m_State= GAME_STATE.MATCH_OVER;
                }
            }

        }
        else if (m_State == GAME_STATE.MATCH_OVER)
        {
            m_State = GAME_STATE.MENU;

        }
    }

    void CheckForRoundEnd()
    {
        for(int index=0;index<4;++index)
        {
            if(!m_PlayerPool[index].transform.GetChild(0).GetComponent<PlayerInfo>().isAlive)
             {
                 if (m_PlayerPool[index].transform.GetChild(0).GetComponent<PlayerInfo>().lives > 0)
                 {
                     m_PlayerPool[index].transform.GetChild(0).GetComponent<Rigidbody>().isKinematic = true;
                     m_PlayerPool[index].transform.position = Level.transform.GetChild(index).transform.position;
                     m_PlayerPool[index].transform.rotation = Level.transform.GetChild(index).transform.rotation;
                     m_PlayerPool[index].transform.localScale = Level.transform.GetChild(index).transform.localScale;
                     --m_PlayerPool[index].transform.GetChild(0).GetComponent<PlayerInfo>().lives;
                     m_PlayerPool[index].transform.GetChild(0).GetComponent<Rigidbody>().isKinematic = false;
                 }
        }
     }

        //Fuck you tho
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
                    

                    CheckForMatchEnd();
                }
            }
        }
    }

    void CheckForMatchEnd()
    {
        foreach(int score in m_PlayerScore)
        {
            if(score>=5)
            {
                m_State = GAME_STATE.MATCH_OVER;
                Camera.main.GetComponent<MusicManager>().PlayMusic(MusicManager.SONG.GAME);
                Debug.Log("Playing Menu Game Music");
                GameObject[] TrashLevel = GameObject.FindGameObjectsWithTag("Level");
                foreach (GameObject Level in TrashLevel)
                {
                    Destroy(Level);
                }
                GameObject[] TrashCharacters = GameObject.FindGameObjectsWithTag("Player wrapper");
                foreach (GameObject Character in TrashCharacters)
                {
                    Destroy(Character);
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
