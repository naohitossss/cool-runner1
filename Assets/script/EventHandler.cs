using System;
using UnityEngine;

public class EventHandler : MonoBehaviour
{
    // 通知游戏结束的事件
    public static event Action GetGameOverEvent;

    // 调用游戏结束事件的方法
    public static void CallGetGameOverEvent()
    {
        GetGameOverEvent?.Invoke();
    }

    // 通知得分的事件
    public static event Action GetPointEvent;

    // 调用得分事件的方法
    public static void CallGetPointEvent()
    {
        GetPointEvent?.Invoke();
    }

    public static event Action GetGameClearEvent;

    public static void CallGetGameClearEvent()
    {
        GetGameClearEvent?.Invoke();
    }
}