using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class FPopup : MonoBehaviour
{
    public bool isActive = false;
    public SpriteRenderer spriteRenderer;

    void Start()
    {
        GetComponentInParent<BoxCollider2D>();
        GetComponent<SpriteRenderer>().enabled = false;
        isActive = false;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isActive = true;
            GetComponent<SpriteRenderer>().enabled = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
         if (!other.CompareTag("Player"))
         {
             isActive = false;
             GetComponent<SpriteRenderer>().enabled = false;
         }
    }
}
