using UnityEngine;
using Random = UnityEngine.Random;

public class FerrymanAudio : MonoBehaviour
{
    public AudioClip[] chucklingAudio;
    public AudioClip[] mumbleAudio;
    public AudioClip[] humAudio;
    public AudioClip[] noticeAudio;

    private AudioSource _audio;

    private Interact _interact;

    private bool yes = true;
    private bool no = true;
    
    private void Start()
    {
        _audio = GetComponent<AudioSource>();
        _interact = GetComponent<Interact>();
        
        HumAudio();
        _audio.loop = true;
    }

    public void ChucklingAudio()
    {
        AudioClipRandom(chucklingAudio);
    }
    
    public void MumbleAudio()
    {
        if (_interact.isTalking && yes)
        {
            _audio.Stop();
            AudioClipRandom(mumbleAudio);
            _audio.loop = false;

            no = true;
            if (yes)
            {
                yes = false;
            }
        } 
        
    }

    public void HumAudio()
    {
        if (!_interact.isTalking && no)
        {
            _audio.Stop();
            AudioClipRandom(humAudio);
            _audio.loop = true;

            yes = true;
            
            if (no)
            {
                no = false;
            }
        }
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