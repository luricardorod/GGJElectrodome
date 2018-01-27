using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesColor : MonoBehaviour {

    // Use this for initialization

    //Regular strenght 4-6
    //Animación 

    PlayerLogic pm;
	public float explosionDuration;
	public Transform attachedObj;
	public float maxEmissionRate = 10.0f;
	public Color colorA;
	public Color colorB;
	public Color explodeA;
	public Color explodeB;
	private	ParticleSystem.EmissionModule emissionMod;
	private ParticleSystem.NoiseModule noiseMod;
	private ParticleSystem.ShapeModule shapeMod;
	private ParticleSystem.ColorOverLifetimeModule colorOverLifeTimeMod;
	private ParticleSystem.MainModule mainMod;
	private bool discharge = false;
	private bool explosionToggled = false;
	private float dischargedTime = 0.0f;

	void Start () {
		ParticleSystem ps = GetComponent<ParticleSystem> ();
		pm = attachedObj.GetComponentInParent<PlayerLogic> ();
		emissionMod = ps.emission;
		noiseMod = ps.noise;
		shapeMod = ps.shape;
		mainMod = ps.main;
		colorOverLifeTimeMod = ps.colorOverLifetime;
		colorA.a = 1.0F;
		colorB.a = 1.0F;
		explodeA.a = 1.0F;
		explodeB.a = 1.0F;
		ParticleSystem.TrailModule trailMod = ps.trails;
		trailMod.colorOverLifetime = new ParticleSystem.MinMaxGradient(colorA);
		SetColor (GetComponent<ParticleSystem> (), colorA, colorB);
		SetColor(transform.GetChild (0).GetComponent<ParticleSystem> (), colorA, colorB);
	}

	void Update() {

		if (pm.fEnergy == 0) {
			Discharge ();
		}

		GetComponent<ParticleSystem> ();

		if (!discharge) {
			float emissionFactor = pm.fEnergy > 0.75f ? 2.0f - (1.0f - pm.fEnergy) : 1.0f;
			emissionMod.rateOverTime = new ParticleSystem.MinMaxCurve (pm.fEnergy * maxEmissionRate * emissionFactor); 
		} 
		else {
			//Debug.Log ("Explosion");
			dischargedTime += Time.deltaTime;
			if (dischargedTime > explosionDuration) {
				ResetParticleSettings ();
			} else if (dischargedTime > (explosionDuration - 0.5f) && !explosionToggled) {
				Explode ();
			}
		}
	}

	void ResetParticleSettings() {
		//Debug.Log ("Explosion Off");
		emissionMod.rateOverTime = new ParticleSystem.MinMaxCurve (maxEmissionRate * 3.0f); 
		noiseMod.scrollSpeed = new ParticleSystem.MinMaxCurve (5);
		shapeMod.radius = 0.01f;
		discharge = false;
		noiseMod.strength = new ParticleSystem.MinMaxCurve (4.0f, 6.0f);
		emissionMod.rateOverTime = new ParticleSystem.MinMaxCurve (50.0f);
		SetColor (GetComponent<ParticleSystem> (), colorA, colorB);
		SetColor(transform.GetChild (0).GetComponent<ParticleSystem> (), colorA, colorB);
		colorOverLifeTimeMod.enabled = false;
		mainMod.startSize = new ParticleSystem.MinMaxCurve (0.3f, 0.8f);
	}

	void Explode() 
	{
		shapeMod.radius = 1.0f;
		//Debug.Log ("Explosion RRR");
		explosionToggled = true;
		noiseMod.strength = new ParticleSystem.MinMaxCurve (8.0f, 12.0f);
	}

	void Discharge()
	{
		if (!discharge) {			
			discharge = true;
			dischargedTime = 0.0f;
			colorOverLifeTimeMod.enabled = true;
			noiseMod.strength = new ParticleSystem.MinMaxCurve (0.0f, 0.5f);
			emissionMod.rateOverTime = new ParticleSystem.MinMaxCurve (50.0f); 
			noiseMod.scrollSpeed = new ParticleSystem.MinMaxCurve (0);
			explosionToggled = false;
			mainMod.startSize = new ParticleSystem.MinMaxCurve (1.0f, 1.5f);
			SetColor (GetComponent<ParticleSystem> (), explodeA, explodeB);
			SetColor(transform.GetChild (0).GetComponent<ParticleSystem> (), explodeA, explodeB);
		}
	}

	void SetColor(ParticleSystem ps, Color a, Color b)
	{
		ParticleSystem.MainModule mm = ps.main;
		mm.startColor = new ParticleSystem.MinMaxGradient (a, b);
	}
}
