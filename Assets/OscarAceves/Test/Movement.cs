using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

    private Vector3 mousePos;
    private Vector3 wantedPos;

    // Use this for initialization
    void Start () {
        moveToMousePosition();
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        moveToMousePosition();
    }

    void moveToMousePosition() {
        mousePos = Input.mousePosition;
        wantedPos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 20.0f));

        transform.position = wantedPos;
    }
}
