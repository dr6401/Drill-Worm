using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class LevelManager : MonoBehaviour
{
    private bool isPaused = false;
    private bool isInputBlocked = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isInputBlocked) return;
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            TogglePauseGame();
        }
    }

    public void TogglePauseGame()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0 : 1;
    }

    private void ToggleInputBlocked()
    {
        isInputBlocked = !isInputBlocked;
    }

    private void OnEnable()
    {
        GameEvents.OnUpgradesOffered +=  TogglePauseGame;
        GameEvents.OnUpgradeChosen +=  TogglePauseGame;
        GameEvents.OnUpgradesOffered +=  ToggleInputBlocked;
        GameEvents.OnUpgradeChosen +=  ToggleInputBlocked;
    }
    
    private void OnDisable()
    {
        GameEvents.OnUpgradesOffered -=  TogglePauseGame;
        GameEvents.OnUpgradeChosen -=  TogglePauseGame;
        GameEvents.OnUpgradesOffered -=  ToggleInputBlocked;
        GameEvents.OnUpgradeChosen -=  ToggleInputBlocked;
    }
}
