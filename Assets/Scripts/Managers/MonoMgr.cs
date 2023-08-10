using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Internal;

namespace Managers
{
    public class MonoMgr
    {
        private static MonoMgr _instance;
        public static MonoMgr Instance => _instance ??= new MonoMgr();
        
        private MonoController controller;

        public MonoMgr()
        {
            GameObject obj = new GameObject("MonoController");
            controller = obj.AddComponent<MonoController>();
        }
        
        public void AddUpdateListener(UnityAction action)
        {
            controller.AddListenerToUpdate(action);
        }
        
        public void RemoveUpdateListener(UnityAction action)
        {
            controller.RemoveListenerToUpdate(action);
        }
        
        public Coroutine StartCoroutine(IEnumerator routine)
        {
            return controller.StartCoroutine(routine);
        }
        
        public Coroutine StartCoroutine(string methodName,[DefaultValue("null")]object value)
        {
            return controller.StartCoroutine(methodName, value);
        }
        
        public Coroutine StartCoroutine(string methodName)
        {
            return controller.StartCoroutine(methodName);
        }
    }
}
