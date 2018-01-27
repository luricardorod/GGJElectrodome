using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{

    //function movement
    //y=(1-x)^(x*1.7)0.1
    //y=(fOffsetMinEnergy-x)^(x+fGainEnergy)(fScaleEnergy)
    //fGainEnergy curva de energia
    //fsacleenergy escala de energia
    //fOffsetMinEnergy  determina que tan rapido se aproximara a uno al final
    // Use this for initialization
    void Start () {
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }

	// Update is called once per frame
	void Update () {
    }

    
}
