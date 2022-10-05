using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerEnergy : MonoBehaviour
{
    public int Energy;
    public int numOfEnergy;

    public Image[] energy;
    public Sprite fullEnergy;
    public Sprite emptyEnergy;

    void Update()
    {
        if (Energy > numOfEnergy)
        {
            Energy = numOfEnergy;
        }
        
        
        for (int i = 0; i < energy.Length; i++)
        {
            if (i < Energy)
            { energy[i].sprite = fullEnergy; }
            else
            { energy[i].sprite = emptyEnergy; }
            
            if (i < numOfEnergy)
            { energy[i].enabled = true; }
            else
            { energy[i].enabled = false; }
        }
    }
}