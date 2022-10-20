using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
        #region INITIALIZE INPUT

        //private InputActions _inputActions;
        private InputTest _inputActions;
        private void Awake() => _inputActions = new InputTest();
        private void OnEnable() => _inputActions.Enable();
        private void OnDisable() => _inputActions.Disable();

        #endregion

        #region INPUT VARIABLES

        public bool characterControl;
        
        public Vector2 MoveVector { get; private set; }
        public Vector2 LookVector { get; private set; }

        public float JumpValue { get; private set; }
        public bool JumpHeld { get; private set; }
        public bool JumpReleased { get; private set; }
        public bool JumpPressed { get; private set; }

        public bool AttackPressed { get; private set; }
        
        public InputAction Jump { get; private set; }

        public float InteractValue { get; private set; }
        public bool Interact { get; private set; }
        
        public bool ContinuePressed { get; private set; }

        public bool escPressed { get; private set; }
        
        
        #endregion

        private void Start()
        {
            characterControl = true;
        }

        private void Update()
        {
            escPressed = _inputActions.Player.Pause.WasPressedThisFrame();
            
            if (characterControl)
            {
                MoveVector = _inputActions.Player.Move.ReadValue<Vector2>();
                LookVector = _inputActions.Player.Look.ReadValue<Vector2>();

                JumpValue = _inputActions.Player.Jump.ReadValue<float>();
                JumpHeld = _inputActions.Player.Jump.inProgress;
                JumpPressed = _inputActions.Player.Jump.triggered;
                JumpReleased = _inputActions.Player.Jump.WasReleasedThisFrame();

                AttackPressed = _inputActions.Player.Attack.WasPressedThisFrame();

                Jump = _inputActions.Player.Jump;

                InteractValue = _inputActions.Player.Interact.ReadValue<float>();
                Interact = _inputActions.Player.Interact.triggered;
                
                
            }
            
            else // menus, interactions, etc.
            {
                // reset values
                MoveVector = Vector2.zero;
                LookVector = Vector2.zero;

                JumpValue = 0;
                InteractValue = 0;
                
                // press left-mouse to continue / select
                ContinuePressed = _inputActions.Player.Interact.WasPressedThisFrame();
            }
        }
}
