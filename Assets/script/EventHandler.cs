using System;
using UnityEngine;

public class EventHandler : MonoBehaviour
{
    // ֪ͨ��Ϸ�������¼�
    public static event Action GetGameOverEvent;

    // ������Ϸ�����¼��ķ���
    public static void CallGetGameOverEvent()
    {
        GetGameOverEvent?.Invoke();
    }

    // ֪ͨ�÷ֵ��¼�
    public static event Action GetPointEvent;

    // ���õ÷��¼��ķ���
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