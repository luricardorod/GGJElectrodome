using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsTimeOut : MonoBehaviour
{

    void Start()
    {
        StartCoroutine(GoToMatchScreen());
    }

    IEnumerator GoToMatchScreen()
    {
        yield return new WaitForSeconds(5.0f);

        SceneManager.LoadScene("MatchStart");
    }

}
