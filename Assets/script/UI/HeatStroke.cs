using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.InputSystem;

public class HeatStroke : MonoBehaviour
{
    public float minStroke = 0f;
    [Header("中暑值")]
    [Range(0, 100)]
    public float maxStroke = 100f;
    public float currentStroke;
    [Header("在阳光下每秒增加的中暑值")]
    [Range(0, 10)]
    public float sunExposureRate = 1f; // 在阳光下每秒增加的中暑值
    [Header("在阴影处每秒减少的中暑值")]
    [Range(0, 10)]
    public float shadeRecoveryRate = 1f; // 在阴影处每秒减少的中暑值
    public Slider strokeBar; // UI中的中暑槽
    public ShadowCollider shadowCollider;
    //private PlayerInput playerInput;


    private void Awake()
    {
        // 获取输入输出组件
        //playerInput = GetComponent<PlayerInput>();
    }

    private void Start()
    {
        shadowCollider = GetComponent<ShadowCollider>();
        // 初始化中暑值
        currentStroke = minStroke;

        // 确保 strokeBar 已经正确分配

        strokeBar.maxValue = maxStroke;
        strokeBar.value = currentStroke;
    }

    private void Update()
    {
        

        // 处理中暑值的逻辑
        HandleStroke();

        // 更新 UI
        strokeBar.value = currentStroke;
    }

    void HandleStroke()
    {
        // 获取角色移动的输入
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // 判断角色是否在奔跑或静止
        bool isRunning = Input.GetKey(KeyCode.LeftShift) && (horizontal != 0 || vertical != 0);
        bool isIdle = horizontal == 0 && vertical == 0;
        bool isJumping = Input.GetKey(KeyCode.Space);



        if (isRunning)
        {

            if (shadowCollider.ifShadow == false)
            {
                currentStroke += sunExposureRate * Time.deltaTime * 3;
            }
            else {
                currentStroke += sunExposureRate * Time.deltaTime * 2;// 奔跑时增加中暑值
            }
        }
        else if (isJumping) {
            currentStroke += sunExposureRate * Time.deltaTime;
        }

        else if (shadowCollider.ifShadow == false)
        {
            currentStroke += sunExposureRate * Time.deltaTime;
        }
        else if (isIdle && currentStroke > minStroke && shadowCollider.ifShadow)
        {
            currentStroke -= shadeRecoveryRate * Time.deltaTime; // 静止时减少中暑值
        }

        // 防止中暑值超出范围
        currentStroke = Mathf.Clamp(currentStroke, minStroke, maxStroke);

        // 当中暑值达到最大时，游戏结束
        if (currentStroke >= maxStroke)
        {
            GameOver();
        }
    }

    void GameOver()
    {
        EventHandler.CallGetGameOverEvent();
       // playerInput.enabled = false;
    }
    
}
