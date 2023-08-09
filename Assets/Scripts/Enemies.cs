using System;
using System.Collections;
using System.Collections.Generic;using UnityEditor;
using UnityEngine;
using Utilities;

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
        EventCenter.Instance.AddListener<Enemies>("EnemyCanDead",OnDead);
        LevelMgr.Instance.enemiesList[(int)enemySerial]=this;
    }

    public void OnDamaged(Vector3 Pos)
    {
        Vector3 dir = (this.transform.position - Pos).normalized;
        rb.AddForce(dir*10,ForceMode.Impulse);
        EventCenter.Instance.EventTrigger<Enemies>("EnemyShouldDead",this);
    }
    
    private void Vanish()
    {
        Destroy(this.gameObject);
    }

    public void OnDead(Enemies enemy)
    {
        if(enemy.enemySerial==enemySerial)
            Vanish();
    }
}
