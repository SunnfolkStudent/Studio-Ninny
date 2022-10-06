using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerAudio : MonoBehaviour
{
    public AudioClip[] attackAudio;
    public AudioClip[] jumpAudio;
    public AudioClip[] landingAudio;
    public AudioClip[] damageAudio;
    public AudioClip[] curiousAudio;
    public AudioClip[] drowningAudio;

    private AudioSource _audio;

    private void Start()
    {
        _audio = GetComponent<AudioSource>();
    }

    //play in animation
    public void AttackAudio()
    {
        AudioClipRandom(attackAudio);
    }
    
    public void JumpAudio()
    {
        AudioClipRandom(jumpAudio);
    }
    
    public void LandingAudio()
    {
        AudioClipRandom(landingAudio);
    }
    
    public void DamageAudio()
    {
        AudioClipRandom(damageAudio);
    }
    
    public void CuriousAudio()
    {
        AudioClipRandom(curiousAudio);
    }
    
    public void DrowningAudio()
    {
        AudioClipRandom(drowningAudio);
    }
    
    //random clip (and pitch)
    private void AudioClipRandom(AudioClip[] audioClips)
    {
        //_audio.pitch = Random.Range(0.75f, 1.25f);
        _audio.PlayOneShot(audioClips[Random.Range(0, audioClips.Length)]);
    }
}
