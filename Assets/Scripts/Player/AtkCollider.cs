using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Controller;

namespace Utilities
{
    [RequireComponent(typeof(Collider))]
    public class AtkCollider : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<ICanDamaged>()!=null)
            {
                other.GetComponent<ICanDamaged>().OnDamaged();
            }
        }
    }
}

