using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	public Transform pointer;
	Vector3 pastBodyPosition;
	PlayerInfo playerInfo;
	float stopTime;

	void Start()
	{
		playerInfo = GetComponent<PlayerInfo> ();
	}

	// Update is called once per frame
	void Update () {
		
	}

	public void PointTo(Vector3 pointDir)
	{
		if (!playerInfo.IsLocked(PlayerInfo.Locks.Pointing)) {
			playerInfo.currentPointerDir = pointDir.magnitude == 0.0f ? playerInfo.currentPointerDir  : pointDir;
			pointer.LookAt(pointer.position + pointDir);
		} 
	}

	public void MoveTo(Vector3 moveDir)
	{
		if (playerInfo.IsLocked(PlayerInfo.Locks.MovementControl)) {
			moveDir = playerInfo.currentMovementDir;
		} 
		if (!playerInfo.IsLocked(PlayerInfo.Locks.Movement) && moveDir.magnitude != 0) {
			
			transform.rotation = Quaternion.LookRotation(moveDir);
			playerInfo.currentMovementDir = moveDir.normalized;
			
			pastBodyPosition = transform.position;
			transform.position += (moveDir *
								  Time.deltaTime *
				                  playerInfo.energy *
								  playerInfo.maxSpeed *
								  playerInfo.speedBoost.boost);
			float movedMagnitude = (pastBodyPosition - transform.position).magnitude;
			if (movedMagnitude > playerInfo.minReqMoveForChargeEnergy) {
				stopTime = 0;
				if (!playerInfo.IsLocked(PlayerInfo.Locks.Energy)) {
					playerInfo.energy += Mathf.Pow((playerInfo.offsetMinenergy - playerInfo.energy),
						(playerInfo.energy + playerInfo.gainEnergy)) *
						playerInfo.scaleEnergy *
						Time.deltaTime *
						movedMagnitude *
						playerInfo.delayTimeCharge;
				}
				
			}
		} 
		else {
			ReduceEnergy ();
		}

		playerInfo.energy = Mathf.Clamp (playerInfo.energy, 0.05f, 1.0f);

	}

	public void ReduceEnergy() {
		stopTime += Time.deltaTime;
		playerInfo.energy -= stopTime * stopTime * playerInfo.delayLoseEnergy;
	}


}
