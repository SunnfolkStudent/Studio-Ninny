using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleEnemy : MonoBehaviour
{
    public float moveSpeed;
    
    
    private Rigidbody2D _rb;
    private BoxCollider2D _bc;
    
    
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _bc = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        _rb.velocity = new Vector2(moveSpeed, _rb.velocity.y);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.transform.CompareTag("Flip"))
        {
            moveSpeed *= -1;
        }
    }
}
