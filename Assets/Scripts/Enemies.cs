using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

public class Enemies : MonoBehaviour,ICanDamaged
{
    public void OnDamaged()
    {
        Destroy(this.gameObject);
    }
}
