using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int score = 0;
    
    public int[] sceneScores = new int[3];


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

    public void IncrementScore(int points)
    {
        score += points;
        Debug.Log(points + " points added to the Score");
    }

    public void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        sceneScores[currentSceneIndex] = score;
        Debug.Log("Score for Scene " + currentSceneIndex + ": " + score);

        score = 0;

        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    public void ReloadCurrentScene()
    {
        score = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Optionally, get the score for a specific scene
    public int GetScoreForScene(int sceneIndex)
    {
        if (sceneIndex >= 0 && sceneIndex < sceneScores.Length)
        {
            return sceneScores[sceneIndex];
        }

        return 0; // Return 0 if the scene index is out of bounds
    }
}
