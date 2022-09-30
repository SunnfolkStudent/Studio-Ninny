using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Slash : MonoBehaviour
{
    public float slashTime = 0.3f;
    public float slashTimer;
    
    private BoxCollider2D _clipCol;
    private SpriteRenderer _clipSprite;
    
    
    void Start()
    {
        _clipCol = GetComponent<BoxCollider2D>();
        _clipSprite = GetComponent<SpriteRenderer>();
        
        _clipCol.enabled = false;
        _clipSprite.enabled = false;
    }

    
    void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            slashTimer = Time.time + slashTime;
            _clipCol.enabled = true;
            _clipSprite.enabled = true;
        }

        if (slashTimer < Time.time)
        {
            _clipCol.enabled = false;
            _clipSprite.enabled = false;
        }
    }
}
