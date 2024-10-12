using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class EndMenuController : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public GameObject endMenuPanel;

    public void Start()
    {
        endMenuPanel.SetActive(true);
        if (GameManager.instance)
        {
            scoreText.text = "Score: " + GameManager.instance.score.ToString();
        }
    }

    public void RestartGame()
    {
        GameManager.instance.RestartScene();
    }
}
