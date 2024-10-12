using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int points = 50;
    public int score = 0;
    public int totalScore = 0;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void IncrementScore()
    {
        score += points;
        Debug.Log($"{points} points added to the Score");
    }

    public void LoadNextScene()
    {
        totalScore = score;
        score = totalScore;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ReloadCurrentScene()
    {
        score = totalScore;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void RestartScene()
    {
        totalScore = 0;
        score = 0;
        SceneManager.LoadScene(1);
    }

}
