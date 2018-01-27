using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesColor : MonoBehaviour {

	// Use this for initialization

	//Regular strenght 4-6
	//Animación 

	PlayerMovement pm;
	public Transform attachedObj;
	public float maxEmissionRate = 10.0f;
	public Color colorA;
	public Color colorB;
	private	ParticleSystem.EmissionModule psem;
	private ParticleSystem.NoiseModule psnm;
	private bool discharge = false;
	private bool explosionToggled = false;
	private float dischargedTime = 0.0f;

	void Start () {
		psem = GetComponent<ParticleSystem> ().emission;
		psnm = GetComponent<ParticleSystem> ().noise;
		pm = attachedObj.GetComponent<PlayerMovement> ();
		colorA.a = 1.0F;
		colorB.a = 1.0F;
		SetColor (GetComponent<ParticleSystem> ());
		SetColor(transform.GetChild (0).GetComponent<ParticleSystem> ());
	}

	void Update() {

		if (Input.GetButtonDown ("Jump")) {
			Discharge ();
		}

		GetComponent<ParticleSystem> ();

		if (!discharge) {
			float emissionFactor = pm.fEnergy > 0.75f ? 2.0f - (1.0f - pm.fEnergy) : 1.0f;
			psem.rateOverTime = new ParticleSystem.MinMaxCurve (pm.fEnergy * maxEmissionRate * emissionFactor); 
		} 
		else {
			Debug.Log ("Explosion");
			dischargedTime += Time.deltaTime;
			if (dischargedTime > 1.5f) {
				Debug.Log ("Explosion Off");
				psem.rateOverTime = new ParticleSystem.MinMaxCurve (maxEmissionRate * 3.0f); 
				psnm.scrollSpeed = new ParticleSystem.MinMaxCurve ();
				discharge = false;
				psnm.strength = new ParticleSystem.MinMaxCurve (4.0f, 6.0f);
			} else if (dischargedTime > 1.0f && !explosionToggled) {
				Debug.Log ("Explosion RRR");
				explosionToggled = true;
				psnm.strength = new ParticleSystem.MinMaxCurve (8.0f, 12.0f);
			}
		}
	}

	void Discharge()
	{
		if (!discharge) {			
			discharge = true;
			dischargedTime = 0.0f;
			psnm.strength = new ParticleSystem.MinMaxCurve (1.0f, 2.0f);
			psem.rateOverTime = new ParticleSystem.MinMaxCurve (maxEmissionRate * 2.0f); 
			explosionToggled = false;
		}
	}

	void SetColor(ParticleSystem ps)
	{
		ParticleSystem.MainModule mm = ps.main;
		mm.startColor = new ParticleSystem.MinMaxGradient (colorA, colorB);
	}
}
