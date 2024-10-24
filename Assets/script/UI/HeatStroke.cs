﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
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
    public UnityEngine.UI.Slider strokeBar; // UI中的中暑槽
    private LaneMovement laneMovement;


    private void Awake()
    {
        laneMovement = GetComponent<LaneMovement>();
    }

    private void Start()
    {
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
