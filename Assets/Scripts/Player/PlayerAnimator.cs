using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    public float attackTimer;
    
    public bool isFacingLeft;

    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private PlayerInput _input;
    private PlayerCollision _collision;
    private Rigidbody2D _rb;

    public Slash slash;

    
    // public float attackTimer;
    // public bool attack;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        
        _input = GetComponentInParent<PlayerInput>();
        _collision = GetComponentInParent<PlayerCollision>();
        _rb = GetComponentInParent<Rigidbody2D>();
    }
    
    void Update()
    {
        if (slash.isSlashing)
        {
            if (slash.upSlash)
            {
                _animator.Play("MeleeUp");
            } 
            else if (slash.downSlash)
            {
                _animator.Play("MeleeDown");
            }
            else
            {
                _animator.Play("Melee");
            }
            attackTimer = Time.time + _animator.GetCurrentAnimatorClipInfo(0).Length;
            return;
        }
        
        
        if (_input.MoveVector.x != 0)
        { 
            _spriteRenderer.flipX = _input.MoveVector.x < 0;
            isFacingLeft = _spriteRenderer.flipX;
        }
    
        if (_collision.IsGrounded() /*|| _collision.IsPlatforming()*/)
        {
            // if not moving, idle animation, else: walk animation
            _animator.Play(_input.MoveVector.x == 0 ? "Idle" : "Walk");
        }
        else
        {
            _animator.Play((_rb.velocity.y < 0) ? "Fall" : "Jump");
        }
    }
}
