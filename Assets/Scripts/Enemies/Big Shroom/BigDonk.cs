using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigDonk : MonoBehaviour
{
    private BigDie _big;

    private void Start()
    {
        _big = GetComponent<BigDie>();
    }

    // Hurt
    private void OnTriggerEnter2D(Collider2D col)
    {
        print("lols");
        if (col.CompareTag("Slash"))
        {
            print("lols");
            // Hurt
            _big.health--;

            _big.isHit = true;
        }
    }
}
