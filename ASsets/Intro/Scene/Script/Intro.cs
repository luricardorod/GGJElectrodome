using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour {

    public float AnimationTime;
    public GameObject text;

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
            Debug.Log("uuuuuuuuuuuuuuuuu");

            text.SetActive(true);
            if (Input.anyKeyDown)
            {
                Debug.Log("dsfsdfs");
                SceneManager.LoadScene(1);
            }
        }
        else Timer += Time.deltaTime;
    }

}
