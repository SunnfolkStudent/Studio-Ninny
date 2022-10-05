using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireplaceAnim : MonoBehaviour
{
    public bool isActive;
    
    private Animator _animator;

    public Hitbox pHitbox;
    
    
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        // if resting or has activated, animate
        if (pHitbox.fireplaceUI || isActive)
        {
            isActive = true;
            _animator.Play("FireplaceActive");
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
