using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Ore : MonoBehaviour
{
    public int currentHealth = 2;
    public int maxHealth = 3;
    public float knockbackForce = 10f;

    [SerializeField] private Transform crackFoldersRoot;
    [SerializeField] private Transform[] crackFolders;

    [SerializeField] private GameObject foodSpawnPrefab;
    private float randomSpawnOffset = 2f;

    private float canBeDamagedCooldown = 0.5f;
    public float timeSinceLastDamaged; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;

        crackFolders = new Transform[crackFoldersRoot.childCount];
        for (int i = 0; i < crackFoldersRoot.childCount; i++)
        {
            crackFolders[i] = crackFoldersRoot.GetChild(i);
            crackFolders[i].gameObject.SetActive(false);
        }

        if (crackFolders.Length > 0)
        {
            crackFolders[0].gameObject.SetActive(true);
        }
    }

    private void Update()
    {
        timeSinceLastDamaged += Time.deltaTime;
    }

    private void TakeDamage(int damage)
    {
        currentHealth -= damage;

        float healthRatio = (float)currentHealth / maxHealth;
        int totalFolders = crackFolders.Length;

        for (int i = 0; i < totalFolders; i++)
        {
            float threshold = 1f - ((float)i / totalFolders);
            if (healthRatio <= threshold)
            {
                crackFolders[i].gameObject.SetActive(true);
            }
        }
        
        if (currentHealth <= 0)
        {
            GetDestroyed();
        }
    }

    private void GetDestroyed()
    {
        Vector3 offset = new Vector3(
            UnityEngine.Random.Range(-randomSpawnOffset, randomSpawnOffset),
            UnityEngine.Random.Range(-randomSpawnOffset, randomSpawnOffset),
            0
        );
        Instantiate(foodSpawnPrefab, transform.position + offset, Quaternion.identity);
        Destroy(gameObject);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        //Debug.Log($"Collided with {other.gameObject.name}");
        if (other.CompareTag("DrillZone") && timeSinceLastDamaged >= canBeDamagedCooldown)
        {
            Movement.Instance.GetKnockedBack(transform.position, knockbackForce);
            TakeDamage(PlayerStats.Instance.drillDamage);
            timeSinceLastDamaged = 0;
        }
    }
}
