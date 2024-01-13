using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
public class LogicManager : MonoBehaviour
{
    public int playerScore;
    public Text scoreText;
    public Text HighScoreText;
    public GameObject gameOverScreen;
    public GameObject startGameScreen;
    public BirdScript bird;
    public HighScore highScore = new HighScore();

    private void Start()
    {
        LoadHighScore();
        HighScoreText.text = highScore.highScore.ToString();
    }

    public void SaveHighScore()
    {
        string score = JsonUtility.ToJson(highScore);
        string filePath = Application.persistentDataPath + "/GameData.json";
        Debug.Log(filePath);
        System.IO.File.WriteAllText(filePath, score);
    }

    public void LoadHighScore()
    {
        string filePath = Application.persistentDataPath + "/GameData.json";
        string score = System.IO.File.ReadAllText(filePath);

        highScore = JsonUtility.FromJson<HighScore>(score); 
    }


    [ContextMenu("ADD SCORE")]
    public void addScore(int Score)
    {
        playerScore += Score;
        scoreText.text = playerScore.ToString();
    }

    public void restartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void gameOver()
    {
        gameOverScreen.SetActive(true);
        bird.myRigidbody.bodyType = RigidbodyType2D.Static;
        bird.isActive = false;
        bird.isAlive = false;
        if (playerScore > highScore.highScore)
        {
            highScore.highScore = playerScore;
            SaveHighScore();
        }
        
    }

    public void startGame()
    {
        startGameScreen.SetActive(false);
        bird.myRigidbody.bodyType = RigidbodyType2D.Dynamic;
        bird.isActive=true;
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}

[System.Serializable]
public class HighScore
{
    public int highScore;
}