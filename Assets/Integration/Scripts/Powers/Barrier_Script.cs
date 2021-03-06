﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier_Script : MonoBehaviour
{
    private PlayerInfo playerInfo;
    public float lifeTime;

    public float ParryWindow = 2.0f;
    public float MaxSpeed = 20;
    public float DurationExisting = 10.0f;

    private bool ParrySuccesful = false;

    private Vector3 ThrustDir;
    private float velocity;

    void DestroyMe()
    {
        Destroy(gameObject);
    }

    public void Init(PlayerInfo playerInf)
    {
        transform.LookAt(transform.position + playerInf.currentPointerDir * 10.0f);
        playerInfo = playerInf;
        ThrustDir = playerInfo.currentPointerDir;
        Invoke("DestroyMe", lifeTime);
        velocity = 0.0f;
    }

    void Update ()
    {
        if (ParrySuccesful)
        {
            //Moverlo el largo del forward thrust sobre un el tiempo que toma el forwardThrust.
            velocity += 0.25f;

            velocity = Mathf.Min(velocity, MaxSpeed);

            transform.position += (Time.deltaTime * velocity * ThrustDir.normalized);
        }

        ParryWindow -= Time.deltaTime;

        if (ParryWindow <= 0.0f)
        {

            ParrySuccesful = true;
        }
	}
}
