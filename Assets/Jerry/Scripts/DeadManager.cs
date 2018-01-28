using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadManager : MonoBehaviour {

	public enum DeadCause {
		SpaceFall,
		Kamehaquiado,
		Incinerated,
		None
	}

	public float speedSpaceFall = 1;
	public DeadCause cause;

	// Use this for initialization
	void Start () {
		Destroy(GetComponent<InputManager> ());
	}

	public void DeadInCombat(DeadCause caus){
		cause = caus;
	}
	
	// Update is called once per frame
	void Update () {
		switch (cause) {
		case DeadCause.SpaceFall:
			
			speedSpaceFall *= 0.99f;
			float fFalling = Mathf.Clamp (speedSpaceFall, 0.3f, 1.0f);

			transform.localScale = new Vector3 (fFalling, fFalling, fFalling);
			transform.parent.GetChild (1).localScale = new Vector3 (fFalling, fFalling, fFalling);
			break;
		default:
			break;
		}
	}
		

	void SpaceFall(){
	}
}
