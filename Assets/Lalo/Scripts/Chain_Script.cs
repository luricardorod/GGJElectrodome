using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chain_Script : MonoBehaviour
{
    // How long will the power have inpact in the enemy.
    public float Duration;

    // The position where the player used the chain 
    private Vector3 StartPosition;
    // If the bullet hit the enemy we sotre that position in here.
    private Vector3 EndPosition;

    private Vector3 MaxDistanceToMove;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    // If the power made contact with the enemy.
    private void OnTriggerEnter(Collider other)
    {
        // We store the enemies position to set how far he can move.
        EndPosition = other.transform.position;
        // TODO: make a vector between endpos and startpos, and take that vector's magnitude to make it the maximum distance the enemy can move from the startpos.
        MaxDistanceToMove = EndPosition - StartPosition;
        float dist = MaxDistanceToMove.magnitude;
    }
}
