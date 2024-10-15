using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevelTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // ��E��Ƿ�����������յ�E
        if (other.CompareTag("Player"))
        {
            // ������E���騹��߼�
            
            EndLevel();
        }
    }

    private void EndLevel()
    {
        EventHandler.CallGetGameClearEvent();
    }
}