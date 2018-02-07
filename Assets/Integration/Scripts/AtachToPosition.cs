using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtachToPosition : MonoBehaviour {

	public Transform attachedObject;
	public Vector3 offset;
		
	// Update is called once per frame
	void Update () {
		transform.position = attachedObject.position + offset;
	}
}
