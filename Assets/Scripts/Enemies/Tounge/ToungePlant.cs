using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToungePlant : MonoBehaviour
{
    public int health = 3;

    public float attackTimer;
    public float attackDelay = 1f;

    public bool isActive;
    public bool isHit;

    public float hurtTimer;
    public float hurtTime = 0.2f;
    
    // private BoxCollider2D _hitBox;
    
    private Animator _anim;
    private BoxCollider2D _attackBox;
    
    private SpriteRenderer _plantRenderer;
    
    // public PlayerHealth pHealth;
    
    void Start()
    {
        // _hitBox = GetComponentInParent<BoxCollider2D>();
        _attackBox = GetComponent<BoxCollider2D>();
        
        _anim = GetComponentInParent<Animator>();
        _plantRenderer = GetComponentInParent<SpriteRenderer>();
        
        _attackBox.enabled = false;
        
        _anim.Play("Idle");
    }

    void Update()
    {
        if (isActive && attackTimer < Time.time)
        {
            print("active");
            
            // Go down (anim)
            _anim.Play("Attack");
            
            _attackBox.enabled = true;
            
            if (isHit)
            {
                _attackBox.enabled = false;
                
                // retreat (anim)
                _anim.Play("Retreat");
                
                // Add timer to isActive
                attackTimer = Time.time + attackDelay;
                
                hurtTimer = Time.time + hurtTime;

                isHit = false;
            }
        }
        else if (_attackBox.enabled)
        {
            _anim.Play("Retreat");
        }

        if (hurtTimer > Time.time) 
        { _plantRenderer.color = Color.red; }
        else
        { _plantRenderer.color = Color.white; }
    }
}
