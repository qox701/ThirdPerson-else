using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;


namespace StateMachineSys
{
    public abstract class StateMachine 
    {
        
        public IState CurrentState { get; private set; }

        protected Dictionary<string, IState> StatePool;

        public StateMachine()
        {
            StatePool = new Dictionary<string, IState>();
        }

        public virtual void Initialize()
        {
            
        }

        public virtual void Enable()
        {
            CurrentState.Enter();
        }
        
        public virtual void Disable()
        {
            CurrentState.Exit();
        }
        
        public void TransitTo(IState nextState)
        {
            CurrentState?.Exit();
            CurrentState = nextState;
            CurrentState.Enter();
        }
        
        public void TransitTo(string nextStateName)
        {
            TransitTo(StatePool[nextStateName]);
        }
        
        public void Update()
        {
            CurrentState?.Update();
        }

        public void FixedUpdate()
        {
            CurrentState?.FixedUpdate();
        }
        #if UNITY_EDITOR
        public void OnDrawGizmos()
        {
            CurrentState?.DrawGizmos();
        }
        #endif
    }
}
