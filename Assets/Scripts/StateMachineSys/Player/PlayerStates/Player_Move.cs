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
            _speed = _animationCurve.Speed;
            _moveDirection = _controller.MoveDir;
            _controller.Rigidbody.AddForce(_moveDirection,ForceMode.Acceleration);
            _controller.Rigidbody.velocity= _moveDirection.normalized*_speed;
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
        }
        
        private void RemoveInputCallbacks()
        {
            _controller.MoveStick.started -= OnMoveStart;
            _controller.MoveStick.canceled -= OnMoveCanceled;
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

        #endregion
    }
}

