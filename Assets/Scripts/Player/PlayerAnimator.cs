using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    public float attackTimer;
    public float animTimer;
    
    public bool isFacingLeft;
    
    public bool isTalking;
    public bool talkAnim;
    
    public bool isResting;
    public bool restAnim;

    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    
    private PlayerInput _input;
    private PlayerCollision _pCol;
    private Rigidbody2D _rb;
    private PlayerMovement _pMove;

    public Slash slash;
    public Hitbox pHitbox;

    
    // public float attackTimer;
    // public bool attack;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        
        _input = GetComponentInParent<PlayerInput>();
        _pCol = GetComponentInParent<PlayerCollision>();
        _rb = GetComponentInParent<Rigidbody2D>();
        _pMove = GetComponentInParent<PlayerMovement>();
    }
    
    void Update()
    {
        #region Slash
        
        // Slash
        if (slash.isSlashing)
        {
            if (slash.upSlash)
            {
                _animator.Play(isFacingLeft ? "MeleeUpLeft" : "MeleeUpRight");
            } 
            else if (slash.downSlash)
            {
                _animator.Play(isFacingLeft ? "MeleeDownLeft" : "MeleeDownRight");
            }
            else
            {
                _animator.Play(isFacingLeft ? "MeleeLeft" : "MeleeRight");
            }
            // attackTimer = Time.time + _animator.GetCurrentAnimatorClipInfo(0).Length;
            return;
        }

        #endregion

        
        #region Interactions

        // Interaction Anim
        if (isTalking)
        {
            if (talkAnim)
            {
                _animator.Play(isFacingLeft ? "TalkLeft" : "TalkRight");
                talkAnim = false;
                animTimer = Time.time + _animator.GetCurrentAnimatorClipInfo(0).Length;
            }

            if (animTimer < Time.time)
            {
                _animator.Play(isFacingLeft ? "TalkIdleLeft" : "TalkIdleRight");
            }
            return;
        }
        else { talkAnim = true; }

        #endregion
        
        
        #region Fireplace

        // TODO: RestLeft hakker på siste frame
        // Fireplace Anim
        if (isResting)
        {
            if (restAnim)
            {
                _animator.Play(isFacingLeft ? "RestLeft" : "RestRight");
                restAnim = false;
                animTimer = Time.time + _animator.GetCurrentAnimatorClipInfo(0).Length;
            }

            if (animTimer < Time.time)
            {
                _animator.Play(isFacingLeft ? "RestIdleLeft" : "RestIdleRight");
            }

            if (pHitbox.fireplaceUI)
            {
                return;
            }
            
        }
        else { restAnim = true; }

        #endregion


        #region Turn & Wallcling

        // Update turn
        if (!(_input.MoveVector.x == 0) && !_pCol.IsWalling())
        {
            isFacingLeft = _input.MoveVector.x < 0;
        }
        // Wall Cling
        else if (_pCol.IsWalling() && !_input.JumpPressed)
        {
            _animator.Play(isFacingLeft ? "WallLeft" : "WallRight");
        }

        #endregion
        

        #region Basic Movement

        // Idle - Walk
        if (_pCol.IsGrounded() /*|| _collision.IsPlatforming()*/)
        {
            if (_input.MoveVector.x == 0)
            {
                _animator.Play(isFacingLeft ? "IdleLeft" : "IdleRight");
            }
            else
            {
                _animator.Play(isFacingLeft ? "WalkLeft" : "WalkRight");
            }
        }
        
        // Fall & Jump
        else
        {
            if (_rb.velocity.y < 0)
            {
                if (_pCol.IsWalling())
                {
                    _animator.Play(isFacingLeft ? "WallLeft" : "WallRight");
                }
                else
                {
                    _animator.Play(isFacingLeft ? "FallLeft" : "FallRight");
                }
            }
            else
            {
                _animator.Play(isFacingLeft ? "JumpLeft" : "JumpRight");
            }
        }

        #endregion
    }
}
