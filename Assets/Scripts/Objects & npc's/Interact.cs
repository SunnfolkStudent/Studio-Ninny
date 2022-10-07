using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    private Animator _anim;
    
    public Hitbox pHit;
    
    void Start()
    {
        _anim = GetComponent<Animator>();
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag("Player") && pHit.talkUI)
        {
            _anim.Play("Talking");
        }
        else
        {
            _anim.Play("Idle");
        }
    }
}
