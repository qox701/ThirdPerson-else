using System.Collections;
using System.Collections.Generic;
using StateMachineSys;
using UnityEngine;

namespace Controller
{
    public class Player_Damaged : IState
    {
        #region Refs
        private PlayerStateMachine _stateMachine;
        private PlayerController _controller;
        private readonly PlayerAnimationCurve _animationCurve;
        #endregion
        
        private float DamagedTime;
        private float DamagedSpeed;
        private float Timer;

        public Player_Damaged(PlayerStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
            _controller=_stateMachine._playerController;
            _animationCurve = _controller.PlayerAnimationCurve;

            DamagedTime = _animationCurve.DamagedTime;
            DamagedSpeed = _animationCurve.DamagedSpeed;
        }

        public void Enter()
        {
            _controller.Rigidbody.AddForce(_controller.DamageDir*DamagedSpeed, ForceMode.VelocityChange);
            Timer = 0f;
        }
        public void Update(){}

        public void FixedUpdate()
        {
            Timer+= Time.deltaTime;
            if (Timer >= DamagedTime)
                _stateMachine.TransitTo("Move");
        }
        public void Exit(){}
        public void DrawGizmos(){}
    }
}

