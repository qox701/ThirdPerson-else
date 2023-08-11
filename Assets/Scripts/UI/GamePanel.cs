using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using Managers;
using UnityEngine.UI;
using Utilities;

namespace UI
{
    public class GamePanel : BasePanel
    {
        public float SwitchTime = 1f;
        
        private List<Image> EnemyImage=new List<Image>();
        private int countNum = 0;
        public int enemyNum = 2;
        protected override void Awake()
        {
            base.Awake();
            for(int i=1;i<=enemyNum;i++)
            {
                EnemyImage.Add(GetControl<Image>("Enemy"+i));
            }

            for (int i = 0; i < EnemyImage.Count; i++)
            {
                if(i!=countNum)
                    EnemyImage[i].gameObject.SetActive(false);
            }
            
            EventCenter.Instance.AddListener("SwitchIcon", SwitchIcon);
        }

        private void OnDestroy()
        {
            EventCenter.Instance.RemoveListener("SwitchIcon", SwitchIcon);
        }

        private void SwitchIcon()
        {
            if(countNum+1>=EnemyImage.Count)
                return;
            StartCoroutine(SwitchIconCoroutine());
        }
        
        private IEnumerator SwitchIconCoroutine()
        {
            float Timer= 0f;
            float nextPos = 100;
            float nowPos = 0;
            
            Image nextImage=EnemyImage[countNum+1];
            Image nowImage=EnemyImage[countNum];
            
            nextImage.gameObject.SetActive(true);
            nextImage.rectTransform.position = new Vector3(nextPos,0,0);
            for(Timer=0;Timer<SwitchTime;Timer+=Time.deltaTime)
            {
                nowPos = Mathf.Lerp(0, -100, Timer / SwitchTime);
                nextPos = Mathf.Lerp(100, 0, Timer / SwitchTime);
                nowImage.rectTransform.localPosition = new Vector3(nowPos,0,0);
                nextImage.rectTransform.localPosition = new Vector3(nextPos,0,0);
                yield return 0;
            }
            nowImage.gameObject.SetActive(false);
            countNum++;
        }
    }
}