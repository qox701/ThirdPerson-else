using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;
using StateMachine = StateMachineSys.StateMachine;

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
            StatePool.Add("Atk", new Player_Atk(this));
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

