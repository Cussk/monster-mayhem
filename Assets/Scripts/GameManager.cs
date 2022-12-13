using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //public variables
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI livesText;
    public Button restartButton;
    public GameObject titleScreen;
    public GameObject pauseScreen;
    public bool isGameActive;

    //private variables
    private int score;
    private int lives;
    private bool paused;

    // Update is called once per frame
    void Update()
    {
        //if player presses P pause the game
        if (Input.GetKeyDown(KeyCode.P))
        {
            ChangePaused();
        }
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd; //score increases everytime UpdateScore argument is run
        scoreText.text = "Score: " + score; //will update text ingame as score variable changes
    }

    public void UpdateLives(int livesToChange)
    {
        lives += livesToChange;
        livesText.text = "Lives: " + lives;
        if (lives <= 0)
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        restartButton.gameObject.SetActive(true); //activates restart button
        gameOverText.gameObject.SetActive(true); //activates gameover text
        isGameActive = false;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); //scene manager will reload the current active scene
    }

    public void StartGame(int difficulty)
    {
        isGameActive = true;
        score = 0;
        UpdateScore(0);
        UpdateLives(3 - difficulty);
        titleScreen.gameObject.SetActive(false); //turns off title screen when game starts
    }

    void ChangePaused()
    {
        if (!paused && isGameActive)
        {
            paused = true;
            pauseScreen.SetActive(true);
            Time.timeScale = 0; //scale at which time passes is zero, therefore paused
        }
        else
        {
            paused = false;
            pauseScreen.SetActive(false);
            Time.timeScale = 1; //scale at which times passes is 1, therefore normal gamespeed resumed.
        }
    }
}
