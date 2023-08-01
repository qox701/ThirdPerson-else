using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachineSys;
using UnityEngine.InputSystem;

namespace Controller
{
    public class Player_Move : IState
    {
        private readonly PlayerStateMachine _stateMachine;
        private readonly PlayerController _controller;
        private readonly PlayerAnimationCurve _animationCurve;
        private Vector3 _moveDirection;
        private float _lookDirectionValue;
        private float _rotateSpeed;
        
        public float _speed=0f;
        public Player_Move(PlayerStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
            _controller = _stateMachine._playerController;
            _animationCurve = _controller.PlayerAnimationCurve;
        }

        public void Enter()
        {
            AddInoutCallbacks();
            
        }
        public void Update(){}

        public void FixedUpdate()
        {
            //Get Value every frame cuz it changes every frame
            _speed = _animationCurve.Speed;
            _rotateSpeed = _animationCurve.RotateSpeed;
            //Get direction every fixed frame
            _moveDirection = _controller.MoveDir;
            _lookDirectionValue = _controller.LookDirValue;
            
            //Move
            _controller.Rigidbody.AddForce(_moveDirection,ForceMode.Acceleration);
            
            _controller.Rigidbody.velocity= _moveDirection.normalized*_speed;
            
            //Rotate
            _controller.transform.Rotate(_controller.transform.up,_lookDirectionValue*Time.deltaTime*_rotateSpeed,Space.Self);
        }

        public void Exit()
        {
            RemoveInputCallbacks();
        }
        public void DrawGizmos(){}

        #region Input Bindings
        private void AddInoutCallbacks()
        {
            _controller.MoveStick.started += OnMoveStart;
            _controller.MoveStick.canceled+= OnMoveCanceled;
            _controller.AtkButton.performed += OnAtk;
        }
        
        private void RemoveInputCallbacks()
        {
            _controller.MoveStick.started -= OnMoveStart;
            _controller.MoveStick.canceled -= OnMoveCanceled;
            _controller.AtkButton.performed -= OnAtk;
        }
        #endregion

        #region Input Callbacks
        private void OnMoveStart(InputAction.CallbackContext context)
        {
            _animationCurve.isChangingSpeed = true;
        }
        
        private void OnMoveCanceled(InputAction.CallbackContext context)
        {
            _animationCurve.isChangingSpeed = false;
        }

        private void OnAtk(InputAction.CallbackContext context)
        {
            _stateMachine.TransitTo("Atk");
        }

        #endregion
    }
}

