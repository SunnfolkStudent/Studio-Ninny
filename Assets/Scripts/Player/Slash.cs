using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Slash : MonoBehaviour
{
    public LayerMask whatIsSpikes;
    
    public float slashTime = 0.3f;
    public float slashTimer;
    public float slashCD = 0.3f;

    public float yOffset = 1.1f;
    public float xOffset = 1.4f;

    public bool canSlash = true;
    public bool isSlashing;

    public bool upSlash;
    public bool downSlash;
    public bool sideSlash;
    
    private BoxCollider2D _clipCol;
    private SpriteRenderer _clipSprite;
    private PlayerInput _input;
    private PlayerCollision _pCol;
    private PlayerMovement _pMove;

    public PlayerAnimator _pAnim;
    public Transform playerTrans;
    
    
    void Start()
    {
        _clipCol = GetComponent<BoxCollider2D>();
        _clipSprite = GetComponent<SpriteRenderer>();
        
        _input = GetComponentInParent<PlayerInput>();
        _pCol = GetComponentInParent<PlayerCollision>();
        _pMove = GetComponentInParent<PlayerMovement>();
        
        _clipCol.enabled = false;
        _clipSprite.enabled = false;
    }

    
    void Update()
    {
        SlashBox();
    }

    private void SlashBox()
    {
        #region PlaceSlashBox

        if (canSlash && _input.AttackPressed)
        {
            if (_input.MoveVector.y > 0)
            {
                transform.position = playerTrans.position + new Vector3(0, yOffset, 0);

                upSlash = true;
            }
            else if ((_input.MoveVector.y < 0) && !_pCol.IsGrounded())
            {
                transform.position = playerTrans.position + new Vector3(0, -yOffset, 0);

                downSlash = true;
            }
            else
            {
                transform.position = playerTrans.position + new Vector3(_pAnim.isFacingLeft ? -xOffset : xOffset, 0, 0);

                sideSlash = true;
            }

            slashTimer = Time.time + slashTime;
            _clipCol.enabled = true;
            //_clipSprite.enabled = true;

            canSlash = false;
        }

        #endregion

        #region Activate/DeactivateSlashBox

        if (upSlash || downSlash || sideSlash)
        {
            isSlashing = true;
        }
        else
        {
            isSlashing = false;
        }


        if (slashTimer < Time.time)
        {
            _clipCol.enabled = false;
            _clipSprite.enabled = false;

            upSlash = false;
            downSlash = false;
            sideSlash = false;
        }

        if ((slashTimer + slashCD) < Time.time)
        {
            canSlash = true;
        }

        #endregion
    }

    // Pogo
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (_clipCol.IsTouchingLayers(whatIsSpikes) && downSlash)
        {
            _pMove.Pogo();
        }
    }
}
