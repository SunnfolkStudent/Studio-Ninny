using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
        [Header("Movement")]
        public float maxMoveSpeed = 6f;
        public float maxVelocityX = 6f;
        public float maxVelocityY= 16f;
        
        public float acceleration = 1f;
        public float groundFriction = 0.3f;
        public float airFriction = 0.005f;
        private Vector2 _currentVelocity;
        private float _moveSpeed;
        
        [Header("Jumping")]
        public float jumpForce = 10f;
        public int maxDoubleJumpValue = 1;
        public float coyoteTime = 0.15f;
        public int _doubleJumpValue;
        
        public float jumpTimeCounter;
        public float jumpTime = 0.25f;
                
        private float _coyoteTimeCounter;
        public bool _isCoyoteTime;
        private bool _isJumping;

       

        [Header("Components")]
        private PlayerInput _input;
        private PlayerCollision _collision;
        private Rigidbody2D _rigidbody2D;

        // Start is called before the first frame update
        private void Start()
        {
            _input = GetComponent<PlayerInput>();
            _collision = GetComponent<PlayerCollision>();
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        private void Update()
        {
            UpdateJumping();
            VariableLongJump();
            
            if (_collision.IsGroundedBox() && _rigidbody2D.velocity.y < 0f)
            {
                _isJumping = false;
                _doubleJumpValue = maxDoubleJumpValue;
            }
        }


        private void FixedUpdate()
        {
            UpdateMovement();
        }
        
        
        private void UpdateJumping()
        {
            VariableLongJump();
            if (!_isJumping && !_collision.IsGroundedBox()) 
            { _coyoteTimeCounter += Time.deltaTime; } else { _coyoteTimeCounter = 0; }
            
            if (_input.JumpPressed 
                && (_collision.IsGroundedBox() || (_coyoteTimeCounter > 0.03f 
                                                   && _coyoteTimeCounter < coyoteTime)))
            {
                _rigidbody2D.velocity = Vector2.up * jumpForce;
                jumpTimeCounter = jumpTime;
                _isJumping = true;
            
            }
            else if (_input.JumpPressed && _doubleJumpValue > 0)
            { 
                _rigidbody2D.velocity = Vector2.up * jumpForce;
                _isJumping = true;
                _doubleJumpValue--;
            }
        }

        private void UpdateMovement()
        {
            //store RigidBody2D.Velocity in _velocity
            _currentVelocity = _rigidbody2D.velocity;
            _currentVelocity.y = Mathf.Clamp(_rigidbody2D.velocity.y, -maxVelocityY, maxVelocityY);

            //change the Velocity
            if (_input.MoveVector.x != 0)
            {
                _moveSpeed += _input.MoveVector.x * acceleration;
                _moveSpeed = Mathf.Clamp(_moveSpeed, -maxMoveSpeed, maxMoveSpeed);
            }
            else
            {
                // LERP: Linear Interpolation: Variable From A to B over T(time)
                                                                        //if onGround, Set Friction to GroundFriction
                                                                        //if !onGround, Set Friction to AirFriction
                _moveSpeed = Mathf.Lerp(_moveSpeed, 0f, _collision.IsGroundedBox() ? groundFriction : airFriction);
            }

            _currentVelocity.x = _moveSpeed;
            //_rigidbody2D.velocity = new Vector2(movespeed, _rigidbody2D.velocity.y);
            //slipper å skrive så jævlig mye kode

            //return current Velocity into RigidBody2D.velocity
            _rigidbody2D.velocity = _currentVelocity;
        }

        private void VariableLongJump()
        {
            if (_input.JumpValue > 0f && _isJumping )
            {
                if (jumpTimeCounter > 0)
                {
                    _rigidbody2D.velocity = Vector2.up * jumpForce;
                    jumpTimeCounter -= Time.deltaTime;
                }
            }
        }
}
