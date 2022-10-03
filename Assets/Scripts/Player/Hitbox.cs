using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour
{
    private Vector3 _respawnPos;
    
    public Transform playerTrans;

    public bool interactUI;
    public bool talkUI;

    private BoxCollider2D _hitbox;
    private PlayerCollision _pCol;
    private PlayerInput _input;
    private PlayerMovement _pMove;

    public PlayerAnimator pAnim;
    
    void Start()
    {
        _hitbox = GetComponent<BoxCollider2D>();
        
        _pMove = GetComponentInParent<PlayerMovement>();
        _pCol = GetComponentInParent<PlayerCollision>();
        _input = GetComponentInParent<PlayerInput>();
        
        _respawnPos = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("SpawnZone"))
        {
            // if u hit a respawn zone that is your hazard respawn
            _respawnPos = other.transform.position;
        }
        if (other.CompareTag($"DeathHazard"))
        {
            // health--
            
            // hurt anim
            
            // if no life; ded
            
            // respawn
            playerTrans.position = _respawnPos;
            
        }
    }

    // interaction
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Interact") && _pCol.IsGrounded())
        {
            // UI interact popup
            interactUI = true;
            
            if (_input.Interact)
            {
                // removes the first UI
                interactUI = false;
                
                // Dissable move
                _input.characterControl = false;
            
                // Play interact anim
                pAnim.isTalking = true;
            
                // UI popup
                talkUI = true;
            }
            
        }
        // if !grounded remove UI
        else { interactUI = false; }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Interact"))
        {
            // removes the first UI
            interactUI = false;
        } 
    }

    void Update()
    {
        // TODO: Add delay later
        if (!_input.characterControl && _input.ContinuePressed)
        {
            // Close UI talking
            talkUI = false;
                
            // Resume Animations
            pAnim.isTalking = false;
                
            // Resume movement
            _input.characterControl = true;
        }
    }
}
