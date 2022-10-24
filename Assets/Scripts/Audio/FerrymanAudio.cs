using UnityEngine;
using Random = UnityEngine.Random;

public class FerrymanAudio : MonoBehaviour
{
    public AudioClip[] chucklingAudio;
    public AudioClip[] mumbleAudio;
    public AudioClip[] humAudio;
    public AudioClip[] noticeAudio;

    private AudioSource _audio;

    private void Start()
    {
        _audio = GetComponent<AudioSource>();
        HumAudio();
    }

    public void ChucklingAudio()
    {
        AudioClipRandom(chucklingAudio);
    }
    
    public void MumbleAudio()
    {
        AudioClipRandom(mumbleAudio);
    }

    public void HumAudio()
    {
        AudioClipRandom(humAudio);
    }
    
    public void NoticeAudio()
    {
        AudioClipRandom(noticeAudio);
    }

    private void AudioClipRandom(AudioClip[] audioClips)
    {
        _audio.PlayOneShot(audioClips[Random.Range(0, audioClips.Length)]);
    }
}