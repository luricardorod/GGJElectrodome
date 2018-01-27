using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chain_Script : MonoBehaviour
{
    // How long will the power have inpact in the enemy.
    public float Duration;

    // The movement limit for the chained character.
    public SphereCollider Col;

    private float ChainMagnitude;
    private GameObject OtherPlayer;

    // The position where the player used the chain 
    [HideInInspector]
    public Vector3 StartPosition;
    // If the bullet hit the enemy we sotre that position in here.
    private Vector3 EndPosition;

    private Vector3 MaxDistanceToMove;

    // Use this for initialization
    void Start()
    {
        Col.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (OtherPlayer)
        {
            Vector3 dist = OtherPlayer.transform.position - StartPosition;
            if (dist.magnitude > Col.radius)
            {
                dist.Normalize();
                dist *= Col.radius;

                OtherPlayer.transform.position = StartPosition + dist;
            }
        }
    }

    // If the power made contact with the enemy.
    private void OnTriggerEnter(Collider other)
    {
        OtherPlayer = other.gameObject;
        // We store the enemies position to set how far he can move.
        EndPosition = other.transform.position;
        // Create a vector between the enemy and the initial pos of the chain.
        MaxDistanceToMove = EndPosition - StartPosition;
        // Set the boundary from where the chained character can move.
        Col.center = StartPosition;
        Col.radius = MaxDistanceToMove.magnitude;
        ChainMagnitude = Col.radius;
        Col.enabled = true;
    }
}
