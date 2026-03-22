using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private bool isPaused = false;
    private bool isInputBlocked = false;

    public GameObject deathCanvas;
    public GameObject pauseCanvas;
    void Start()
    {
        deathCanvas?.SetActive(false);
        pauseCanvas?.SetActive(false);
        Time.timeScale = 1;
        isPaused = false;
        isInputBlocked = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isInputBlocked) return;
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            PauseGame();
        }
    }

    public void TogglePauseGame()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0 : 1;
    }

    public void PauseGame()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0 : 1;
        pauseCanvas.SetActive(isPaused);
    }

    private void ToggleInputBlocked()
    {
        isInputBlocked = !isInputBlocked;
    }

    public void HandlePlayerDeath()
    {
        TogglePauseGame();
        deathCanvas?.SetActive(true);
    }

    public void RetryLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    private void OnEnable()
    {
        GameEvents.OnUpgradesOffered +=  TogglePauseGame;
        GameEvents.OnUpgradeChosen +=  TogglePauseGame;
        GameEvents.OnUpgradesOffered +=  ToggleInputBlocked;
        GameEvents.OnUpgradeChosen +=  ToggleInputBlocked;
        GameEvents.OnPlayerDeath += HandlePlayerDeath;
    }
    
    private void OnDisable()
    {
        GameEvents.OnUpgradesOffered -=  TogglePauseGame;
        GameEvents.OnUpgradeChosen -=  TogglePauseGame;
        GameEvents.OnUpgradesOffered -=  ToggleInputBlocked;
        GameEvents.OnUpgradeChosen -=  ToggleInputBlocked;
        GameEvents.OnPlayerDeath -= HandlePlayerDeath;
    }
}
