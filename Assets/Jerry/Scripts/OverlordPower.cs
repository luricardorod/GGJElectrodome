using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverlordPower : MonoBehaviour {

	[HideInInspector]public Transform invokerObj;
	[HideInInspector]public PlayerLogic pl;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		Quaternion q = new Quaternion ();
		q.SetLookRotation (pl.Aim);
		transform.position = invokerObj.position;
		transform.rotation = q;
	}
}
