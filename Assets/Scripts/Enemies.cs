using System;
using System.Collections;
using System.Collections.Generic;using UnityEditor;
using UnityEngine;
using Utilities;
using Managers;

[RequireComponent(typeof(Rigidbody))]

public class Enemies : MonoBehaviour,ICanDamaged
{
    private Rigidbody rb;
    
    public enum EnemySerial
    {
        first,
        second,
    }

    public EnemySerial enemySerial;
    
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        EventCenter.Instance.AddListener<int>("EnemyCanDead",OnDead);
        
        LevelMgr.Instance.enemiesList[(int)enemySerial]=(int)enemySerial;
    }

    public void OnDamaged(Vector3 Pos)
    {
        Vector3 dir = (this.transform.position - Pos).normalized;
        rb.AddForce(dir*10,ForceMode.Impulse);
        EventCenter.Instance.EventTrigger<int>("EnemyShouldDead",(int)enemySerial);
    }
    
    private void Vanish()
    {
        EventCenter.Instance.RemoveListener<int>("EnemyCanDead",OnDead);
        Destroy(this.gameObject);
    }

    public void OnDead(int value)
    {
        if(value==(int)enemySerial)
            Vanish();
    }
}
