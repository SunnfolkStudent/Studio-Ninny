using System.Linq.Expressions;
using UnityEngine;
using Random = UnityEngine.Random;

public class RinnAudio : MonoBehaviour
{
    public AudioClip[] reactionAudio;
    public AudioClip[] gibberishAudio;
    public AudioClip[] idleAudio;
    
    private AudioSource _audio;

    private void Start()
    {
        _audio = GetComponent<AudioSource>();
        IdleAudio();
        
    }
    
    public void ReactionAudio()
    {
        AudioClipRandom(reactionAudio);
    }
    
    public void Gibberish()
    {
        AudioClipRandom(gibberishAudio);
    }
    
    public void IdleAudio()
    {
        AudioClipRandom(idleAudio);
        _audio.loop = true;
    }
    
    private void AudioClipRandom(AudioClip[] audioClips)
    {
        _audio.PlayOneShot(audioClips[Random.Range(0, audioClips.Length)]);
    }
}