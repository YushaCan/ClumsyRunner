using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using JetBrains.Annotations;
using System;

public class GameManager : MonoBehaviour
{
    private int score;
    [SerializeField] float gameTime = 20;
    [SerializeField] float gameOverCount = 0.5f;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI timeText;
    [SerializeField] TextMeshProUGUI gameOverText;
    [SerializeField] Button restartButton;
    [SerializeField] Button mainMenuButton;
    private PlayerController playerControllerScript;
    private PauseMenu pauseMenu;
    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        
    }

    // Update is called once per frame
    void Update()
    {   
        if (playerControllerScript.gameOver == false)
        {
            gameTime += Time.deltaTime;
            UpdateTime();
        }
        else
        {
            GameOver();
        }
    }

    private void UpdateTime()
    {
        timeText.text = "Time:" + Mathf.RoundToInt(gameTime);
    }
    private void GameOver()
    {
        gameOverCount -= Time.deltaTime;
        if(gameOverCount <= 0)
        {
            gameOverText.gameObject.SetActive(true);
            restartButton.gameObject.SetActive(true);
            mainMenuButton.gameObject.SetActive(true);
        }       
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void MainMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
