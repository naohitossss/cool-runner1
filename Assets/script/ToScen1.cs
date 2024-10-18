using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToScen1 : MonoBehaviour
{
    public void LoadGame()
    {
        SceneManager.LoadScene("Scene1");
    }
    public void LoadGameTutorial()
    {
        SceneManager.LoadScene("Tutorial1");
    }
    public void ExilGame()
    {
        Application.Quit();
    }
}
