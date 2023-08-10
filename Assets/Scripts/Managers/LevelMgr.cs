using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

namespace Managers
{
    public class LevelMgr : MonoBehaviour
    {
        private static LevelMgr _instance;
        public static LevelMgr Instance => _instance;

        private void Awake()
        {
            _instance = this;
        }

        public List<int> enemiesList = new List<int>();
        private int countNum = 0;

        private void Start()
        {
            EventCenter.Instance.AddListener<int>("EnemyShouldDead", OnEnemyDead);
            
            UIMgr.Instance.ShowPanel<BeginPanel>("BeginPanel", E_UI_Layer.Mid, null);
        }

        public void OnEnemyDead(int value)
        {
            if (value == enemiesList[countNum])
            {
                EventCenter.Instance.EventTrigger<int>("EnemyCanDead", value);
                countNum++;
            }
        }

        private void OnDestroy()
        {
            EventCenter.Instance.RemoveListener<int>("EnemyShouldDead", OnEnemyDead);
        }
    }
}
