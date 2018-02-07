using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour {

    public float AnimationTime;
    private float Timer;

    private Animator animater;

    private void Start()
    {
        animater = GetComponent<Animator>();
        animater.Play("MenuAnim");
    }

    // Update is called once per frame
    void Update()
    {
        if (Timer > AnimationTime)
        {
            if (Input.anyKeyDown)
            {
                SceneManager.LoadScene(1);
            }
        }
        else Timer += Time.deltaTime;
    }

}
