using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

    private Vector3 mousePos;
    private Vector3 wantedPos;
    private float offset = 0.5f;

    public GameObject bombPrefab;

    // Use this for initialization
    void Start () {
        moveToMousePosition();
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        moveToMousePosition();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Bomba");
            Instantiate(bombPrefab, new Vector3(transform.position.x + offset, 0, transform.position.z + offset), Quaternion.identity);
        }
    }

    void moveToMousePosition() {
        mousePos = Input.mousePosition;
        wantedPos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 20.0f));

        transform.position = wantedPos;
    }
}
