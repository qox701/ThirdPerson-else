using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


namespace Controller
{
    [RequireComponent(typeof(PlayerInput))]
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(PlayerAnimationCurve))]
    public class PlayerController : MonoBehaviour
    {
        

        
        #region Internal Components Refs
        private PlayerInput PlayerInput { get;set; }
        private PlayerStateMachine StateMachine { get; set; }
        public Rigidbody Rigidbody { get;  set; }
        public PlayerAnimationCurve PlayerAnimationCurve { get; private set; }
        #endregion
        
        #region InputActions
        public InputAction MoveStick { get;private set; }
        private InputAction LookStick { get; set; }
        public InputAction FireButton { get; private set; }
        #endregion
        
        #region Movement

        public float MaxSpeed = 5f;
        public Vector3 MoveDir { get; private set; }
        public Vector3 Velocity=> Rigidbody.velocity;
        
        #endregion

        #region MonoBehaviours
        void OnEnable()
        {
            PlayerInput = GetComponent<PlayerInput>();
            Rigidbody= GetComponent<Rigidbody>();
            PlayerAnimationCurve = GetComponent<PlayerAnimationCurve>();
            
            MoveStick = PlayerInput.actions["Move"];
            LookStick = PlayerInput.actions["Look"];
            FireButton = PlayerInput.actions["Fire"];
            
            StateMachine = new PlayerStateMachine(this);
            StateMachine.Initialize();

            MoveStick.performed += OnMove;
            
            
            StateMachine.TransitTo("Move");
        }
        
        void OnDisable()
        {
            StateMachine.Disable();
        }

        private void FixedUpdate()
        {
            StateMachine.FixedUpdate();
        }

        private void Update()
        {
            StateMachine.Update();
        }
        #endregion

        

        #region Input Handling
        private void OnMove(InputAction.CallbackContext context)
        {
            MoveDir = new Vector3(context.ReadValue<Vector2>().x,0, context.ReadValue<Vector2>().y);
            MoveDir = MoveDir.normalized;
        }
        

        #endregion
    }
}
