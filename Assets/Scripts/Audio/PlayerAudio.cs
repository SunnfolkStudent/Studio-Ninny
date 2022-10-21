using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerAudio : MonoBehaviour
{
    public AudioClip[] attackAudio;
    public AudioClip[] jumpAudio;
    public AudioClip[] landingAudio;
    public AudioClip[] damageAudio;
    public AudioClip[] curiousAudio;
    public AudioClip[] drowningAudio;
    public AudioClip[] walkingAudio;

    private AudioSource _audio;

    private void Start()
    {
        _audio = GetComponent<AudioSource>();
    }

    public void AttackAudio()
    {
        AudioClipRandom(attackAudio);
    }
    
    public void JumpAudio()
    {
        AudioClipRandom(jumpAudio);
    }
    
    public void LandingAudio()
    {
        AudioClipRandom(landingAudio);
    }
    
    public void DamageAudio()
    {
        AudioClipRandom(damageAudio);
    }
    
    public void CuriousAudio()
    {
        AudioClipRandom(curiousAudio);
    }
    
    public void DrowningAudio()
    {
        AudioClipRandom(drowningAudio);
    }

    public void WalkingAudio()
    {
        AudioClipRandom(walkingAudio);
    }
    
    private void AudioClipRandom(AudioClip[] audioClips)
    {
        _audio.PlayOneShot(audioClips[Random.Range(0, audioClips.Length)]);
    }
}