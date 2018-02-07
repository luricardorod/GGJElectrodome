using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesColor : MonoBehaviour
{

    // Use this for initialization

    //Regular strenght 4-6
    //Animación 

    ParticleSystem partSystem;

    PlayerInfo playerInfo;
    public float explosionDuration;
    public Transform attachedObj;
    public float maxEmissionRate = 10.0f;
    public Color colorAUncharged;
    public Color colorBUncharged;
    public Color colorACharged;
    public Color colorBCharged;
    public Color explodeA;
    public Color explodeB;
    private ParticleSystem.EmissionModule emissionMod;
    private ParticleSystem.NoiseModule noiseMod;
    private ParticleSystem.ShapeModule shapeMod;
    private ParticleSystem.ColorOverLifetimeModule colorOverLifeTimeMod;
    private ParticleSystem.MainModule mainMod;
    private bool discharge = false;
    private bool explosionToggled = false;
    private float dischargedTime = 0.0f;
    private bool canExplode = false;

    void Start()
    {
        partSystem = GetComponent<ParticleSystem>();
        playerInfo = attachedObj.GetComponentInParent<PlayerInfo>();


        emissionMod = partSystem.emission;
        noiseMod = partSystem.noise;
        shapeMod = partSystem.shape;
        mainMod = partSystem.main;
        colorOverLifeTimeMod = partSystem.colorOverLifetime;

        colorACharged.a = 1.0F;
        colorBCharged.a = 1.0F;
        colorAUncharged.a = 1.0F;
        colorBUncharged.a = 1.0F;
        explodeA.a = 1.0F;
        explodeB.a = 1.0F;

        ParticleSystem.TrailModule trailMod = partSystem.trails;
        trailMod.colorOverLifetime = new ParticleSystem.MinMaxGradient(colorACharged);
        SetColor(partSystem, colorAUncharged, colorBUncharged);
        SetColor(transform.GetChild(0).GetComponent<ParticleSystem>(), colorAUncharged, colorBUncharged);
    }

    void Update()
    {

        if (playerInfo.energy <= 0.06f && canExplode)
        {
            Discharge();
        }

        if (!discharge)
        {
            if (playerInfo.energy > 0.06f)
            {
                canExplode = true;
            }

            Color aLerped = Color.Lerp(colorAUncharged, colorACharged, playerInfo.energy);
            Color bLerped = Color.Lerp(colorBUncharged, colorBCharged, playerInfo.energy);

            SetColor(partSystem, aLerped, bLerped);
            SetColor(transform.GetChild(0).GetComponent<ParticleSystem>(), aLerped, bLerped);

            float emissionFactor = playerInfo.energy > 0.75f ? 2.0f - (1.0f - playerInfo.energy) : 1.0f;
            emissionMod.rateOverTime = new ParticleSystem.MinMaxCurve(playerInfo.energy * maxEmissionRate * emissionFactor);
        }
        else
        {
            //Debug.Log ("Explosion");
            dischargedTime += Time.deltaTime;
            if (dischargedTime > explosionDuration)
            {
                ResetParticleSettings();
            }
            else if (dischargedTime > (explosionDuration - 0.5f) && !explosionToggled)
            {
                Explode();
            }
        }
    }

    void ResetParticleSettings()
    {
        //Debug.Log ("Explosion Off");
        emissionMod.rateOverTime = new ParticleSystem.MinMaxCurve(maxEmissionRate * 3.0f);
        noiseMod.scrollSpeed = new ParticleSystem.MinMaxCurve(5);
        shapeMod.radius = 0.01f;
        discharge = false;
        noiseMod.strength = new ParticleSystem.MinMaxCurve(4.0f, 6.0f);
        emissionMod.rateOverTime = new ParticleSystem.MinMaxCurve(50.0f);
        SetColor(GetComponent<ParticleSystem>(), colorAUncharged, colorBUncharged);
        SetColor(transform.GetChild(0).GetComponent<ParticleSystem>(), colorAUncharged, colorBUncharged);
        colorOverLifeTimeMod.enabled = false;
        mainMod.startSize = new ParticleSystem.MinMaxCurve(0.3f, 0.8f);
        canExplode = false;
    }

    void Explode()
    {
        shapeMod.radius = 1.0f;
        //Debug.Log ("Explosion RRR");
        explosionToggled = true;
        noiseMod.strength = new ParticleSystem.MinMaxCurve(8.0f, 12.0f);
    }

    void Discharge()
    {
        if (!discharge)
        {
            discharge = true;
            dischargedTime = 0.0f;
            colorOverLifeTimeMod.enabled = true;
            noiseMod.strength = new ParticleSystem.MinMaxCurve(0.0f, 0.5f);
            emissionMod.rateOverTime = new ParticleSystem.MinMaxCurve(50.0f);
            noiseMod.scrollSpeed = new ParticleSystem.MinMaxCurve(0);
            explosionToggled = false;
            mainMod.startSize = new ParticleSystem.MinMaxCurve(1.0f, 1.5f);
            SetColor(partSystem, explodeA, explodeB);
            SetColor(transform.GetChild(0).GetComponent<ParticleSystem>(), explodeA, explodeB);
        }
    }

    void SetColor(ParticleSystem ps, Color a, Color b)
    {
        ParticleSystem.MainModule mm = ps.main;
        mm.startColor = new ParticleSystem.MinMaxGradient(a, b);
    }
}
