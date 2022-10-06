using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(PlayerCollision))]
public class PlayerMovement : MonoBehaviour
{
    #region Variables

    [Header("Hurt")] 
    public float hitForceX = 4f;
    public float hitForceY = 6f;
    public float disableTimeHurt = 0.3f;
    
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
    public bool isJumping;
    
    public float wallJumpForceY = 10f;
    public float wallJumpForceX = 6;
    // public bool isWallJumping;

    public float disableMove;
    public float disableTimeWall = 0.3f;

    public float jumpTimer = -1f;
    public float jumpKindness = 0.07f;
    
    [Header("CoyoteTime")] 
    public float coyoteTimeTimer;
    public float coyoteTimeTime = 0.2f;

    public bool coyoteTime;
    
    [Header("Restrictions")] 
    public float maxVelocityY = 16f;
    public float maxVelocityX = 6f;

    [Header("Gravity")] 
    public float norGravity = 1.3f;
    public float fallGravity = 3f;
    public float noGravity = 0f;
    
    [Header("Components")]
    private PlayerInput _input;
    private Rigidbody2D _rb;
    private PlayerCollision _pCol;
    private PlayerAnimator _pAnim;
    //private BoxCollider2D _bCol;

    private SpriteRenderer _pRenderer;
    
    #endregion
    
    // GetComponents
    private void Start()
    {
        _pCol = GetComponent<PlayerCollision>();
        _input = GetComponent<PlayerInput>();
        _rb = GetComponent<Rigidbody2D>();
        //_bCol = GetComponent<BoxCollider2D>();

        _pRenderer = GetComponentInChildren<SpriteRenderer>();
        _pAnim = GetComponentInChildren<PlayerAnimator>();
    }

    
    void Update()
    {
        UpdateGravity();
    
        UpdateJumping();

        UpdateMovement();

        if (disableMove < Time.time)
        {
            _pRenderer.color = Color.white;
        }
    }

    private void UpdateGravity()
    {
        // If grounded or jumping upwards, nor gravity
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

        // Checks
        if (_rb.velocity.y <= 0)
        {
            isPogoing = false;
        }

        if (_pCol.IsGrounded())
        {
            isJumping = false;
        }
    }

    private void UpdateJumping()
    {
        #region CoyoteTime

        if (!isJumping && !_pCol.IsGrounded())
        {
            coyoteTimeTimer += Time.deltaTime;
        }
        else { coyoteTimeTimer = 0; }

        if (coyoteTimeTimer < coyoteTimeTime && !isJumping && !_pCol.IsGrounded())
        {
            coyoteTime = true;
        }
        else { coyoteTime = false; }

        #endregion
        
        #region Normal Jump
        
        if (_input.JumpPressed)
        {
            jumpTimer = Time.time + jumpKindness;
        }
        
        if ((jumpTimer >= Time.time) && (_pCol.IsGrounded() || coyoteTime))
        {
            _rb.velocity = new Vector2(_rb.velocity.x, jumpForce);
            coyoteTime = false;
            isJumping = true;
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

    public void Hurt()
    {
        disableMove = Time.time + disableTimeHurt;
        
        _rb.velocity = new Vector2(_pAnim.isFacingLeft ? hitForceX : -hitForceX, hitForceY);
        
        _pRenderer.color = Color.red;
    }
}
