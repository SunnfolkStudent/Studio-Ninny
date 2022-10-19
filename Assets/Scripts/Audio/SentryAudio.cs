using UnityEngine;
using Random = UnityEngine.Random;

public class SentryAudio : MonoBehaviour
{
    public AudioClip[] attackAudio;
    public AudioClip[] damageAudio;
    public AudioClip[] deathAudio;
    public AudioClip[] idleAudio;
    public AudioClip[] snoringAudio;
    public AudioClip[] wakeAudio;
    
    private AudioSource _audio;

    private void Start()
    {
        _audio = GetComponent<AudioSource>();
    }
    
    public void AttackAudio()
    {
        AudioClipRandom(attackAudio);
    }
    
    public void DamageAudio()
    {
        AudioClipRandom(damageAudio);
    }
    
    public void DeathAudio()
    {
        AudioClipRandom(deathAudio);
    }
    
    public void IdleAudio()
    {
        AudioClipRandom(idleAudio);
    }
    
    public void SnoringAudio()
    {
        AudioClipRandom(snoringAudio);
    }
    
    public void WakeAudio()
    {
        AudioClipRandom(wakeAudio);
    }

    private void AudioClipRandom(AudioClip[] audioClips)
    {
        _audio.PlayOneShot(audioClips[Random.Range(0, audioClips.Length)]);
    }
}