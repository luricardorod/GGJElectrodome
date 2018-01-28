using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour {

    private AudioSource Speaker;

    public List<AudioClip> Songs = new List<AudioClip>();
    public AudioClip m_ACMainMenu;
    public AudioClip m_ACGame;

    public enum SONG
    {
        MENU = 0,
        GAME
    }

    private void Start()
    {
        Speaker = gameObject.AddComponent<AudioSource>();

        Songs.Insert(Songs.Count, m_ACMainMenu);
        Songs.Insert(Songs.Count, m_ACGame);
    }

    public void PlayMusic(SONG songToplay)
    {
        Debug.Log("Playing... 1");
        if ((int)songToplay < Songs.Count)
        {
            Debug.Log("Playing... 2");
            if (Songs[(int)songToplay] != null)
            {
                Speaker.clip = Songs[(int)songToplay];
                Speaker.loop = true;
                Speaker.Play();
                Debug.Log("Playing... 3");
            }
        }
    }
}
