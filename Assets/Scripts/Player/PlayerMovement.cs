using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(PlayerCollision))]
public class PlayerMovement : MonoBehaviour
{
    #region Variables
    
    [Header("Movement")]
    public float groundAcceleration = 1f;
    public float airAcceleration = 0.5f;
    public float groundFriction = 0.1f;
    public float airFriction = 0.1f;
    public float wallFriction = 1.5f;

    private Vector2 _currentVelocity;
    private float _moveSpeed;
    
    [Header("Jump")] 
    public float jumpForce = 10f;
    public float pogoForce = 7f;

    public bool isPogoing;
    
    public float wallJumpForceY = 10f;
    public float wallJumpForceX = 6;
    // public bool isWallJumping;

    public float disableMove;
    public float disableTimeWall = 0.3f;
    public float disableTimeBounce = 0.5f;

    public float jumpTimer;
    public float jumpKindness = 0.07f;

    [Header("Restrictions")] 
    public float maxVelocityY = 16f;
    public float maxVelocityX = 6f; // normal max

    [Header("Gravity")] 
    public float norGravity = 1.3f;
    public float fallGravity = 3f;
    public float noGravity = 0f;
    
    [Header("Components")]
    private PlayerInput _input;
    private Rigidbody2D _rb;
    private PlayerCollision _pCol;
    //private BoxCollider2D _bCol;
    
    #endregion
    
    // GetComponents
    private void Start()
    {
        _pCol = GetComponent<PlayerCollision>();
        _input = GetComponent<PlayerInput>();
        _rb = GetComponent<Rigidbody2D>();
        //_bCol = GetComponent<BoxCollider2D>();
    }

    
    void Update()
    {
        UpdateGravity();
        
        UpdateJumping();

        // Vil egentlig ha den i fixed update, men inputs er ikke enig
        UpdateMovement();
    }

    private void UpdateGravity()
    {
        // If grounded, ground gravity
        if (_pCol.IsGrounded() || ((isPogoing || _input.JumpHeld) && _rb.velocity.y > 0))
        {
            _rb.gravityScale = norGravity;
        }
        // If walling, slide
        else if (_pCol.IsWalling() && !_input.JumpPressed)
        {
            _rb.gravityScale = noGravity;
            _rb.velocity = Vector2.down * wallFriction;
        }
        // If falling, fallGravity
        else if (!_pCol.IsGrounded())
        {
            _rb.gravityScale = fallGravity;
        }

        if (_rb.velocity.y <= 0)
        {
            isPogoing = false;
        }
    }

    private void UpdateJumping()
    {
        #region Normal Jump

        if (_input.JumpPressed)
        {
            jumpTimer = Time.time + jumpKindness;
        }
        if ((jumpTimer >= Time.time) && _pCol.IsGrounded())
        {
            _rb.velocity = new Vector2(_rb.velocity.x, jumpForce);
        }

        #endregion
        
        #region WallJump

        if (_pCol.IsWalling() && _input.JumpPressed)
        {
            disableMove = Time.time + disableTimeWall;
            _rb.velocity = new Vector2(_pCol.IsWallingLeft() ? wallJumpForceX : -wallJumpForceX,
                wallJumpForceY);
            // isWallJumping = true;
        } 

        #endregion
    }

    private void UpdateMovement()
    {
        // disable movement if needed
        // Vil egentlig bruke velocity for restriksjoner
        
        /*if (disableMove >= Time.time)
        {
            return;
        }*/

        
        // Store Rigidbody2D.Velocity in _velocity
        _currentVelocity = _rb.velocity;
        _currentVelocity.y = Mathf.Clamp(_currentVelocity.y, -maxVelocityY, maxVelocityY);

        // Acceleration if moving
        if (_input.MoveVector.x != 0)
        {
            _moveSpeed += _input.MoveVector.x * (_pCol.IsGrounded() ? groundAcceleration : airAcceleration);
            _moveSpeed = Mathf.Clamp(_moveSpeed, -maxVelocityX, maxVelocityX);
        }
        // Friction if no button is pressed
        else
        {
            _moveSpeed = Mathf.Lerp(_moveSpeed, 0f, _pCol.IsGrounded() ? groundFriction : airFriction);
        }

        // Apply speed to rigidbody
        _currentVelocity.x = _moveSpeed;
        _rb.velocity = (disableMove >= Time.time) ? _rb.velocity : _currentVelocity;
    }

    public void Pogo()
    {
        _rb.velocity = new Vector2(_rb.velocity.x, pogoForce);
        isPogoing = true;
    }
}
