using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventCenter
{
    #region 单例
    private static EventCenter instance;

    public static EventCenter Instance
    {
        get
        {
            if(instance == null)
            {
                instance = new EventCenter();
            }
            return instance;
        }
    }
    #endregion

    // 事件列表
    private Dictionary<string, IEventInfo> _eventDic = new Dictionary<string, IEventInfo>();

    #region 无参事件方法

    // 创建、添加事件
    public void AddEventListener(string name, UnityAction action)
    {        
        if (_eventDic.ContainsKey(name))
        {            
            (_eventDic[name] as EventInfo).actions += action;            
        }
        else
        {
            _eventDic.Add(name, new EventInfo(action));            
        }
    }   

    // 触发事件
    public void EventTrigger(string name)
    {
        if (_eventDic.ContainsKey(name))
        {            
            (_eventDic[name] as EventInfo).actions?.Invoke();
        }
    }

    // 移除事件
    public void RemoveEventListener(string name, UnityAction action)
    {
        if (_eventDic.ContainsKey(name))
        {
            (_eventDic[name] as EventInfo).actions -= action;
        }
    }
    #endregion

    #region 有参事件方法

    public void AddEventListener<T>(string name, UnityAction<T> action)
    {        
        if (_eventDic.ContainsKey(name))
        {
            Debug.Log(typeof(EventInfo<T>));
            Debug.Log(_eventDic[name].GetType().Name);
            (_eventDic[name] as EventInfo<T>).actions += action;     
        }
        else
        {
            _eventDic.Add(name, new EventInfo<T>(action));
        }
    }
    public void EventTrigger<T>(string name, T info)
    {
        if (_eventDic.ContainsKey(name))
        {
            (_eventDic[name] as EventInfo<T>).actions?.Invoke(info);
        }
    }


    public void RemoveEventListener<T>(string name, UnityAction<T> action)
    {
        if (_eventDic.ContainsKey(name))
        {
            (_eventDic[name] as EventInfo<T>).actions -= action;
        }
    }

    #endregion

    // 清空事件
    public void ClearEventListener(string name)
    {
        _eventDic.Remove(name);
    }   
    
}

#region 订阅事件

// 订阅者接口
public interface IEventInfo
{

}

// 订阅者类，无参
public class EventInfo : IEventInfo
{
    public UnityAction actions;

    public EventInfo(UnityAction action)
    {
        actions += action;
    }
}

// 订阅者类，有参
public class EventInfo<T> : IEventInfo
{
    public UnityAction<T> actions;
    public EventInfo(UnityAction<T> action)
    {
        actions += action;
    }
}
#endregion