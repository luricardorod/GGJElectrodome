using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidePower : MonoBehaviour {

	public int decalsPlaceableCount;
	public float placeRate;
	private float timer;
	public Transform decal;
	[HideInInspector] public Transform attachedObj;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		if (decalsPlaceableCount > 0) {
			if (timer >= placeRate) {
				timer = 0.0f;
				--decalsPlaceableCount;
				Instantiate (decal, attachedObj.position, Quaternion.identity);
			}
		} 
		else {
			Destroy (gameObject);
		}
	}
}
