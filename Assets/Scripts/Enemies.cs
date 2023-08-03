using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

public class Enemies : MonoBehaviour,ICanDamaged
{
    public void OnDamaged()
    {
        EventCenter.Instance.EventTrigger<Enemies>("Enemydead",this);
        Destroy(this.gameObject);
    }
    
}
