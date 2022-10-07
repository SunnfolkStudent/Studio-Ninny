using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Child : MonoBehaviour
{
    private SimpleEnemy _not;

    private void Start()
    {
        _not = GetComponentInParent<SimpleEnemy>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Slash"))
        {
            _not.isHit = true;
        }
        else
        {
            _not.isHit = false;
        }

        if (col.CompareTag("Spikes"))
        {
            _not.Destroy();
        }
        
        if (col.transform.CompareTag("Flip"))
        {
            _not.Flip();
        }
    }
}
