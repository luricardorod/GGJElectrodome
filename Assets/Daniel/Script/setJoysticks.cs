using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class setJoysticks : MonoBehaviour
{
    private string[] controllers = new string[4];
    private int PlayersJoined = 0;
    private bool playable = false;
    private bool[] playerSet;

    private string[] ColorPlayers;
    public Color[] playerColor;

    Text startTxt;

    public GameObject startT;
    public Image[] playerImg;

    // Use this for initialization
    void Start()
    {
        startTxt = startT.gameObject.GetComponent<Text>();

        ColorPlayers = new string[4];
        ColorPlayers[0] = "Green";
        ColorPlayers[1] = "Orange";
        ColorPlayers[2] = "Red";
        ColorPlayers[3] = "Blue";

        playerSet = new bool[4];

        for (int i = 0; i < 4; ++i)
            playerSet[i] = false;
    }

    void Update()
    {
        if (PlayersJoined < 4)
        {
            for (int i = 0; i < 4; ++i)
            {
                if (Input.GetButtonDown("FlipMoveSet" + i.ToString()) && !playerSet[i])
                {
                    GameState.GlobalGameState.JoinPlayer(i + 1);
                    playerImg[i].color = playerColor[i];
                    playerSet[i] = true;
                    ++PlayersJoined;

                    if (PlayersJoined >= 2)
                    {
                        playable = true;
                        startTxt.text = "Press  start  to  begin  match";
                    }
                }
            }
        }

        if (PlayersJoined > 0)
        {
            for (int i = 0; i < 4; ++i)
            {
                if (Input.GetButtonDown("Cancel" + i.ToString()) && playerSet[i])
                {
                    GameState.GlobalGameState.DropPlayer(i + 1);
                    playerImg[i].color = Color.white;
                    playerSet[i] = false;
                    --PlayersJoined;

                    if (PlayersJoined < 2)
                    {
                        playable = false;
                        startTxt.text = "";
                    }
                }
            }
        }

        if (playable)
        {
            if (Input.GetButtonDown("Start"))
            {
                GameState.GlobalGameState.m_State = GAME_STATE.MATCH_START;
                GameState.GlobalGameState.InitMatch(PlayersJoined);
                SceneManager.LoadScene("Game_Scene");
            }
        }
    }

}
