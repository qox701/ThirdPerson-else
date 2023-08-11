using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;

namespace UI
{
    public class BeginPanel : BasePanel
    {
        protected override void Awake()
        {
            base.Awake();
        }
        
        protected override void OnClick(string btnName)
        {
            switch (btnName)
            {
                case "btnStart":
                    ClickStart();
                    break;
                case "btnExit":
                    ClickExit();
                    break;
            }
        }

        public void ClickStart()
        {
            Time.timeScale = 1;
            UIMgr.Instance.ShowPanel<GamePanel>("GamePanel", E_UI_Layer.Mid, null);
            UIMgr.Instance.HidePanel("BeginPanel");
        }

        public void ClickExit()
        {

        }
    }
}
