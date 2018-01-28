using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour {
    public Animation anim;
    private Animator animater;

    private float Timer;
    private void Start()
    {
        animater = GetComponent<Animator>();
        animater.Play("MenuAnim");
    }

    private void Update()
    {

    }

    public void MainMenu()
    {
        SceneManager.LoadScene("");   
    }
}
