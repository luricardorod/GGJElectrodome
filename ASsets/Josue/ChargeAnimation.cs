using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// This class makes sure that the lighting animations are loaded accordingly to each player's energy.
public class ChargeAnimation : MonoBehaviour
{
    // From here we'll take all the player's Energy in order to show the energy animations.
    public GameObject gameState;
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
    public Transform[] BoltToSpawn;

    // It's used to just instantiate the energy icon once.
    private bool bFirstTime = true;

    // Use this for initialization
    void Start()
    {
        // We set the energy's color according to each player.
        for (int i = 0; i < FillColor.Length; i++)
        {
            // The color that fills the bar.
            FillColor[i].GetComponent<Image>().color = gameState.GetComponent<GameState>().m_PlayerPool[i].transform.GetChild(0).GetComponent<PlayerInfo>().color;
            // circle that moves across the bar.
            IconColor[i].GetComponent<Image>().color = gameState.GetComponent<GameState>().m_PlayerPool[i].transform.GetChild(0).GetComponent<PlayerInfo>().color;
            // Big icon representing how many bars have been filled.
            BoltToSpawn[i].GetComponent<Image>().color = gameState.GetComponent<GameState>().m_PlayerPool[i].transform.GetChild(0).GetComponent<PlayerInfo>().color;
            // Big icon representing each player's score.
            PlayerIcon[i].GetComponent<Image>().color = gameState.GetComponent<GameState>().m_PlayerPool[i].transform.GetChild(0).GetComponent<PlayerInfo>().color;
        }

        // We initialize the energy counters for every player.
        for (int i = 0; i < iCurrentBar.Length; i++)
        {
            iCurrentBar[i] = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //
        uint uiPlayersAmount = gameState.GetComponent<GameState>().m_NumberOfPlayersAlive;

        // Se aumenta cada barra dependiendo de la energia de cada mono.
        for (int i = 0; i < Rays.Length; i++)
        {
            Rays[i].value = gameState.GetComponent<GameState>().m_PlayerPool[i].transform.GetChild(0).GetComponent<PlayerInfo>().energy;
        }
    }

    public void AddBar(int iPlayer)
    {
        Transform test = new GameObject().transform;

        if (Rays[iPlayer].value > 0.25f * (iCurrentBar[iPlayer] + 1))
        {
            iCurrentBar[iPlayer]++;
            test = Instantiate(BoltToSpawn[iPlayer]);
            test.parent = GetComponent<Canvas>().transform;
            test.position = IconColor[iPlayer].position;
            test.localScale = PlayerIcon[iPlayer].lossyScale / 2;
            test.rotation = Quaternion.identity;
        }
        else if (Rays[iPlayer].value < 0.25f * iCurrentBar[iPlayer])
        {
            if (test != null)
            {
                Destroy(test.gameObject);
            }
        }
    }
}
