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
    private float fAngle = 0;

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
        MovePlayer();
        if (Input.GetKeyDown("space"))
        {
            fEnergy = 0;
            print("space key was pressed");
        }
    }

    void MovePlayer () {
        float fAxisX = Input.GetAxis("Horizontal");
        float fAxisY = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(fAxisX, 0, fAxisY);
        float fMagnitude = direction.magnitude;
        if (fMagnitude > 0)
        {
            fStopTime = 0;
            fEnergy += Mathf.Pow((fOffsetMinEnergy - fEnergy), (fEnergy + fGainEnergy))*(fScaleEnergy) * Time.deltaTime * fMagnitude * fDelayTimeCharge;
            if (direction != Vector3.zero)
                transform.rotation = Quaternion.LookRotation(direction);
            transform.position += (direction * Time.deltaTime * fEnergy * fMaxSpeed);
        }
        else
        {
            fStopTime += Time.deltaTime;
            fEnergy -= fStopTime * fStopTime  * fDelayLooseEnergy;
        }
        fEnergy = Mathf.Clamp(fEnergy, 0, 1);
    }
}
