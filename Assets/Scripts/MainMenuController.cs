using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void StartGame()
    {
        GameManager.instance.LoadNextScene();
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
