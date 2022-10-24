using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    private BigDie _big;
    private static bool bigDead;
    
    void Start()
    {
        _big = GetComponentInChildren<BigDie>();
        
        if (bigDead) { Destroy(gameObject); }
    }

    void Update()
    {
        if (_big.health <= 0)
        {
            Destroy(gameObject);
            bigDead = true;
        }
    }
}
