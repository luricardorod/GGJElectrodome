using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public enum SOUND_EFFECT
    {
        PLAYER_MOVEMENT = 0,
        DASH,
        SHOT,
        STUN_HIT,
        BOMB,
        BOMB_EXP,
        OVERLORD,
        DAMAGE,
        DEATH,
        ENERGY_CHARGE,
        ENERGY_EMPTY,
        BARRIER,
        PARRY,
        SLIDE,
        MAX
    }

    public static AudioManager GlobalAudioManager;

    private AudioSource Speaker;

    public AudioClip[] SoundEffect = new AudioClip[(int)SOUND_EFFECT.MAX];

    private void Start()
    {
        GlobalAudioManager = this;
        DontDestroyOnLoad(this.gameObject);

        Speaker = gameObject.AddComponent<AudioSource>();

    }

    public void PlaySoundEffect(SOUND_EFFECT soundEffectToPlay, float clipTime = 0.0f)
    {
        if((int)soundEffectToPlay < (int)SOUND_EFFECT.MAX)
        {
            if(SoundEffect[(int)soundEffectToPlay] != null)
            {
                Speaker.clip = SoundEffect[(int)soundEffectToPlay];
                Speaker.time = clipTime;
                Speaker.Play();
            }
        }
    }
}
