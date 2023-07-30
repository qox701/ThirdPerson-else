using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachineSys;

namespace Controller
{
    public class PlayerStateMachine : StateMachine
    {
        public readonly PlayerController _playerController;
        
        public PlayerStateMachine(PlayerController playerController):base()
        {
            _playerController = playerController;
        }
        
        public override void Initialize()
        {
            base.Initialize();
            StatePool.Add("Move", new Player_Move(this));
            //StatePool.Add("Controlled", new ControlledState(this));
        }
        
        public override void Enable()
        {
            CurrentState.Enter();
        }
        
        public override void Disable()
        {
            CurrentState.Exit();
        }
    }
}

