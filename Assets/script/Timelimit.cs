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
        //计时苼E
        timer += Time.deltaTime;
        //当时间累加到一脕E并且 倒计时未终止  
        if (timer >= 1 && second > 0)
        {
            //则倒计时减一脕E
            second--;
            //显示倒计时文本
            timerText.text = string.Format("You only have\n{0:d2}:{1:d2}", second / 60, second % 60);
            timer = 0;//且计时器归羴E 重新计时
            if (second <= 10)//当时紒E小于等于10脕E保湮丒痔丒
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

