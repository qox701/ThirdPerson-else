using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;

public class BeginPanel : BasePanel
{
    protected override void Awake()
    {
        base.Awake();
    }

    public override void ShowMe()
    {
        base.ShowMe();
    }

    public override void HideMe()
    {
        base.HideMe();
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
        
    }
    
    public void ClickExit()
    {
        
    }
}
