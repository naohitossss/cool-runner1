using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevelTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // ¼EéÊÇ·ñÊÇÍæ¼ÒÅöµ½ÖÕµE
        if (other.CompareTag("Player"))
        {
            // ÔÚÕâÀE¦Àúé¨¹ØÂß¼­
            
            EndLevel();
        }
    }

    private void EndLevel()
    {
        EventHandler.CallGetGameClearEvent();
    }
}