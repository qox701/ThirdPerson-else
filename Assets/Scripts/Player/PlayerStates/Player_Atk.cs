using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachineSys;
using UnityEngine.InputSystem;

namespace Controller
{
    public class Player_Atk : IState
    {
        #region Refs
        private PlayerStateMachine _stateMachine;
        private PlayerController _controller;
        //private GameObject _atkCollider;
        private readonly PlayerAnimationCurve _animationCurve;
        #endregion

        private float AtkTime;
        private float AtkSpeed;
        private float Timer;
        
        public Player_Atk(PlayerStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
            _controller=_stateMachine._playerController;
            _animationCurve = _controller.PlayerAnimationCurve;

            AtkTime = _animationCurve.AtkTime;
            AtkSpeed = _animationCurve.AtkSpeed;
        }

        public void Enter()
        {
            _controller.Rigidbody.AddForce(_controller.transform.forward*AtkSpeed, ForceMode.VelocityChange);
            //_atkCollider.SetActive(true);
            Timer=0f;
        }
        public void Update(){}

        public void FixedUpdate()
        {
            Timer+= Time.deltaTime;
            if (Timer >= AtkTime||_controller.Rigidbody.velocity.magnitude==0)
                _stateMachine.TransitTo("Move");
        }

        public void Exit()
        {
            //_atkCollider.SetActive(false);
            
        }
        public void DrawGizmos(){}
        
        
    }
}

