using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Countdown_Sic : MonoBehaviour
{


    private Text timerText;

    float timer;

    public int second = 120;
    float nextTime = 1;


    private void Start()
    {

        timerText = Object.FindObjectOfType<Text>();
        timerText.color = Color.yellow ;
        timerText.text = string.Format("You only have\n{0:d2}:{1:d2}", second / 60, second % 60);

    }

    private void Update()
    {
        
        CountDownF2();
    }

    private void CountDownF2()
    {
        //��ʱƁE
        timer += Time.deltaTime;
        //��ʱ���ۼӵ�һÁE���� ����ʱδ��ֹ  
        if (timer >= 1 && second > 0)
        {
            //�򵹼�ʱ��һÁE
            second--;
            //��ʾ����ʱ�ı�
            timerText.text = string.Format("You only have\n{0:d2}:{1:d2}", second / 60, second % 60);
            timer = 0;//�Ҽ�ʱ������E ���¼�ʱ
            if (second <= 10)//��ʱ��EС�ڵ���10ÁE�����Ϊ��E���́E
            { 
                timerText.color = Color.red;
            }
            
        }
        else if (second == 0) 
            {
                EventHandler.CallGetGameOverEvent();
                Debug.Log("game over");
            }
    }
}

