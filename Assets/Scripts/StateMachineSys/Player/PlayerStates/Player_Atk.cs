using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachineSys;
using UnityEngine.InputSystem;

namespace Controller
{
    public class Player_Atk : IState
    {
        private PlayerStateMachine _stateMachine;
        private PlayerController _controller;
        private GameObject _atkCollider;
        private readonly PlayerAnimationCurve _animationCurve;
        public Player_Atk(PlayerStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
            _controller=_stateMachine._playerController;
            _animationCurve = _controller.PlayerAnimationCurve;
        }

        public void Enter()
        {
            _controller.Rigidbody.AddForce(_controller.MoveDir, ForceMode.Impulse);
            _atkCollider.SetActive(true);
        }
        public void Update(){}
        public void FixedUpdate(){}

        public void Exit()
        {
            _atkCollider.SetActive(false);
        }
        public void DrawGizmos(){}
        
        
    }
}

