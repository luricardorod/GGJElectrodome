using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidePower : MonoBehaviour
{

    public float spawnDistance;
    public GameObject SlidePuddle;
    public float fLiveTime;
    private Vector3 startPos;
    private Transform PlayerSpawned;
    private int ID;

    void Update()
    {
        if (PlayerSpawned)
        {
            fLiveTime -= Time.deltaTime;

            if ((startPos - PlayerSpawned.position).magnitude > spawnDistance)
            {
                Slide slide = Instantiate(SlidePuddle, PlayerSpawned.position, Quaternion.identity).GetComponent<Slide>();
                startPos = PlayerSpawned.position;
                slide.IDLanzador = ID;
                AudioManager.GlobalAudioManager.PlaySoundEffect(AudioManager.SOUND_EFFECT.SLIDE, 0.5f);

            }

            if (fLiveTime < 0)
            {
                Destroy(gameObject);
            }
        }
    }


    public void Init(Transform player)
    {
        PlayerSpawned = player;
        startPos = player.position;
        Slide slide = Instantiate(SlidePuddle, startPos, Quaternion.identity).GetComponent<Slide>();
        ID = player.gameObject.GetInstanceID();
        slide.IDLanzador = ID;

    }
}
