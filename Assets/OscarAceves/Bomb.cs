using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour {

    public float expansivePower = 0.01f;
    public float expansiveRadio = 3.0f;
    public GameObject particle;

    private void FixedUpdate()
    {
        if (gameObject.transform.localScale.x < expansiveRadio)
        {
            gameObject.transform.localScale = new Vector3(
                    gameObject.transform.localScale.x + expansivePower,
                    gameObject.transform.localScale.y,
                    gameObject.transform.localScale.z + expansivePower
                );

            var material = GetComponent<Renderer>().material;
            var color = material.color;

            material.color = new Color(color.r, color.g, color.b, color.a - ((expansivePower / 2) * Time.deltaTime));
        }
        else {
            particle.GetComponent<ParticleSystem>().Play();
            Destroy(gameObject);
        }
    }



}
