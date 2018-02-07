using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICountDown : MonoBehaviour
{
    Text Countdown;
    public GameObject ElectroDomeIntro;
    float CountDownTime = 3.5f;

    // Use this for initialization
    void Start()
    {
        Countdown = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        CountDownTime -= Time.deltaTime;

        Countdown.text = Mathf.FloorToInt(CountDownTime).ToString();

        if (Mathf.FloorToInt(CountDownTime) <= 0.0f)
        {
            Countdown.enabled = false;
            GameObject Animation = Instantiate(ElectroDomeIntro);
            Destroy(Animation, 5.0f);

            Destroy(this.gameObject);

        }
    }
}
