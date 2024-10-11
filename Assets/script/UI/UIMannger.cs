using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UiManager : MonoBehaviour
{
    // ÓÎÏ·½áÊøÒ³ÃæµÄ²Ù×E
    public GameObject gameOverPanel;
    public GameObject gameClearPanel;
    public GameObject TUPanel1;
    public GameObject TUPanel2;
    public GameObject TUPanel3;
    public GameObject TUPanel4;
    private bool isStart = false;

    // ½Å±¾¸Õ±»µ÷ÓÃÊ±Ê¹ÓÃ
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
        // »Ö¸´ÓÎÏ·ËÙ¶È£¬ÓÎÏ·Õı³£½øĞĞ
        Time.timeScale = 1;
        // ×¢²á½ÓÊÕµÃ·ÖµÄÎ¯ÍĞ
        EventHandler.GetPointEvent += OnGetPointEvent;
        // ÓÎÏ·½áÊøµÄÍ¨Öª
        EventHandler.GetGameOverEvent += OnGetGameOverEvent;
        EventHandler.GetGameClearEvent += OnGetGameClearEvent;
    }

    // ½Å±¾²»ÔÙ±»Ê¹ÓÃ
    private void OnDisable()
    {
        EventHandler.GetPointEvent -= OnGetPointEvent;
        EventHandler.GetGameOverEvent -= OnGetGameOverEvent;
        EventHandler.GetGameClearEvent -= OnGetGameClearEvent;
    }

    ///<summary>
    /// ´¦ÀúïÎÏ·½áÊøµÄÎ¯ÍĞ
    ///</summary>
    private void OnGetGameOverEvent()
    {
        // ÏÔÊ¾ÓÎÏ·½áÊøÒ³ÃE
        gameOverPanel.SetActive(true);
        // Èç¹ûÓÎÏ·½áÊøÒ³Ãæ±»ÏÔÊ¾
        if (gameOverPanel.activeInHierarchy)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            // ÓÎÏ·ËÙ¶È·ÅÂıÎª0£¬ÓÎÏ·Í£Ö¹
            Time.timeScale = 0;
        }
    }

    private void OnGetGameClearEvent()
    {
        // ÏÔÊ¾ÓÎÏ·½áÊøÒ³ÃE
        gameClearPanel.SetActive(true);
        // Èç¹ûÓÎÏ·½áÊøÒ³Ãæ±»ÏÔÊ¾
        if (gameClearPanel.activeInHierarchy)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            // ÓÎÏ·ËÙ¶È·ÅÂıÎª0£¬ÓÎÏ·Í£Ö¹
            Time.timeScale = 0;
        }
    }

    public void LoadTitle()
    {
        SceneManager.LoadScene("Openpanel");
    }

    ///<summary>
    /// ´¦ÀúÑÃ·ÖÊÂ¼şµÄÎ¯ÍĞ
    ///</summary>
    private void OnGetPointEvent()
    {
        // ÕâÀEÉÒÔÌúØÓ´¦ÀúÑÃ·ÖµÄÂß¼­
        Debug.Log("Point event triggered!");
        // ¸ù¾İĞèÇóÌúØÓ´¦ÀúŞß¼­£¬ÀıÈç¸EÂµÃ·ÖÏÔÊ¾µÈ
    }
    public void RestartGame()
    {
        //ÖØĞÂ¼ÓÔØÖ®Ç°»ûğ¾¹ıµÄ³¡¾°
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}