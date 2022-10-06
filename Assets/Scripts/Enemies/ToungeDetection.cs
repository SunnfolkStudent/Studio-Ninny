using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToungeDetection : MonoBehaviour
{

    public ToungePlant parent;

    // Starts attacking
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && parent.attackTimer < Time.time)
        {
            parent.isActive = true;
        }
    }

    // Stops attacking
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            parent.isActive = false;
        }
    }
}
