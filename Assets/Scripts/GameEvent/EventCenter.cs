using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventCenter
{
    #region ����
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

    // �¼��б�
    private Dictionary<string, IEventInfo> _eventDic = new Dictionary<string, IEventInfo>();

    #region �޲��¼�����

    // ����������¼�
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

    // �����¼�
    public void EventTrigger(string name)
    {
        if (_eventDic.ContainsKey(name))
        {            
            (_eventDic[name] as EventInfo).actions?.Invoke();
        }
    }

    // �Ƴ��¼�
    public void RemoveEventListener(string name, UnityAction action)
    {
        if (_eventDic.ContainsKey(name))
        {
            (_eventDic[name] as EventInfo).actions -= action;
        }
    }
    #endregion

    #region �в��¼�����

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

    // ����¼�
    public void ClearEventListener(string name)
    {
        _eventDic.Remove(name);
    }   
    
}

#region �����¼�

// �����߽ӿ�
public interface IEventInfo
{

}

// �������࣬�޲�
public class EventInfo : IEventInfo
{
    public UnityAction actions;

    public EventInfo(UnityAction action)
    {
        actions += action;
    }
}

// �������࣬�в�
public class EventInfo<T> : IEventInfo
{
    public UnityAction<T> actions;
    public EventInfo(UnityAction<T> action)
    {
        actions += action;
    }
}
#endregion