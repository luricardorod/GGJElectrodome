using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidePower : MonoBehaviour {

	public float spawnDistance;
	public Transform decal;
	public float fLiveTime;
	[HideInInspector] public Transform attachedObj;
	[HideInInspector] public Vector3 startPos;
	private Transform spawnPoint;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		fLiveTime -= Time.deltaTime;
		if ((startPos - attachedObj.position).magnitude > spawnDistance) {
			Instantiate (decal, spawnPoint.position, Quaternion.identity);
			startPos = attachedObj.position;
		}
		if (fLiveTime < 0) {
			Destroy (gameObject);
		}
	}
				

	public void Init (Transform player) {
		attachedObj = player;
		startPos = player.position;
		spawnPoint = attachedObj.GetChild (3);
		Instantiate (decal, spawnPoint.position, Quaternion.identity);
	}
}
