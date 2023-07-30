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
        [SerializeField] private AnimationCurve _speedDownCurve;
        [SerializeField] private AnimationCurve _roundCurve;

        [SerializeField] private float speedUpTotalTime=1f;
        [SerializeField] private float speedDownTotalTime=1f;
        private float _timer;
        private float _normalizeTime;
        #endregion
        
        public float Speed { get; private set; }
        public float maxSpeed = 5f;
        private float _roundSpeed;

        public bool isChangingSpeed;
        public bool isAccelerating=false;
        //public bool isDecelerating;
        Coroutine myCouroutine;
        

        private void OnEnable()
        {
            
        }

        private void Update()
        {
            if (isChangingSpeed)
            {
                //Debug.Log(Speed);
                if (isAccelerating)
                {
                    return;
                }
                else
                {
                    isAccelerating = true;
                    if (myCouroutine!=null)
                    {
                        StopCoroutine(myCouroutine);
                    }
                    _timer = 0;
                    //ChangeStartSpeed();
                    myCouroutine = StartCoroutine(ChangeSpeed());
                }
            }
            else
            {
                if (!isAccelerating)
                {
                    return;
                }
                else
                {
                    isAccelerating = false;
                    if (myCouroutine!=null)
                    {
                        StopCoroutine(myCouroutine);
                    }
                    _timer = 0;
                    myCouroutine = StartCoroutine(ChangeSpeedDown());
                }
            }
        }
        
        #region Read Curve
        private IEnumerator ChangeSpeed()
        {
            _timer = 0;
            while (_timer <= speedUpTotalTime)
            {
                _normalizeTime= _timer / speedUpTotalTime;
                _timer+= Time.deltaTime;
                Speed= _speedCurve.Evaluate(_normalizeTime)*maxSpeed ;
                
                yield return null;
            }
        }
        
        private IEnumerator ChangeSpeedDown()
        {
            while (_timer <= speedDownTotalTime)
            {
                _normalizeTime= _timer / speedDownTotalTime;
                _timer+= Time.deltaTime;
                Speed= _speedDownCurve.Evaluate(_normalizeTime)*maxSpeed;
                yield return null;
            }
        }
        
        #endregion
    }
}
