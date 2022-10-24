using UnityEngine;
using Random = UnityEngine.Random;

public class MellinAudio : MonoBehaviour
{
    public AudioClip[] endConversationAudio;
    public AudioClip[] reactionAudio;
    public AudioClip[] talkingAudio;
    
    private AudioSource _audio;

    private Interact _interact;

    private bool yes = true;
    
    private void Start()
    {
        _audio = GetComponent<AudioSource>();
        _interact = GetComponent<Interact>();
        
    }
    
    public void EndConversationAudio()
    {
        AudioClipRandom(endConversationAudio);
    }
    
    public void ReactionAudio()
    {
        AudioClipRandom(reactionAudio);
    }
    
    public void TalkingAudio()
    {
        if (_interact.isTalking && yes)
        {
            _audio.Stop();
            AudioClipRandom(talkingAudio);
            _audio.loop = false;

            if (yes)
            {
                yes = false;
            }
        } 
    }

    public void IdleAudio()
    {
        yes = true;
    }
    
    private void AudioClipRandom(AudioClip[] audioClips)
    {
        _audio.PlayOneShot(audioClips[Random.Range(0, audioClips.Length)]);
    }
}