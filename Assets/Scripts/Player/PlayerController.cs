using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Utilities;

namespace Controller
{
    [RequireComponent(typeof(PlayerInput))]
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(PlayerAnimationCurve))]
    public class PlayerController : MonoBehaviour,ICanDamaged
    {
        #region Internal Components Refs
        private PlayerInput PlayerInput { get;set; }
        private PlayerStateMachine StateMachine { get; set; }
        public Rigidbody Rigidbody { get;  set; }
        public PlayerAnimationCurve PlayerAnimationCurve { get; private set; }
        public GameObject AtkCollider;
        #endregion
        
        #region InputActions
        public InputAction MoveStick { get;private set; }
        private InputAction LookStick { get; set; }
        public InputAction AtkButton { get; private set; }
        #endregion
        
        #region MonoBehaviours
        void OnEnable()
        {
            Cursor.visible = false;
            
            PlayerInput = GetComponent<PlayerInput>();
            Rigidbody= GetComponent<Rigidbody>();
            PlayerAnimationCurve = GetComponent<PlayerAnimationCurve>();
            AtkCollider=transform.GetChild(0).gameObject;
            AtkCollider.SetActive(false);
            
            MoveStick = PlayerInput.actions["Move"];
            LookStick = PlayerInput.actions["Look"];
            AtkButton = PlayerInput.actions["Fire"];
            
            StateMachine = new PlayerStateMachine(this);
            StateMachine.Initialize();

            MoveStick.performed += OnMove;
            LookStick.performed += OnLook;

            StateMachine.TransitTo("Move");
        }
        
        void OnDisable()
        {
            StateMachine.Disable();
            MoveStick.performed -= OnMove;
            LookStick.performed -= OnLook;
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

        #region Movement
        public Vector3 MoveDir { get; private set; }
        public float LookDirValue { get; private set; }
        public Vector3 Velocity=> Rigidbody.velocity;
        
        #endregion

        #region Input Handling
        private void OnMove(InputAction.CallbackContext context)
        {
            //Get Move Direction Input when MoveStick is performed
            GetMoveDirection();
        }
        
        private void OnLook(InputAction.CallbackContext context)
        {
            //Get the Look Direction Input when LookStick is performed
            if (Mathf.Abs(LookStick.ReadValue<Vector2>().x) < 5)
            {
                LookDirValue = 0;
                return;
            }
            LookDirValue = LookStick.ReadValue<Vector2>().x;
            
            //Refresh MoveDir when LookStick is performed
            //If you dont refresh MoveDir, the player will move in the direction of the last MoveStick input
            //Which is usually performed frames before
            GetMoveDirection();
        }

        private void GetMoveDirection()
        {
            MoveDir=transform.forward*MoveStick.ReadValue<Vector2>().y+transform.right*MoveStick.ReadValue<Vector2>().x;
            MoveDir = MoveDir.normalized;
        }

        public Vector3 DamageDir { get;private set; }
        
        public void OnDamaged(Vector3 damagePos)
        {
            DamageDir = Vector3.Normalize(transform.position - damagePos);
            StateMachine.TransitTo("Damaged");
        }

        #endregion
    }
}
