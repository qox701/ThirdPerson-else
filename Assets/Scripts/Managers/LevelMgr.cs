using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;
    
public class LevelMgr : MonoBehaviour
{
    private static LevelMgr _instance;
    public static LevelMgr Instance => _instance;

    private void Awake()
    {
        _instance = this;
    }
    
    public List<Enemies> enemiesList=new List<Enemies>();
    private int countNum=0;

    private void Start()
    {
        EventCenter.Instance.AddListener<Enemies>("EnemyShouldDead",OnEnemyDead);
    }

    public void OnEnemyDead(Enemies enemy)
    {
        if (enemy.enemySerial==enemiesList[countNum].enemySerial)
        {
            EventCenter.Instance.EventTrigger<Enemies>("EnemyCanDead",enemy);
            countNum++;
        }
    }
}
