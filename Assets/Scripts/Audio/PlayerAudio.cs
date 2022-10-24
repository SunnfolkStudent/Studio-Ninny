using Unity.VisualScripting;
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
    
    public bool canLand;

    [Header("COMPONENTS")]
    private AudioSource _audio;
    private PlayerInput _input;
    private PlayerCollision _collision;
    private PlayerMovement _movement;

    private void Start()
    {
        _audio = GetComponent<AudioSource>();
        _input = GetComponentInParent<PlayerInput>();
        _collision = GetComponentInParent<PlayerCollision>();
        _movement = GetComponentInParent<PlayerMovement>();
        _audio.pitch = 1f;
    }

    /*private void Update()
    {
        LandingAudio();
        JumpAudio();
    }*/

    public void AttackAudio()
    {
        AudioClipRandom(attackAudio);
    }
    
    public void JumpAudio()
    {
        if (_input.JumpPressed && (_collision.IsGrounded()))
        {
            AudioClipRandom(jumpAudio);
        }
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