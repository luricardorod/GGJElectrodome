using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Strun_script : MonoBehaviour
{
    public float StunDuration;
    public float Speed;
    public Vector3 Direction;

    // Use this for initialization
    void Start()
    {
        Destroy(this, 10);
    }

    void Update()
    {
        transform.Translate(Direction * Speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        StartCoroutine(StunTime(other));
    }

    // Stuns the player for a given time.
    IEnumerator StunTime(Collider other)
    {
        PlayerLogic playerOther = other.GetComponentInParent<PlayerLogic>();

        playerOther.Die();
        Destroy(other.gameObject.transform.parent.gameObject);

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
