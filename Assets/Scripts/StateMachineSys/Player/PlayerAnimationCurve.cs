using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Controller
{
    public class PlayerAnimationCurve : MonoBehaviour
    {
        #region Curves
        [SerializeField] private AnimationCurve _speedCurve;
        [SerializeField] private AnimationCurve _roundCurve;

        [SerializeField] private float totalTime=1f;
        private float _timer;
        private float _normalizeTime;
        #endregion
        
        public float Speed { get; private set; }
        private float _roundSpeed;

        public bool isChangingSpeed;
        Coroutine myCouroutine;
        
        //public PlayerController _playerController;

        private void OnEnable()
        {
            
        }

        private void Update()
        {
            //_playerController.speed = Speed;
            if (isChangingSpeed)
            {
                //Debug.Log(Speed);
                if (myCouroutine!=null)
                {
                    return;
                }
                else
                {
                    myCouroutine = StartCoroutine(ChangeSpeed());
                }
            }
            else
            {
                Speed = 0f;
                
                if (myCouroutine!=null)
                {
                    StopCoroutine(myCouroutine);
                    
                    myCouroutine = null;
                }
            }
        }
        
        #region Read Curve
        private IEnumerator ChangeSpeed()
        {
            _timer = 0;
            while (_timer <= totalTime)
            {
                _normalizeTime= _timer / totalTime;
                _timer+= Time.deltaTime;
                Speed= _speedCurve.Evaluate(_normalizeTime) ;
                
                yield return null;
            }
        }
        
        private IEnumerator ChangeRoundSpeed()
        {
            _timer = 0;
            while (_timer <= totalTime)
            {
                _normalizeTime= _timer / totalTime;
                _timer+= Time.deltaTime;
                _roundSpeed= _roundCurve.Evaluate(_normalizeTime)* 100;
                yield return null;
            }
        }
        #endregion
    }
}
