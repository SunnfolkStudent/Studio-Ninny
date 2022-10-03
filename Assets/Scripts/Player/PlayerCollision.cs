using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollision : MonoBehaviour

{
        public LayerMask WhatIsGround;
        public LayerMask WhatIsWall;

        private Rigidbody2D _rb;
        private BoxCollider2D _boxCollider2D;
        
        
        private void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        /*private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag($"Death"))
            {
                //_checkPointController.LoadPosition();
                RestartScene();
            }
        }*/

        private void RestartScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public bool IsGrounded()
            {
                var position = transform.position;
                var direction = Vector2.down;
                const float distance = 0.7f;
                const float rayOffset = 0.29f;
                const float colOffset = -0.05f;
        
                Debug.DrawRay(position, direction, new Color(1f, 0f, 1f));
                var hit = Physics2D.Raycast(position+ new Vector3(colOffset, 0, 0), direction, distance, WhatIsGround);
                var hitLeft = Physics2D.Raycast(position + new Vector3(rayOffset + colOffset, 0, 0), direction, distance, WhatIsGround);
                var hitRight = Physics2D.Raycast(position + new Vector3(-rayOffset + colOffset, 0, 0), direction, distance, WhatIsGround);
        
                if (hitLeft == true || hitRight == true || hitLeft == true)
                {
                    return true;
                } else
                {
                    return false;
                }
            }
        
        public bool IsWallingLeft()
        {
            var position = transform.position;
            var directionLeft = Vector2.left;
            const float distance = 0.55f;
            const float rayOffset = 0.6f;
                
            //See where it at, no actual use
            Debug.DrawRay(position, directionLeft, new Color(1f, 1f, 0f));
                
            //Draw raycasts to the left
            var hitLeft = Physics2D.Raycast(position, directionLeft, distance, WhatIsWall);
            var hitLeftUp = Physics2D.Raycast(position + new Vector3(0f, rayOffset, 0), directionLeft, distance, WhatIsWall);
            var hitLeftDown = Physics2D.Raycast(position + new Vector3(0f, -rayOffset, 0), directionLeft, distance, WhatIsWall);
        
            //if raycasts hit a wall, while in the air and moving downwards, return true
            if (IsGrounded() || _rb.velocity.y > 0)
            {
                return false;
            }
            else if (hitLeft || hitLeftUp || hitLeftDown)
            {
                return true;
            }
            else
            {
                return false;
            }
                
        }
        
        public bool IsWallingRight()
        {
            var position = transform.position;
            var directionRight = Vector2.right;
            const float distance = 0.55f;
            const float rayOffset = 0.49f;
                
            //See where it at, no actual use
            Debug.DrawRay(position, directionRight, new Color(1f, 1f, 0f));
        
            //Draw raycasts to the right
            var hitRight = Physics2D.Raycast(position, directionRight, distance, WhatIsWall);
            var hitRightUp = Physics2D.Raycast(position + new Vector3(0f, rayOffset, 0), directionRight, distance, WhatIsWall);
            var hitRightDown = Physics2D.Raycast(position + new Vector3(0f, -rayOffset, 0), directionRight, distance, WhatIsWall);
                
            //if raycasts hit a wall, while in the air and moving downwards, return true
            if (IsGrounded() || _rb.velocity.y > 0)
            { return false; }
            else if (hitRight || hitRightDown || hitRightUp)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        
        public bool IsWalling()
        {
            if (IsWallingLeft() || IsWallingRight())
            {
                return true;
            }
            else
            { return false; }
        }
        
    }

