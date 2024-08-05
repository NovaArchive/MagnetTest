using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : Singleton<PauseManager>
{
    public bool GameIsPaused { get; private set; }

    [SerializeField] private GameObject pauseMenu;

    private void Awake()
    {
        ResumeGame();
    }

    private void Update()
    {
        if (GameManager.Instance.CurrentPhase != GameManager.GamePhase.InGame) return;
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        GameIsPaused = true;
        pauseMenu.SetActive(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        GameIsPaused = false;
        pauseMenu.SetActive(false);
    }

    public void ToMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
