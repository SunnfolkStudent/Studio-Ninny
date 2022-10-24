using System;
using System.Linq.Expressions;
using UnityEngine;
using Random = UnityEngine.Random;

public class RinnAudio : MonoBehaviour
{
    public AudioClip[] reactionAudio;
    public AudioClip[] gibberishAudio;
    public AudioClip[] idleAudio;
    
    private AudioSource _audio;

    private Interact _interact;

    private bool yes = true;
    private bool no = true;
    
    private void Start()
    {
        _audio = GetComponent<AudioSource>();
        _interact = GetComponent<Interact>();
        
        _audio.loop = true;
        IdleAudio();
        
    }
    
    public void ReactionAudio()
    {
        AudioClipRandom(reactionAudio);
    }
    
    public void Gibberish()
    {
        if (_interact.isTalking && yes)
        {
            _audio.Stop();
            AudioClipRandom(gibberishAudio);
            _audio.loop = false;

            no = true;
            if (yes)
            {
                yes = false;
            }
        } 
    }
    
    public void IdleAudio()
    {
        if (!_interact.isTalking && no)
        {
            _audio.Stop();
            AudioClipRandom(idleAudio);
            _audio.loop = true;

            yes = true;

            no = false;
        }

        if (!_audio.isPlaying)
        {
            AudioClipRandom(idleAudio);
        }
    }

    private void AudioClipRandom(AudioClip[] audioClips)
    {
        _audio.PlayOneShot(audioClips[Random.Range(0, audioClips.Length)]);
    }
}