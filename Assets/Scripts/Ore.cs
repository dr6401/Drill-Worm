using System;
using UnityEngine;

public class Ore : MonoBehaviour
{
    public int currentHealth = 2;
    public int maxHealth = 3;
    public float knockbackForce = 10f;

    [SerializeField] private GameObject foodSpawnPrefab;
    private float randomSpawnOffset = 2f;

    private float canBeDamagedCooldown = 0.5f;
    public float timeSinceLastDamaged; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;
    }

    private void Update()
    {
        timeSinceLastDamaged += Time.deltaTime;
    }

    private void TakeDamage(int damage)
    {
        currentHealth -= damage;
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

    private void OnTriggerEnter2D(Collider2D other)
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
