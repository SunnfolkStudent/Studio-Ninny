using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigDetection : MonoBehaviour
{
    public BigDie big;

    // Starts attacking
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            big.isActive = true;
        }
    }

    // Stops attacking
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            big.canAttack = false;
        }
    }
}
