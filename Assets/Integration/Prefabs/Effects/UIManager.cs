using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// This class makes sure that the lighting animations are loaded accordingly to each player's energy.
public class UIManager : MonoBehaviour
{
    public static UIManager GlobalUIManager;

    // How much energy is needed to unlock the next energy bar.
    public int iEnergyAmountPerBlock;
    // Stores the player's energy bars that are turned on.
    public int[] iCurrentBar;
    // The rays that will be activated.
    public Slider[] Rays;

    // Each palyer's power bar.
    public Transform[] FillColor;
    public Transform[] IconColor;
    public Transform[] PlayerIcon;

    public EnergyBarControl[] PlayersEnergy;

    public Color[] PlayerColor;

    public Text WinningPlayer;

    public Text[] PlayerScoreText;

    // It's used to just instantiate the energy icon once.
    private bool bFirstTime = true;

    // From here we'll take all the player's Energy in order to show the energy animations.
    private GameState gameState;

    // Use this for initialization
    void Start()
    {
        UIManager.GlobalUIManager = this;
        gameState = GameState.GlobalGameState;

        // Create all the colours.
        PlayerColor = new Color[(int)PLAYER.PLAYER_MAX];
        for (int i = 0; i < (int)PLAYER.PLAYER_MAX; ++i)
        {
            PlayerColor[i] = new Color();
        }

        Color Orange;
        Orange.r = 0.8f;
        Orange.g = 0.7f;
        Orange.b = 0.0f;
        Orange.a = 1.0f;

        PlayerColor[(int)PLAYER.PLAYER_1] = Color.green;
        PlayerColor[(int)PLAYER.PLAYER_2] = Orange;
        PlayerColor[(int)PLAYER.PLAYER_3] = Color.red;
        PlayerColor[(int)PLAYER.PLAYER_4] = Color.blue;

        // We set the energy's color according to each player.
        for (int i = 0; i < FillColor.Length; i++)
        {
            // The color that fills the bar.
            FillColor[i].GetComponent<Image>().color = PlayerColor[i];
            // circle that moves across the bar.
            IconColor[i].GetComponent<Image>().color = PlayerColor[i];
            // Big icon representing each player's score.
            PlayerIcon[i].GetComponent<Image>().color = PlayerColor[i];
        }

        // We initialize the energy counters for every player.
        for (int i = 0; i < iCurrentBar.Length; i++)
        {
            iCurrentBar[i] = 0;
        }

        //Change the player score text ui elements. 
        for (int i = 0; i < 4; ++i)
        {
            PlayerScoreText[i].text = gameState.PlayerScore[i].ToString();
        }

        for (int i = 0; i < 4; ++i)
        {
            if (!gameState.PlayerActiveInMatch[i])
            {
                Destroy(Rays[i].gameObject);
                Destroy(PlayerIcon[i].gameObject);
                Destroy(PlayerScoreText[i].gameObject);

            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //
        uint uiPlayersAmount = gameState.m_NumberOfPlayersAlive;

        // Se aumenta cada barra dependiendo de la energia de cada mono.
        for (int i = 0; i < Rays.Length; i++)
        {
            if (gameState.PlayerActiveInMatch[i])
            {
                if (gameState.PlayerPool[i])
                {
                    Transform Body = gameState.PlayerPool[i].transform.GetChild((int)PlayerSubObjects.BODY);
                    Rays[i].value = Body.GetComponent<PlayerInfo>().energy;
                }
            }
        }

    }

    public void PlayerChangeMoveset(int player, PowersAdmin.MovSet moveSet)
    {
        PlayersEnergy[player].SetMoveSet(moveSet);
    }

    public void AddBar(int iPlayer)
    {
        PlayersEnergy[iPlayer].SetEnergy(PlayerColor[iPlayer], Rays[iPlayer].value);
    }

    public void PlayerDead(int playerDead)
    {
        // The color that fills the bar.
        FillColor[playerDead].GetComponent<Image>().color = Color.grey;
        // circle that moves across the bar.
        IconColor[playerDead].GetComponent<Image>().color = Color.grey;
        // Big icon representing each player's score.
        PlayerIcon[playerDead].GetComponent<Image>().color = Color.grey;
    }

    public void SetWinningPlayerMatch(PLAYER playerWon)
    {
        WinningPlayer.text = "Player " + ((int)playerWon).ToString() + " Won \nElectrodome";
        WinningPlayer.color = PlayerColor[(int)playerWon-1];

        //Change the player score text ui elements. 
        for (int i = 0; i < 4; ++i)
        {
            PlayerScoreText[i].text = gameState.PlayerScore[i].ToString();
        }
    }

    public void SetWinningPlayer(PLAYER playerWon)
    {
        WinningPlayer.text = "Player " + ((int)playerWon + 1).ToString() + " Won";
        WinningPlayer.color = PlayerColor[(int)playerWon];

        //Change the player score text ui elements. 
        for (int i = 0; i < 4; ++i)
        {
            PlayerScoreText[i].text = gameState.PlayerScore[i].ToString();
        }
    }
}
