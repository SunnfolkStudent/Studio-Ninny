using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSpikes : MonoBehaviour
{
    public float spikeMove;
    
    void Update()
    {
        transform.position += new Vector3(spikeMove * Time.deltaTime, 0, 0);
    }
}
