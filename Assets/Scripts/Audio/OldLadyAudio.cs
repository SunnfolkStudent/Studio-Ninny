using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class OldLadyAudio : MonoBehaviour
{
    public AudioClip[] endConversationAudio;
    public AudioClip[] gibberish;
    public AudioClip[] idle;
    
    private AudioSource _audio;

    private Interact _interact;

    private bool yes = true;
    private bool no = true;
    
    private void Start()
    {
        _audio = GetComponent<AudioSource>();
        _interact = GetComponent<Interact>();
        
        Idle();
        _audio.loop = true;
    }
    
    public void EndConversationAudio()
    {
        AudioClipRandom(endConversationAudio);
    }
    
    public void Gibberish()
    {
        if (_interact.isTalking && yes)
        {
            _audio.Stop();
            AudioClipRandom(gibberish);
            _audio.loop = false;

            no = true;
            if (yes)
            {
                yes = false;
            }
        } 
    }
    
    public void Idle()
    {
        if (!_interact.isTalking && no)
        {
            _audio.Stop();
            AudioClipRandom(idle);
            _audio.loop = true;

            yes = true;
            
            if (no)
            {
                no = false;
            }
        }
    }
    
    private void AudioClipRandom(AudioClip[] audioClips)
    {
        _audio.PlayOneShot(audioClips[Random.Range(0, audioClips.Length)]);
    }
}