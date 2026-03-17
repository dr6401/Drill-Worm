using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class LevelManager : MonoBehaviour
{
    private bool isPaused = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
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

    private void OnEnable()
    {
        GameEvents.OnUpgradesOffered +=  TogglePauseGame;
        GameEvents.OnUpgradeChosen +=  TogglePauseGame;
    }
    
    private void OnDisable()
    {
        GameEvents.OnUpgradesOffered -=  TogglePauseGame;
        GameEvents.OnUpgradeChosen -=  TogglePauseGame;
    }
}
