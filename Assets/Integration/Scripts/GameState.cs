using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GAME_STATE
{
    MENU = 0,
    PRE_ROUND,
    PLAYING,
    PLAYER_DEATH,
    ROUND_OVER,
    MATCH_OVER,
    MATCH_START
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
    public static GameState GlobalGameState;

    public LevelLoader LoaderLevel;

    public GAME_STATE m_State;

    public GameObject playerPrefab;

    public GameObject[] PlayerPool;

    public GameObject[] PlayerArtBody;
    public GameObject[] PlayerArtPointer;
    public GameObject[] PlayerArtEffects;
    public GameObject[] PlayerArtExplosion;

    public bool[] PlayerActiveInMatch;

    public int MaxScore = 5;
    public uint[] PlayerScore;
    public float MaxPlayerSpeed;

    public uint m_NumberOfPlayersAlive;
    public bool[] PlayerIsAlive;

    private int LastLevel = -1;
    private GameObject Level;

    private uint m_StartingPlayers;
    private float fTimeToWait;

    void Awake()
    {
        DontDestroyOnLoad(this);
        GlobalGameState = this;
        ///
        PlayerScore = new uint[(int)PLAYER.PLAYER_MAX];
        PlayerIsAlive = new bool[(int)PLAYER.PLAYER_MAX];
        PlayerPool = new GameObject[(int)PLAYER.PLAYER_MAX];
        PlayerActiveInMatch = new bool[(int)PLAYER.PLAYER_MAX];
        ///
        int playersActive = 0;

        for (int i = 0; i < (int)PLAYER.PLAYER_MAX; ++i)
        {
            if (!PlayerPool[i])
            {
                PlayerPool[i] = Instantiate<GameObject>(playerPrefab);
                PlayerPool[i].GetComponent<PlayerInit>().CreatePlayer((PLAYER)i);
                PlayerPool[i].SetActive(false);

                if (PlayerActiveInMatch[i])
                {
                    ++playersActive;
                }
            }
        }

        LoaderLevel = GetComponent<LevelLoader>();
    }

    public void JoinPlayer(int player)
    {
        PlayerActiveInMatch[player - 1] = true;
    }

    public void DropPlayer(int player)
    {
        PlayerActiveInMatch[player - 1] = false;
    }

    public void InitMatch(int playerNumber)
    {
        m_StartingPlayers = (uint)playerNumber;
        MusicManager.globalMusicManager.PlayMusic(MusicManager.SONG.GAME);

        for (int i = 0; i < (int)PLAYER.PLAYER_MAX; ++i)
        {
            if (PlayerActiveInMatch[i])
            {
                PlayerPool[i].SetActive(true);
            }

            PlayerScore[i] = 0;
        }
    }

    public void InitRound()
    {
        //Set the number of players alive to the match players
        m_NumberOfPlayersAlive = m_StartingPlayers;
        //Before round.
        m_State = GAME_STATE.PRE_ROUND;
        //Set the default time scale
        Time.timeScale = 1.0f;


        for (int i = 0; i < 4; ++i)
        {
            if (PlayerActiveInMatch[i])
                PlayerIsAlive[i] = true;
        }

        //Choose a random level, if it is the same one as last time. Pick again.
        int randlevel = Random.Range(0, LoaderLevel.Levels.Count - 1);
        while (randlevel == LastLevel)
        {
            randlevel = Random.Range(0, LoaderLevel.Levels.Count - 1);
        }
        LastLevel = randlevel;
        //Load the level.
        LoaderLevel.SetLevel(randlevel);

        //Get the level from the scene graph
        Level = GameObject.FindGameObjectWithTag("Level");

        //Set the corresponding player to the its spawn point in the level.
        int spawnindex = 0;
        for (int i = 0; i < (int)PLAYER.PLAYER_MAX; ++i)
        {
            if (PlayerActiveInMatch[i])
            {
                GameObject Player = PlayerPool[i];
                Player.transform.GetChild((int)PlayerSubObjects.BODY).position = Level.transform.GetChild(spawnindex).transform.position;
                Player.transform.GetChild((int)PlayerSubObjects.BODY).rotation = Level.transform.GetChild(spawnindex).transform.rotation;
                Player.transform.GetChild((int)PlayerSubObjects.BODY).localScale = Level.transform.GetChild(spawnindex).transform.localScale;
                Player.gameObject.SetActive(true);
                Transform Body = Player.transform.GetChild(0);
                Body.GetComponent<InputManager>().enabled = false;
                Body.GetComponent<PlayerInfo>().energy = 0.0f;

                Rigidbody rigid = Body.GetComponent<Rigidbody>();
                rigid.velocity.Set(0, 0, 0);
                rigid.rotation = Quaternion.identity;
            }
            ++spawnindex;
        }

        Invoke("WaitToMove", 4);
    }

    void WaitToMove()
    {
        for (int i = 0; i < (int)PLAYER.PLAYER_MAX; ++i)
        {
            if (PlayerActiveInMatch[i])
            {
                PlayerPool[i].transform.GetChild(0).GetComponent<InputManager>().enabled = true;
            }
        }

        m_State = GAME_STATE.PLAYING;
    }

    void Update()
    {
        if (m_State == GAME_STATE.PLAYING)
        {
        }
        else if (m_State == GAME_STATE.ROUND_OVER)
        {
            foreach (uint Score in PlayerScore)
            {
                if (Score >= MaxScore)
                {
                    m_State = GAME_STATE.MATCH_OVER;
                }
            }

        }
        else if (m_State == GAME_STATE.MATCH_OVER)
        {
            m_State = GAME_STATE.MENU;

        }
    }

    bool CheckForRoundEnd()
    {

        if (m_NumberOfPlayersAlive == 1)
        {
            //Look for the player still alive.
            for (int playerAliveIndex = 0; playerAliveIndex < (int)PLAYER.PLAYER_MAX; ++playerAliveIndex)
            {
                if (PlayerActiveInMatch[playerAliveIndex])
                {
                    if (PlayerIsAlive[playerAliveIndex])
                    {
                        //Increase his score.
                        ++PlayerScore[playerAliveIndex];

                        //Change the state to round over.
                        m_State = GAME_STATE.ROUND_OVER;

                        if (!CheckForMatchEnd())
                        {
                            UIManager.GlobalUIManager.SetWinningPlayer((PLAYER)playerAliveIndex);
                            StartCoroutine(RestartRound());
                        }

                        return true;
                    }
                }
            }
        }

        return false;
    }

    bool CheckForMatchEnd()
    {
        for (int i = 0; i < (int)PLAYER.PLAYER_MAX; ++i)
        {
            if (PlayerScore[i] >= 3)
            {
                m_State = GAME_STATE.MATCH_OVER;

                MusicManager.globalMusicManager.PlayMusic(MusicManager.SONG.MENU);

                UIManager.GlobalUIManager.SetWinningPlayerMatch((PLAYER)i+1);

                StartCoroutine(FinishGame());
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// A function to call when a player gets killed. Receives the player killed.
    /// </summary>
    /// <param name="playerKilled"></param>
    public void PlayerKilled(PLAYER playerKilled)
    {
        Vector3 pos = PlayerPool[(int)playerKilled].transform.GetChild(0).position;

        Instantiate(PlayerArtExplosion[(int)playerKilled], pos, Quaternion.Euler(-90, 0, 0));

        PlayerPool[(int)playerKilled].SetActive(false);

        //Set the playerIsAlive bool to false
        PlayerIsAlive[(int)playerKilled] = false;

        //Decrease the number of players alive.
        --m_NumberOfPlayersAlive;

        AudioManager.GlobalAudioManager.PlaySoundEffect(AudioManager.SOUND_EFFECT.DEATH, 0.5f);


        UIManager.GlobalUIManager.PlayerDead((int)playerKilled);

        //Check to see if there is just one player left and the round should end.
        bool RoundEnd = CheckForRoundEnd();

        Time.timeScale = 0.25f;

        if (!RoundEnd)
            StartCoroutine(ResumeNormalTime());
    }

    IEnumerator ResumeNormalTime()
    {
        yield return new WaitForSeconds(0.66f);

        Time.timeScale = 1.0f;
    }

    IEnumerator FinishGame()
    {
        yield return new WaitForSeconds(1.5f);


        //Destroy the level created.
        GameObject[] TrashLevel = GameObject.FindGameObjectsWithTag("Level");
        foreach (GameObject Level in TrashLevel)
        {
            Destroy(Level);
        }

        //Destroy the level created.
        foreach (GameObject Obj in PlayerPool)
        {
            Destroy(Obj);
        }

        Destroy(this.gameObject);

        Time.timeScale = 1.0f;

        SceneManager.LoadScene("Credits");

    }

    IEnumerator RestartRound()
    {
        yield return new WaitForSeconds(1.0f);

        Time.timeScale = 1.0f;

        SceneManager.LoadScene("Game_Scene");
    }
}
