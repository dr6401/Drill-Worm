using UnityEngine;

public class Unit : MonoBehaviour
{
    public int hp ;
    public int maxHp;
    public float knockbackForce;
    
    [SerializeField] private GameObject foodSpawnPrefab;
    private float randomFoodSpawnOffset = 2f;

    private float canBeDamagedCooldown = 0.5f;
    public float timeSinceLastDamaged; 
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        hp = maxHp;
    }
    private void Update()
    {
        timeSinceLastDamaged += Time.deltaTime;
    }
    
    protected virtual void TakeDamage(int damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            GetDestroyed();
            return;
        }
        Movement.Instance.GetKnockedBack(transform.position, knockbackForce);
    }

    protected virtual void GetDestroyed()
    {
        Vector3 offset = new Vector3(
            Random.Range(-randomFoodSpawnOffset, randomFoodSpawnOffset),
            Random.Range(-randomFoodSpawnOffset, randomFoodSpawnOffset),
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
            TakeDamage(PlayerStats.Instance.drillDamage);
            timeSinceLastDamaged = 0;
        }
    }
}
