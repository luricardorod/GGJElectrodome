using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerSubObjects
{
    BODY,
    POINTER,
    EFFECTS
}

public class PlayerInit : MonoBehaviour
{
    private GameObject Body;
    private GameObject Pointer;
    private GameObject Effects;

    public void CreatePlayer(PLAYER player)
    {
        DontDestroyOnLoad(this.gameObject);

        GetComponentInChildren<PlayerInfo>().Init(player);

        Body = transform.GetChild((int)PlayerSubObjects.BODY).gameObject;
        Pointer = transform.GetChild((int)PlayerSubObjects.POINTER).gameObject;
        Effects = transform.GetChild((int)PlayerSubObjects.EFFECTS).gameObject;

        Instantiate(GameState.GlobalGameState.PlayerArtBody[(int)player], Body.transform);
        Instantiate(GameState.GlobalGameState.PlayerArtPointer[(int)player], Pointer.transform);
        Instantiate(GameState.GlobalGameState.PlayerArtEffects[(int)player], Effects.transform);

        foreach (ParticlesColor part in GetComponentsInChildren<ParticlesColor>())
            part.attachedObj = Body.transform;
    }

}
