using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Layers
{
    PlayerLayer = 8,
    FloorLayer,
    PowerLayer
}

public class PlayerMovement : MonoBehaviour
{
    public Transform pointer;

    float stoppedTime;

    public float maxSuspendedTime;
    float suspendedTime;

    Rigidbody rigidBody;
    PlayerInfo playerInfo;


    void Start()
    { 
        //Cache the player Info reference.
        playerInfo = GetComponent<PlayerInfo>();
        suspendedTime = maxSuspendedTime;
        rigidBody = GetComponent<Rigidbody>();
    }

    void Update()
    {
    }

    public void PointTo(Vector3 pointDir)
    {
        //If the player is not locked from changing pointer direction.
        if (!playerInfo.IsLocked(PlayerInfo.Locks.Pointing))
        {
            //Change the currentPointerDirection if the pointDir magnitude is bigger than 0, if it is 0, keep the current direction.
            playerInfo.currentPointerDir = pointDir.magnitude == 0.0f ? playerInfo.currentPointerDir : pointDir;
            //Make the pointer point to the direction.
            pointer.LookAt(pointer.position + pointDir);
        }
    }

    public void MoveTo(Vector3 moveDir)
    {
        //If the player is locked from changing direction.
        if (playerInfo.IsLocked(PlayerInfo.Locks.MovementControl))
        {
            //Keep the previous direction.
            moveDir = playerInfo.currentMovementDir;
        }

        //If the player is not locked from moving and it is moving.
        if (!playerInfo.IsLocked(PlayerInfo.Locks.Movement) && moveDir.magnitude != 0)
        {
            //Look towards the move Direction 
            transform.rotation = Quaternion.LookRotation(moveDir);
            //Set the current movemenet direction.
            playerInfo.currentMovementDir = moveDir.normalized;
            //Save the previous position
            Vector3 previousPosition = transform.position;

            //Change the position.
            transform.position += (moveDir *
                                  Time.deltaTime *
                                  playerInfo.energy *
                                  playerInfo.maxSpeed *
                                  playerInfo.speedBoost.boost);

            //Calculate how much the player moved from the previous position.
            float movedMagnitude = (previousPosition - transform.position).magnitude;
            //If it moved more than the minimal required movement to increase energy.
            if (movedMagnitude > playerInfo.minReqMoveForChargeEnergy)
            {
                //Set the stoppedTime to 0.
                stoppedTime = 0;
                //If the player's energy is not locked.
                if (!playerInfo.IsLocked(PlayerInfo.Locks.Energy))
                {
                    //Increase the player's energy.
                    playerInfo.energy += Mathf.Pow((playerInfo.offsetMinenergy - playerInfo.energy),
                        (playerInfo.energy + playerInfo.gainEnergy)) *
                        playerInfo.scaleEnergy *
                        Time.deltaTime *
                        movedMagnitude *
                        playerInfo.delayTimeCharge;
                }

            }
        }
        else //If it is locked from moving or not moving on purpose.
        {
            ReduceEnergy();
        }

        //Clamp the player's energy.
        playerInfo.energy = Mathf.Clamp(playerInfo.energy, 0.05f, 1.0f);
    }

    public void ReduceEnergy()
    {
        stoppedTime += Time.deltaTime;
        playerInfo.energy -= stoppedTime * stoppedTime * playerInfo.delayLoseEnergy;
    }

    void CheckGround()
    {
        if (Physics.Raycast(transform.position, -transform.up, 2.0f, (int)Layers.FloorLayer))
        {
            playerInfo.onGround = true;
            rigidBody.constraints |= RigidbodyConstraints.FreezePositionY;
        }
        else
        {
            playerInfo.onGround = false;
            suspendedTime = maxSuspendedTime;
        }
    }
}
