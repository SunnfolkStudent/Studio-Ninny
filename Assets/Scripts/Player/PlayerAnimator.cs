using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    public float attackTimer;
    
    public bool isFacingLeft;
    public bool isTalking;
    public bool talkAnim;

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
        // Interact
        if (isTalking)
        {
            if (talkAnim)
            {
                _animator.Play("Talk");
                talkAnim = false;
            }

            _animator.Play("TalkIdle");
            return;
        } 
        else { talkAnim = true; }


        #region Slash

        // Slash
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

        #endregion

        #region Basic Movement

        // Flip
        if (_input.MoveVector.x != 0)
        { 
            _spriteRenderer.flipX = _input.MoveVector.x < 0;
            isFacingLeft = _spriteRenderer.flipX;
        }
        
        // Idle - Walk
        if (_collision.IsGrounded() /*|| _collision.IsPlatforming()*/)
        {
            _animator.Play(_input.MoveVector.x == 0 ? "Idle" : "Walk");
        }
        
        // Fall - Jump
        else
        {
            _animator.Play((_rb.velocity.y < 0) ? "Fall" : "Jump");
        }

        #endregion
    }
}
