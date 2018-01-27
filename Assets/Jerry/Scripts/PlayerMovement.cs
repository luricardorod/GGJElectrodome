using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour {
    public float fEnergy = 0;
    public float fMaxSpeed = 20;
    public float fScaleEnergy = 0.4f;
    public float fGainEnergy = 0.4f;
    public float fOffsetMinEnergy = 1.2f;
    public float fDelayTimeCharge = 0.5f;
    public float fDelayLooseEnergy = 0.001f;
    private float fStopTime = 0;
    //function movement
    //y=(1-x)^(x*1.7)0.1
    //y=(fOffsetMinEnergy-x)^(x+fGainEnergy)(fScaleEnergy)
    //fGainEnergy curva de energia
    //fsacleenergy escala de energia
    //fOffsetMinEnergy  determina que tan rapido se aproximara a uno al final
    // Use this for initialization
    void Start () {

	}

	// Update is called once per frame
	void Update () {
        MovePlayer();
        if (Input.GetKeyDown("space"))
        {
            fEnergy = 0;
            print("space key was pressed");
        }
    }

    void MovePlayer () {
        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        transform.Translate(direction * Time.deltaTime * fEnergy * fMaxSpeed);
        float fMagnitude = direction.magnitude;
        if (fMagnitude > 0)
        {
            fStopTime = 0;
            fEnergy += Mathf.Pow((fOffsetMinEnergy - fEnergy), (fEnergy + fGainEnergy))*(fScaleEnergy) * Time.deltaTime * fMagnitude * fDelayTimeCharge;
        }
        else
        {
            fStopTime += Time.deltaTime;
            fEnergy -= fStopTime * fStopTime  * fDelayLooseEnergy;
        }
        fEnergy = Mathf.Clamp(fEnergy, 0, 1);
    }
}
