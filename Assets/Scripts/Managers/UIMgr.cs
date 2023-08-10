using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Managers
{
    public enum E_UI_Layer  
    {  
        Bot,  
        Mid,  
        Top,  
        System,  
    }
    
    public class UIMgr
    {
        private static UIMgr _instance;
        public static UIMgr Instance => _instance ??= new UIMgr();
        
        public Dictionary<string,BasePanel> panelDic=new Dictionary<string, BasePanel>();

        private Transform bot;
        private Transform mid;
        private Transform top;
        private Transform system;
        
        //record the canvas
        public RectTransform canvas;

        public UIMgr()
        {
            //create canvas,don't destroy on load
            GameObject obj = ResMgr.Instance.Load<GameObject>("UI/Canvas");
            canvas=obj.transform as RectTransform;
            GameObject.DontDestroyOnLoad(obj);
            
            //find the layers
            bot=canvas.Find("Bot");
            mid=canvas.Find("Mid");
            top=canvas.Find("Top");
            system=canvas.Find("System");
            
            //create event system,don't destroy on load
            obj=ResMgr.Instance.Load<GameObject>("UI/EventSystem");
            GameObject.DontDestroyOnLoad(obj);
        }

        public Transform GetLayerFather(E_UI_Layer layer)
        {
            switch (layer)
            {
                case E_UI_Layer.Bot:
                    return bot;
                case E_UI_Layer.Mid:
                    return mid;
                case E_UI_Layer.Top:
                    return top;
                case E_UI_Layer.System:
                    return system;
            }
            return null;
        }

        public void ShowPanel<T>(string panelName, E_UI_Layer layer = E_UI_Layer.Mid, UnityAction<T> callback=null)
            where T : BasePanel
        {
            if (panelDic.ContainsKey(panelName))
            {
                panelDic[panelName].ShowMe();
                if (callback != null)
                    callback(panelDic[panelName] as T);
                return;
            }
            
            ResMgr.Instance.LoadAsync<GameObject>("UI/"+panelName, (obj) =>
            {
                Transform father = bot;
                switch (layer)
                {
                    case E_UI_Layer.Mid:
                        father = mid;
                        break;
                    case E_UI_Layer.Top:
                        father = top;
                        break;
                    case E_UI_Layer.System:
                        father = system;
                        break;
                }
                obj.transform.SetParent(father);
                
                obj.transform.localPosition = Vector3.zero;  
                obj.transform.localScale = Vector3.one;  
  
                (obj.transform as RectTransform).offsetMax = Vector2.zero;  
                (obj.transform as RectTransform).offsetMin = Vector2.zero;  
                
                T panel = obj.GetComponent<T>();
                callback?.Invoke(panel);
                panel.ShowMe();
                
                panelDic.Add(panelName,panel);
            });
        }
        
        public void HidePanel(string panelName)
        {
            if (panelDic.ContainsKey(panelName))
            {
                panelDic[panelName].HideMe();
                GameObject.Destroy(panelDic[panelName].gameObject);
                panelDic.Remove(panelName);
            }
        }
        
        public T GetPanel<T>(string panelName) where T : BasePanel
        {
            if (panelDic.ContainsKey(panelName))
                return panelDic[panelName] as T;
            return null;
        }

        public static void AddCustomEventListener(UIBehaviour control, EventTriggerType type,
            UnityAction<BaseEventData> callBack)
        {
            EventTrigger trigger = control.GetComponent<EventTrigger>();  
            if (trigger == null)  
                trigger = control.gameObject.AddComponent<EventTrigger>();  
  
            EventTrigger.Entry entry = new EventTrigger.Entry();  
            entry.eventID = type;  
            entry.callback.AddListener(callBack);  
  
            trigger.triggers.Add(entry); 
        }
    }
}
