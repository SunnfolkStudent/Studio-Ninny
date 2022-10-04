using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Hitbox : MonoBehaviour
{
    private Vector3 _respawnPos;
    
    //TODO: Add actual start pos
    private static Vector3 _deathRespawnPoint = new Vector2(0.5f,-0.25f);
    
    public Transform playerTrans;

    public bool fireplaceEncounterUI;
    public bool fireplaceUI;

    public static int fireplaceScene = 1;
    
    public bool npcInteractUI;
    public bool talkUI;

    private BoxCollider2D _hitbox;
    private PlayerCollision _pCol;
    private PlayerInput _input;
    private PlayerMovement _pMove;
    private PlayerHealth _pHealth;
    
    public PlayerAnimator pAnim;
    
    void Start()
    {
        _hitbox = GetComponent<BoxCollider2D>();

        _pHealth = GetComponentInParent<PlayerHealth>();
        _pMove = GetComponentInParent<PlayerMovement>();
        _pCol = GetComponentInParent<PlayerCollision>();
        _input = GetComponentInParent<PlayerInput>();
        
        _respawnPos = transform.position;

        playerTrans.position = _deathRespawnPoint;
        
        // respawn at _deathRespawnPoint
        _deathRespawnPoint = transform.position;
    }

    // Respawn and "checkpoints"
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("SpawnZone"))
        {
            // if u hit a respawn zone that is now your hazard respawn
            _respawnPos = other.transform.position;
        }
        if (other.CompareTag("DeathHazard"))
        {
            // hurt anim
            
            // health--
            _pHealth.currentHealth--;
            
            // if no life; ded (Spawn at fireplace)
            if (_pHealth.currentHealth <= 0)
            {
                // reload scene
                SceneManager.LoadScene(fireplaceScene);
            }
            else
            {
                //if not, respawn
                playerTrans.position = _respawnPos;
            }
            
            
        }
    }

    // Interaction
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

        #region Fireplace

        if (other.CompareTag("Fireplace") && _pCol.IsGrounded())
        {
            fireplaceEncounterUI = true;
            // Save fireplace pos
            if (pAnim.isResting)
            {
                _deathRespawnPoint = other.transform.position;
            }
        }
        else { fireplaceEncounterUI = false; }

        #endregion
    }

    // Removes npc and fireplace interact UI when leaving hitbox
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Interact") || other.CompareTag("Fireplace"))
        {
            npcInteractUI = false;
            fireplaceEncounterUI = false;
        } 
        
    }

    void Update()
    {
        #region UI interact

        // TODO: Add delayed transition
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

        #region Fireplace

        if (fireplaceEncounterUI)
        {
            if (_input.Interact)
            {
                // dissable UI
                fireplaceEncounterUI = false;
                
                // dissableMove
                _input.characterControl = false;
                
                // Rest Anim
                pAnim.isResting = true;

                // UI Activate
                fireplaceUI = true;

                // restore life
                _pHealth.currentHealth = _pHealth.maxHealth;
                
                // SAve scene
                fireplaceScene = SceneManager.GetActiveScene().buildIndex;
            }
        }

        #endregion
    }
}
