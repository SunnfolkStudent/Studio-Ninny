using UnityEngine;
using Random = UnityEngine.Random;

public class HangerAudio : MonoBehaviour
{
    public AudioClip[] activationAudio;
    public AudioClip[] idleAudio;
    
    private AudioSource _audio;

    private void Start()
    {
        _audio = GetComponent<AudioSource>();
    }
    
    public void ActivationAudio()
    {
        AudioClipRandom(activationAudio);
    }
    
    public void IdleAudio()
    {
        AudioClipRandom(idleAudio);
    }
    
    private void AudioClipRandom(AudioClip[] audioClips)
    {
        _audio.PlayOneShot(audioClips[Random.Range(0, audioClips.Length)]);
    }
}
