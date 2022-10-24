using UnityEngine;
using Random = UnityEngine.Random;

public class MellinAudio : MonoBehaviour
{
    public AudioClip[] endConversationAudio;
    public AudioClip[] reactionAudio;
    public AudioClip[] talkingAudio;
    
    private AudioSource _audio;

    private void Start()
    {
        _audio = GetComponent<AudioSource>();
        
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
        AudioClipRandom(talkingAudio);
    }
    
    private void AudioClipRandom(AudioClip[] audioClips)
    {
        _audio.PlayOneShot(audioClips[Random.Range(0, audioClips.Length)]);
    }
}