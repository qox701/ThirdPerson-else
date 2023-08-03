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

        public AnimationCurve _speedCurve;
        public AnimationCurve _speedDownCurve;
        
        public float speedUpTotalTime=1f;
        public float speedDownTotalTime=1f;
        
        private float _timer;
        private float _normalizeTime;
        #endregion
        
        //Move data
        public float Speed { get; private set; }
        public float maxSpeed = 5f;
        public float RotateSpeed=10f;

        //Atk data
        public float AtkTime=0.5f;
        public float AtkSpeed=10f;
        
        //Damaged data
        public float DamagedTime=0.5f;
        public float DamagedSpeed=10f;

        //Internal
        [HideInInspector]
        public bool isChangingSpeed;
        [HideInInspector]
        public bool isAccelerating=false;
        
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
                //Reading the acceleration curve when the player is accelerating
                else
                {
                    isAccelerating = true;
                    if (myCouroutine!=null)
                    {
                        StopCoroutine(myCouroutine);
                    }
                    _timer = 0;
                    myCouroutine = StartCoroutine(ChangeSpeed());
                }
            }
            else
            {
                if (!isAccelerating)
                {
                    return;
                }
                //Reading the deceleration curve when the player is decelerating
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
