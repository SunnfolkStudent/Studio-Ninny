using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantDestroy : MonoBehaviour
{
    private ToungePlant _plant;
    
    void Start()
    {
        _plant = GetComponentInChildren<ToungePlant>();
    }

    void Update()
    {
        if (_plant.health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
