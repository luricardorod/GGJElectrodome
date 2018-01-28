using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PlayButton : MonoBehaviour {
    public float AnimationTime;
    public GameObject text;

    private float Timer;
	// Use this for initialization
	void Start () {
        text.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        if (Timer > AnimationTime)
        {
            if (Input.anyKeyDown)
            {
                Debug.Log("dsfsdfs");
                SceneManager.LoadScene(1);
            }
            text.SetActive(true);
        }
        else Timer += Time.deltaTime;
	}
}
