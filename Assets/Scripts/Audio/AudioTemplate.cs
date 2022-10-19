using UnityEngine;
using Random = UnityEngine.Random;

public class AudioTemplate : MonoBehaviour
{
    public AudioClip[] sampleAudio;
    
    private AudioSource _audio;

    private void Start()
    {
        _audio = GetComponent<AudioSource>();
    }
    
    public void SampleAudio()
    {
        AudioClipRandom(sampleAudio);
    }
    
    private void AudioClipRandom(AudioClip[] audioClips)
    {
        _audio.PlayOneShot(audioClips[Random.Range(0, audioClips.Length)]);
    }
}