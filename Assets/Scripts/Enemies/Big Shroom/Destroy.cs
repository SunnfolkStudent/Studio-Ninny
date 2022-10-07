using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    private BigDie _big;
    
    void Start()
    {
        _big = GetComponentInChildren<BigDie>();
    }

    void Update()
    {
        if (_big.health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
