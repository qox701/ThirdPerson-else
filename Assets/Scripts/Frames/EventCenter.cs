using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.Events;

namespace Utilities
{
    public interface IEventInfo{}

public class EventInfo<T> : IEventInfo
{
    public UnityAction<T> actions;
    
    public EventInfo(UnityAction<T> action)
    {
        actions += action;
    }
}
public class EventInfo:IEventInfo
{
    public UnityAction actions;

    public EventInfo(UnityAction action)
    {
        actions += action;
    }
}

public class EventCenter
{
    private static EventCenter _instance=new EventCenter();
    public static EventCenter Instance=>_instance;

    private Dictionary<string, IEventInfo> eventDic = new Dictionary<string, IEventInfo>();

    public void AddListener<T>(string eventName, UnityAction<T> action)
    {
        if(eventDic.ContainsKey(eventName))
        {
            ((EventInfo<T>)eventDic[eventName]).actions += action;
        }
        else
        {
            eventDic.Add(eventName,new EventInfo<T>(action));
        }
    }
    
    public void AddListener(string eventName, UnityAction action)
    {
        if (eventDic.ContainsKey(eventName))
        {
            ((EventInfo)eventDic[eventName]).actions += action;
        }
        else
        {
            eventDic.Add(eventName,new EventInfo(action));
        }
    }
    
    public void RemoveListener<T>(string eventName, UnityAction<T> action)
    {
        if(eventDic.ContainsKey(eventName))
            ((EventInfo<T>)eventDic[eventName]).actions -= action;
    }
    
    public void RemoveListener(string eventName, UnityAction action)
    {
        if(eventDic.ContainsKey(eventName))
            ((EventInfo)eventDic[eventName]).actions -= action;
    }
    
    public void EventTrigger<T>(string eventName,T info)
    {
        if(eventDic.ContainsKey(eventName))
            ((EventInfo<T>)eventDic[eventName]).actions?.Invoke(info);
    }
    
    public void EventTrigger(string eventName)
    {
        if(eventDic.ContainsKey(eventName))
            ((EventInfo)eventDic[eventName]).actions?.Invoke();
    }

    public void Clear()
    {
        eventDic.Clear();
    }
}
}

