using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    private AudioSource Speaker;

    public List<AudioClip> SoundEffect = new List<AudioClip>();
    public AudioClip m_ACPlayerMov;
    public AudioClip m_ACDash;
    public AudioClip m_ACShot;
    public AudioClip m_ACBomb;
    public AudioClip m_ACLaser;
    public AudioClip m_ACDamage;
    public AudioClip m_ACEnergyCharge;
    public AudioClip m_ACEnergyEmpty;
    public AudioClip m_ACBarrier;
    public AudioClip m_ACParry;

    public enum SOUND_EFFECT
    {
        PLAYER_MOVEMENT = 0,
        DASH,
        SHOT,
        BOMB, 
        LASER,
        DAMAGE,
        ENERGY_CHARGE,
        ENERGY_EMPTY,
        BARRIER,
        PARRY
    }

    private void Start()
    {
        Speaker = gameObject.AddComponent<AudioSource>();

        SoundEffect.Insert(SoundEffect.Count, m_ACPlayerMov);
        SoundEffect.Insert(SoundEffect.Count, m_ACDash);
        SoundEffect.Insert(SoundEffect.Count, m_ACShot);
        SoundEffect.Insert(SoundEffect.Count, m_ACBomb);
        SoundEffect.Insert(SoundEffect.Count, m_ACLaser);
        SoundEffect.Insert(SoundEffect.Count, m_ACDamage);
        SoundEffect.Insert(SoundEffect.Count, m_ACEnergyCharge);
        SoundEffect.Insert(SoundEffect.Count, m_ACEnergyEmpty);
        SoundEffect.Insert(SoundEffect.Count, m_ACBarrier);
        SoundEffect.Insert(SoundEffect.Count, m_ACParry);
    }

    public void PlaySoundEffect(SOUND_EFFECT soundEffectToPlay, float clipTime = 0.0f)
    {
        if((int)soundEffectToPlay < SoundEffect.Count)
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
