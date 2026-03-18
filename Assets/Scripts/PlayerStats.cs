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
    
    [Header("Health")]
    public int currentHealth = 5;
    public int maxHealth = 10;
    
    [Header("Damage")]
    public int drillDamage = 1;

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
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        // DEBUG
        /*if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            Consume(1);
        }*/
        /*if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            TakeDamage(1);
        }*/
    }

    public void Consume(int xpAmount)
    {
        currentExperience += xpAmount;
        if (currentExperience >= xpUntilLevelUp)
        {
            LevelUp();
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
    }

    private void LevelUp()
    {
        currentExperience = 0;
        xpUntilLevelUp = Mathf.RoundToInt(xpUntilLevelUp * 1.25f);
        currentLevel++;
        GameEvents.OnLevelUp?.Invoke();
    }

    private void Die()
    {
        Debug.Log("Man im dead");
        GameEvents.OnPlayerDeath?.Invoke();
    }
}
