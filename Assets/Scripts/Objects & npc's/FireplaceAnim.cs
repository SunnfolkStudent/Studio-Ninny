using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FireplaceAnim : MonoBehaviour
{
    public bool isActive;
    
    private Animator _animator;
    private Light2D _light;

    public Hitbox pHitbox;
    
    
    void Start()
    {
        _animator = GetComponent<Animator>();
        _light.intensity = 0;
    }

    void Update()
    {
        // if resting or has activated, animate
        if (pHitbox.fireplaceUI || isActive)
        {
            isActive = true;
            _animator.Play("FireplaceActive");
            _light.intensity = 1.5f;
        }
        
        //if standing, smoke
        else if (pHitbox.fireplaceEncounterUI)
        {
            _animator.Play("FireplaceInactive");
        }
        //if nowhere, nothing
        else
        {
            _animator.Play("Nowhere");
        }
    }
}
