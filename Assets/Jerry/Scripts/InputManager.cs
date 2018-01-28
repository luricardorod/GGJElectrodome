using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

	string[] powers = new string[4];

	PlayerMovement playerMovement;
	PlayerInfo playerInfo;
	PowersAdmin powersAdmin;
	// Use this for initialization
	void Start () {
		playerInfo = GetComponent<PlayerInfo> ();
		powersAdmin = GetComponent<PowersAdmin> ();
		playerMovement = GetComponent<PlayerMovement> ();
		powers [0] = "Dash/Parry" + playerInfo.number;
		powers [1] = "Stun/Chain" + playerInfo.number;
		powers [2] = "Barrier/Slide" + playerInfo.number;
		powers [3] = "Bomb/Overlord" + playerInfo.number;
	}
	
	// Update is called once per frame
	void Update () {
		MovementInput ();
		PowersInput ();
		PauseInput ();
		MovSetInput ();
	}

	void MovementInput() 
	{
		Vector3 rightStick;

		rightStick.x = Input.GetAxis("RHorizontal" + playerInfo.number);
		rightStick.y = 0;
		rightStick.z = Input.GetAxis("RVertical" + playerInfo.number);

		if (rightStick.sqrMagnitude > 0.0f) 
		{
			playerMovement.PointTo (rightStick.normalized);
			//Girar	
		}

		Vector3 leftStick;


		leftStick.x = Input.GetAxis("LHorizontal" + playerInfo.number);
		leftStick.y = 0;
		leftStick.z = Input.GetAxis("LVertical" + playerInfo.number);

		playerMovement.MoveTo (leftStick);
		

	}

	void PowersInput()
	{
		int movSetOffest = (int)playerInfo.movSet * 4;
		for(int i = 0; i < 2; ++i)
		{
			if(Input.GetButtonDown(powers[i]))
			{
				powersAdmin.ExcecutePower((PowersAdmin.Powers)(i + movSetOffest));
			}

			if(Input.GetAxis(powers[i + 2]) > 0.8f)
			{
				powersAdmin.ExcecutePower((PowersAdmin.Powers)(i + 2 + movSetOffest));
			}
		}

	}

	void MovSetInput()
	{
		if(Input.GetButtonDown("FlipMoveSet" + playerInfo.number))
		{
			playerInfo.ToggleMovSet ();
		}
	}

	void PauseInput()
	{
		
	}
}
