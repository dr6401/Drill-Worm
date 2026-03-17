using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance;
    
    [Header("Experience")]
    public int currentLevel = 1;
    public int currentExperience = 0;
    public int xpUntilLevelUp = 10;

    private void Awake()
    {
        if (Instance != null &&  Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // DEBUG
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            Consume(1);
        }
    }

    private void Consume(int xpAmount)
    {
        currentExperience += xpAmount;
        if (currentExperience >= xpUntilLevelUp)
        {
            LevelUp();
        }
    }

    private void LevelUp()
    {
        currentExperience = 0;
        xpUntilLevelUp = Mathf.RoundToInt(xpUntilLevelUp * 1.25f);
        currentLevel++;
        GameEvents.OnLevelUp?.Invoke();
    }
}
