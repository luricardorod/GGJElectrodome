using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier_Script : MonoBehaviour
{
    public float ParryWindow = 0.5f;
    public float ForwardThrustLength = 5.0f;
    public float ForwardThrustTime = 1.0f;
    public float DurationExisting = 10.0f;

    private bool ParrySuccesful = false;

	void Update ()
    {
        if (ParrySuccesful)
        {
            //Moverlo el largo del forward thrust sobre un el tiempo que toma el forwardThrust.
            transform.Translate(Time.deltaTime * (ForwardThrustLength / ForwardThrustTime) * transform.forward);
        }

        ParryWindow -= Time.deltaTime;

        if (ParryWindow <= 0.0f)
        {
            DurationExisting -= Time.deltaTime;

            if (DurationExisting <= 0.0f)
            {
                //TODO: Add a fade out animation for this instead.
                Destroy(this);
            }
        }
	}

    void OnTriggerEnter(Collider other)
    {
        //Si el otro tiene un tag de tipo shot y la ventana para hacer parry aun no acaba.
        if (other.tag == "Shot" && ParryWindow > 0.0f)
        {
            ParrySuccesful = true;
        }
    }
}
