using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{

    string[] powers = new string[4];

    PlayerMovement playerMovement;
    PlayerInfo playerInfo;
    PowersAdmin powersAdmin;

    void Start()
    {
        playerInfo = GetComponent<PlayerInfo>();
        powersAdmin = GetComponent<PowersAdmin>();
        playerMovement = GetComponent<PlayerMovement>();
        powers[0] = "Dash/Parry" + (int)playerInfo.number;
        powers[1] = "Stun/Slide" + (int)playerInfo.number;
        powers[2] = "Barrier/Chain" + (int)playerInfo.number;
        powers[3] = "Bomb/Overlord" + (int)playerInfo.number;
    }

    // Update is called once per frame
    void Update()
    {
        MovementInput();
        PowersInput();
        PauseInput();
        MovSetInput();
    }

    void MovementInput()
    {
        Vector3 rightStick;

        rightStick.x = Input.GetAxis("RHorizontal" + (int)playerInfo.number);
        rightStick.y = 0;
        rightStick.z = Input.GetAxis("RVertical" + (int)playerInfo.number);

        if (rightStick.sqrMagnitude > 0.0f)
        {
            playerMovement.PointTo(rightStick.normalized);
            //Girar	
        }

        Vector3 leftStick;


        leftStick.x = Input.GetAxis("LHorizontal" + (int)playerInfo.number);
        leftStick.y = 0;
        leftStick.z = Input.GetAxis("LVertical" + (int)playerInfo.number);

        playerMovement.MoveTo(leftStick);

    }

    void PowersInput()
    {
        int movSetOffest = (int)playerInfo.movSet * 4;

        if (Input.GetButtonDown(powers[0]))
        {
            if (playerInfo.movSet == PowersAdmin.MovSet.Defensive)
                powersAdmin.ExcecutePower(PowersAdmin.Powers.Dash);
            else
                powersAdmin.ExcecutePower(PowersAdmin.Powers.Parry);

        }

        if (Input.GetButtonDown(powers[1]))
        {
            if (playerInfo.movSet == PowersAdmin.MovSet.Defensive)
                powersAdmin.ExcecutePower(PowersAdmin.Powers.Slide);
            else
                powersAdmin.ExcecutePower(PowersAdmin.Powers.Stun);

        }

        if (Input.GetAxis(powers[2]) > .8f)
        {
            if (playerInfo.movSet == PowersAdmin.MovSet.Defensive)
                powersAdmin.ExcecutePower(PowersAdmin.Powers.Barrier);
            else
                powersAdmin.ExcecutePower(PowersAdmin.Powers.Chain);

        }

        if (Input.GetAxis(powers[3]) > .8f)
        {
            if (playerInfo.movSet == PowersAdmin.MovSet.Defensive)
                powersAdmin.ExcecutePower(PowersAdmin.Powers.Bomb);
            else
                powersAdmin.ExcecutePower(PowersAdmin.Powers.Overlord);

        }
    }

    void MovSetInput()
    {
        if (Input.GetButtonDown("FlipMoveSet" + (int)playerInfo.number))
        {
            playerInfo.ChangeMoveSet(PowersAdmin.MovSet.Offensive);
            UIManager.GlobalUIManager.PlayerChangeMoveset((int)playerInfo.number, PowersAdmin.MovSet.Offensive);
        }
        else
        if (Input.GetButtonDown("MoveSetDef" + (int)playerInfo.number))
        {
            playerInfo.ChangeMoveSet(PowersAdmin.MovSet.Defensive);
            UIManager.GlobalUIManager.PlayerChangeMoveset((int)playerInfo.number, PowersAdmin.MovSet.Defensive);
        }
    }

    void PauseInput()
    {

    }
}
