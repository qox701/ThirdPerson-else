using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Managers
{
    public class MonoController : MonoBehaviour
    {
        private event UnityAction updateEvent;

        private void Start()
        {
            DontDestroyOnLoad(this.gameObject);
        }

        private void Update()
        {
            updateEvent?.Invoke();
        }

        public void AddListenerToUpdate(UnityAction action)
        {
            updateEvent += action;
        }

        public void RemoveListenerToUpdate(UnityAction action)
        {
            updateEvent -= action;
        }
    }
}
