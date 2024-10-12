using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ThirdSceneController : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    public void Start()
    {
        UpdateScoreUI();
    }

    public void Update()
    {
        UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
        if (GameManager.instance && scoreText != null)
        {
            scoreText.text = "Score: " + GameManager.instance.score.ToString();
        }
    }
}
