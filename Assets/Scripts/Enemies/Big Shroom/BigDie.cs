using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BigDie : MonoBehaviour
{
    public int health = 8;

    public float attackTimer;
    public float attackDelay = 2f;

    public bool isActive;
    public bool isHit;
    public bool canAttack;

    public float hurtTimer;
    public float hurtTime = 0.2f;
    
    private BoxCollider2D _attackBox;
    
    private Animator _anim;
    private SpriteRenderer _bigRenderer;
    

    void Start()
    {
        _attackBox = GetComponent<BoxCollider2D>();
        
        _anim = GetComponentInParent<Animator>();
        _bigRenderer = GetComponentInParent<SpriteRenderer>();
        
        _attackBox.enabled = false;
        
        _anim.Play("Idle");
    }

    
    void Update()
    {
        // Hit
        if (isHit)
        {
            hurtTimer = Time.time + hurtTime;
            
            isHit = false;
        }
        
        // Color change when hit
        if (hurtTimer > Time.time)
        { _bigRenderer.color = Color.red; }
        else
        { _bigRenderer.color = Color.white; }
                
        
        if (isActive)
        {
            _anim.Play("Stand");
            isActive = false;
            canAttack = true;
            print("stand");
        }

        
        if (canAttack && attackTimer < Time.time)
        {
            //Attack
            _anim.Play("Attack");
            
            _attackBox.enabled = true;
            // only visualization, hope for new animation
            transform.GetChild(0).gameObject.SetActive(true); 
            
            attackTimer = Time.time + attackDelay;
            
        }
        else if (canAttack && attackTimer - 1.5f < Time.time)
        {
            _anim.Play("IdleStand");
            _attackBox.enabled = false;
            // only visualization, hope for new animation
            transform.GetChild(0).gameObject.SetActive(false); 
        }

        if (!canAttack)
        {
            _anim.Play("Down");
        }
    }
}
