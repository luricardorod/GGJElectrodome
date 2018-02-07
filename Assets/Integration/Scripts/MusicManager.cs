using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager globalMusicManager;

    private AudioSource Speaker;

    public List<AudioClip> Songs = new List<AudioClip>();
    public AudioClip m_ACMainMenu;
    public AudioClip m_ACGame;

    public enum SONG
    {
        MENU = 0,
        GAME = 1
    }

    private void Start()
    {
        globalMusicManager = this;

        DontDestroyOnLoad(this.gameObject);

        Speaker = gameObject.AddComponent<AudioSource>();

        Songs.Insert(Songs.Count, m_ACMainMenu);
        Songs.Insert(Songs.Count, m_ACGame);

        PlayMusic(SONG.MENU);
    }

    public void PlayMusic(SONG songToplay)
    {
        if ((int)songToplay < Songs.Count)
        {
            if (Songs[(int)songToplay] != null)
            {
                Speaker.Stop();
                Speaker.clip = Songs[(int)songToplay];
                Speaker.loop = true;
                Speaker.Play();
            }
        }
    }
}
