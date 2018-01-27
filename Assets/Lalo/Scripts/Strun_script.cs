using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Strun_script : MonoBehaviour
{
    public float StunDuration;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        StartCoroutine(StunTime(other));
        //StopCoroutine(StunTime);

    }

    // Stuns the player for a given time.
    IEnumerator StunTime(Collider other)
    {
        PlayerLogic playerOther = other.GetComponentInParent<PlayerLogic>();

        // Store the player's speed.
        float fOtherSpped = playerOther.fMaxSpeed;
        // The other player is stopped.
        playerOther.fMaxSpeed = 0;
        // We wait for the stun to finish.
        yield return new WaitForSeconds(StunDuration);
        // We Make the enemy able to move again.
        playerOther.fMaxSpeed = fOtherSpped;

        Destroy(gameObject);
    }
}
