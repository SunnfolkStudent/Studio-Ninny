using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Interact : MonoBehaviour
{
    private Animator _anim;
    
    public SpriteRenderer spriteRenderer;
    public Hitbox pHit;
    
    public bool isTalking;

    void Start()
    {
        _anim = GetComponent<Animator>();

        spriteRenderer.enabled = false;
        _anim.Play("Idle");
    }

    void Update()
    {
        if (pHit.npcInteractUI && !pHit.talkUI)
        {
            spriteRenderer.enabled = true;
            
            if (_anim.GetCurrentAnimatorStateInfo(0).IsName("Talking"))
            {
                _anim.Play("Idle");
            }

            isTalking = false;
        } 
        else if (pHit.talkUI)
        {
            spriteRenderer.enabled = false;
            _anim.Play("Talking");
            
            isTalking = true;
        }
        else
        {
            spriteRenderer.enabled = false;
            _anim.Play("Idle");

            isTalking = false;
        }
    }
}
