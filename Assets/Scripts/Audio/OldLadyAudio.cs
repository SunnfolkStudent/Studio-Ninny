using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class OldLadyAudio : MonoBehaviour
{
    public AudioClip[] endConversationAudio;
    public AudioClip[] gibberish;
    public AudioClip[] idle;
    
    private AudioSource _audio;
    public Toggle _toggle;

    private void Start()
    {
        _audio = GetComponent<AudioSource>();
        Idle();
    }
    
    public void EndConversationAudio()
    {
        AudioClipRandom(endConversationAudio);
    }
    
    public void Gibberish()
    {
        AudioClipRandom(gibberish);
    }
    
    public void Idle()
    {
        AudioClipRandom(idle);
    }
    
    private void AudioClipRandom(AudioClip[] audioClips)
    {
        _audio.PlayOneShot(audioClips[Random.Range(0, audioClips.Length)]);
    }
}