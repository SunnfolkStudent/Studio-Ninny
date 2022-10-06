using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleEnemy : MonoBehaviour
{
    public float moveSpeed;

    public float knockbackX = 3f;
    public float knockbackY = 3f;

    public float canMoveTime = 0.7f;
    public float canMoveTimer;

    public bool isHit;

    public int health = 2;

    private Rigidbody2D _rb;
    private BoxCollider2D _bc;
    private SpriteRenderer _sr;
    private Animator _anim;
    
    
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _bc = GetComponent<BoxCollider2D>();
        _sr = GetComponent<SpriteRenderer>();
        _anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (canMoveTimer < Time.time)
        {
             _rb.velocity = new Vector2(moveSpeed, _rb.velocity.y);
             _anim.Play("BlueWalk");
        }
        
        
        if (isHit)
        {
            canMoveTimer = Time.time + canMoveTime;
            health--;
            isHit = false;
        }

        
        if (health < 1)
        {
            Destroy(gameObject);
        }
        
        // Color change when hit
        if (canMoveTimer > Time.time)
        { _sr.color = Color.red; }
        else
        { _sr.color = Color.white; }
    }

    public void Knockback()
    {
        _rb.velocity = moveSpeed < 0 ? new Vector2(knockbackX, _rb.velocity.y) : new Vector2(-knockbackX, _rb.velocity.y);
        canMoveTimer = Time.time + canMoveTime;
    }
    
    public void Destroy()
    {
        Destroy(gameObject);
    }
    
    public void Flip()
    {
        moveSpeed *= -1;
    }
}
