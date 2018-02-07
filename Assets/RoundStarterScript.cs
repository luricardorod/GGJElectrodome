using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundStarterScript : MonoBehaviour
{
    void Start()
    {
        GameState.GlobalGameState.InitRound();
    }
}
