using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UiManager : MonoBehaviour
{
    // 游戏结束页面的操讈E
    public GameObject gameOverPanel;
    public GameObject gameClearPanel;
    public GameObject TUPanel1;
    public GameObject TUPanel2;
    public GameObject TUPanel3;
    public GameObject TUPanel4;
    private bool isStart = false;

    // 脚本刚被调用时使用
    private void Start()
    {
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void Update()
    {
        if (Input.anyKey&&!isStart)
        {
            TUPanel1.SetActive(false);
            isStart = true;
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = false;
        }
    }
    private void OnEnable()
    {
        // 恢复游戏速度，游戏正常进行
        Time.timeScale = 1;
        // 注册接收得分的委托
        EventHandler.GetPointEvent += OnGetPointEvent;
        // 游戏结束的通知
        EventHandler.GetGameOverEvent += OnGetGameOverEvent;
        EventHandler.GetGameClearEvent += OnGetGameClearEvent;
    }

    // 脚本不再被使用
    private void OnDisable()
    {
        EventHandler.GetPointEvent -= OnGetPointEvent;
        EventHandler.GetGameOverEvent -= OnGetGameOverEvent;
        EventHandler.GetGameClearEvent -= OnGetGameClearEvent;
    }

    ///<summary>
    /// 处历镂戏结束的委托
    ///</summary>
    private void OnGetGameOverEvent()
    {
        // 显示游戏结束页脕E
        gameOverPanel.SetActive(true);
        // 如果游戏结束页面被显示
        if (gameOverPanel.activeInHierarchy)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            // 游戏速度放慢为0，游戏停止
            Time.timeScale = 0;
        }
    }

    private void OnGetGameClearEvent()
    {
        // 显示游戏结束页脕E
        gameClearPanel.SetActive(true);
        // 如果游戏结束页面被显示
        if (gameClearPanel.activeInHierarchy)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            // 游戏速度放慢为0，游戏停止
            Time.timeScale = 0;
        }
    }

    public void LoadTitle()
    {
        SceneManager.LoadScene("Openpanel");
    }

    ///<summary>
    /// 处历衙分事件的委托
    ///</summary>
    private void OnGetPointEvent()
    {
        // 这纴E梢蕴哟梅值穆呒�
        Debug.Log("Point event triggered!");
        // 根据需求铁赜处历捱辑，例如竵E碌梅窒允镜�
    }
    public void RestartGame()
    {
        //重新加载之前畸鹁过的场景
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}