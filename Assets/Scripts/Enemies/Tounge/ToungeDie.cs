using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToungeDie : MonoBehaviour
{
    private ToungePlant _plant;
    
    void Start()
    {
        _plant = GetComponent<ToungePlant>();
    }
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        print("enter");
        
        if (col.CompareTag("Slash"))
        {
            // Hurt
            _plant.health--;

            _plant.isHit = true;
        }
    }
}
