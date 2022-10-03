using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour
{
    private Vector3 _respawnPos;
    private Vector3 _deathRespawnPoint;
    
    public Transform playerTrans;

    public bool fireplaceUI;
    public bool npcInteractUI;
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
        _deathRespawnPoint = transform.position;
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
        #region NPC interaction

        if (other.CompareTag("Interact") && _pCol.IsGrounded())
        {
            // UI interact popup and tell update to run
            npcInteractUI = true;
        }
        // if !grounded remove UI
        else { npcInteractUI = false; }

        #endregion

        if (other.CompareTag("Fireplace") && _pCol.IsGrounded())
        {
            fireplaceUI = true;
            
            if (_input.Interact)
            {
                // Rest Anim
                // dissableMove
                _input.characterControl = false;
                // 
                _deathRespawnPoint = other.transform.position;
            }
        }
    }

    // removes npc interact UI
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Interact"))
        {
            npcInteractUI = false;
        } 
    }

    void Update()
    {
        #region UI interact

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

        #endregion

        #region NPC interaction

        if (npcInteractUI)
        {
            if (_input.Interact)
            {
                // removes the first UI
                npcInteractUI = false;
                
                // Dissable move
                _input.characterControl = false;
            
                // Play interact anim
                pAnim.isTalking = true;
            
                // UI popup
                talkUI = true;
            }
        }

        #endregion
    }
}
