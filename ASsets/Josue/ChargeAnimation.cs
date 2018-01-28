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

    public PlayerInfo playerInformation;

    // Use this for initialization
    void Start()
    {
        //playerInformation = gameState.GetComponent<PlayerInfo>();

        // We initialize the energy counters for every player.
        for (int i = 0; i < iCurrentBar.Length; i++)
        {
            iCurrentBar[i] = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        uint uiPlayersAmount = gameState.GetComponent<GameState>().m_NumberOfPlayersAlive;

        //Debug.Log(playerInformation.energy);
        //Rays[0].value = playerInformation.energy;

        // Se aumenta cada barra dependiendo de la energia de cada mono.
        for (int i = 0; i < Rays.Length; i++)
        {
            Debug.Log("Energy de player " + i + ": " + gameState.GetComponent<GameState>().m_PlayerPool[i].transform.GetChild(0).GetComponent<PlayerInfo>().energy);
            Rays[i].value = gameState.GetComponent<GameState>().m_PlayerPool[i].transform.GetChild(0).GetComponent<PlayerInfo>().energy;
        }


        //// Go through all of the players from the pool.
        //for (uint j = 0; j < uiPlayersAmount; j++)
        //{
        //    // We see if the energy of each player is greater than a given amount, times the amount of energy bars filled.
        //    if (gameState.GetComponent<GameState>().m_PlayerPool[j].transform.GetChild(0).GetComponent<PlayerInfo>().energy > iEnergyAmountPerBlock * (iCurrentBar[j] + 1))
        //    {
        //        //TODO: aumentar el valor del slider y activar animaciones along the way.
        //        iCurrentBar[j]++;

        //    }
        //}
    }
}
