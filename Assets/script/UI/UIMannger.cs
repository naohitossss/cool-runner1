using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UiManager : MonoBehaviour
{
    // ��Ϸ����ҳ��Ĳ�ׁE
    public GameObject gameOverPanel;
    public GameObject gameClearPanel;
    public GameObject TUPanel1;
    public GameObject TUPanel2;
    public GameObject TUPanel3;
    public GameObject TUPanel4;
    private bool isStart = false;

    // �ű��ձ�����ʱʹ��
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
        // �ָ���Ϸ�ٶȣ���Ϸ��������
        Time.timeScale = 1;
        // ע����յ÷ֵ�ί��
        EventHandler.GetPointEvent += OnGetPointEvent;
        // ��Ϸ������֪ͨ
        EventHandler.GetGameOverEvent += OnGetGameOverEvent;
        EventHandler.GetGameClearEvent += OnGetGameClearEvent;
    }

    // �ű����ٱ�ʹ��
    private void OnDisable()
    {
        EventHandler.GetPointEvent -= OnGetPointEvent;
        EventHandler.GetGameOverEvent -= OnGetGameOverEvent;
        EventHandler.GetGameClearEvent -= OnGetGameClearEvent;
    }

    ///<summary>
    /// ������Ϸ������ί��
    ///</summary>
    private void OnGetGameOverEvent()
    {
        // ��ʾ��Ϸ����ҳÁE
        gameOverPanel.SetActive(true);
        // �����Ϸ����ҳ�汻��ʾ
        if (gameOverPanel.activeInHierarchy)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            // ��Ϸ�ٶȷ���Ϊ0����Ϸֹͣ
            Time.timeScale = 0;
        }
    }

    private void OnGetGameClearEvent()
    {
        // ��ʾ��Ϸ����ҳÁE
        gameClearPanel.SetActive(true);
        // �����Ϸ����ҳ�汻��ʾ
        if (gameClearPanel.activeInHierarchy)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            // ��Ϸ�ٶȷ���Ϊ0����Ϸֹͣ
            Time.timeScale = 0;
        }
    }

    public void LoadTitle()
    {
        SceneManager.LoadScene("Openpanel");
    }

    ///<summary>
    /// �����÷��¼���ί��
    ///</summary>
    private void OnGetPointEvent()
    {
        // ����E������Ӵ����÷ֵ��߼�
        Debug.Log("Point event triggered!");
        // �����������Ӵ����߼������縁Eµ÷���ʾ��
    }
    public void RestartGame()
    {
        //���¼���֮ǰ�����ĳ���
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}