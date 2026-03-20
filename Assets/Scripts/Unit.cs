using UnityEngine;
using UnityEngine.UI;

public class Unit : MonoBehaviour
{
    [Header("Animal")]
    [SerializeField] private GameObject animalObjectRoot; 
    
    [Header("HP")]
    public int hp ;
    public int maxHp;
    
    [Header("On Damage")]
    public float knockbackForce;
    [Header("On Death")]
    [SerializeField] private GameObject foodSpawnPrefab;
    private float randomFoodSpawnOffset = 0.25f;

    [Header("Health Bar")]
    [SerializeField] private bool displayHealthBar = true;
    [SerializeField] private GameObject hpBar;
    [SerializeField] private Image hpFillBar;
    private Vector3 hpBarOffset = new Vector3(0f, 1f, 0f);

    private Collider2D[] colliders;
    
    private float canBeDamagedCooldown = 0.5f;
    private float timeSinceLastDamaged;
    private float timeSinceLastCheckedForNewColliders;
    private float timeSinceLastCheckedForNewCollidersInterval = 4f;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        hp = maxHp;
        hpBar.SetActive(false);

        if (animalObjectRoot != null)
        {
            colliders = animalObjectRoot.GetComponentsInChildren<Collider2D>();
            //Debug.Log("Used colliders from object root");
        }
        else
        {
            colliders = GetComponentsInChildren<Collider2D>();
            //Debug.Log("Used colliders from this object");
        }
        timeSinceLastCheckedForNewColliders = Random.Range(0.5f, timeSinceLastCheckedForNewCollidersInterval);
    }
    protected virtual void Update()
    {
        timeSinceLastDamaged += Time.deltaTime;
        timeSinceLastCheckedForNewColliders += Time.deltaTime;
        if (hpBar.activeSelf)
        {
            Vector3 worldPos = transform.position + hpBarOffset;
            //Vector3 screenPost = Camera.main.WorldToScreenPoint(worldPos);
            //hpBar.transform.position = transform.position + screenPost;
            hpBar.transform.position = worldPos;
            hpBar.transform.rotation = Quaternion.identity;
        }
        
        // Check for Collisions
        foreach (var col in colliders)
        {
            if (col.IsTouchingLayers(LayerMask.GetMask("DrillZoneLayer")) && timeSinceLastDamaged >= canBeDamagedCooldown)
            {
                TakeDamage(PlayerStats.Instance.drillDamage, true);
                timeSinceLastDamaged = 0;
            }
            // Else -> implement for enemy damaging
        }

        if (timeSinceLastCheckedForNewColliders >= timeSinceLastCheckedForNewCollidersInterval)
        {
            CheckForNewColliders();
            timeSinceLastCheckedForNewColliders = 0;
        }
    }
    
    public virtual void TakeDamage(int damage, bool damagedFromPlayer)
    {
        hp -= damage;
        if (hp <= 0)
        {
            GetDestroyed();
            return;
        }
        UpdateHealthBar();
        if (damagedFromPlayer) Movement.Instance.GetKnockedBack(transform.position, knockbackForce);
    }

    protected virtual void GetDestroyed()
    {
        Vector3 offset = new Vector3(
            Random.Range(-randomFoodSpawnOffset, randomFoodSpawnOffset),
            Random.Range(-randomFoodSpawnOffset, randomFoodSpawnOffset),
            0
        );
        Instantiate(foodSpawnPrefab, transform.position + offset, Quaternion.identity);
        if (animalObjectRoot != null) Destroy(animalObjectRoot);
        else Destroy(gameObject);
    }

    protected void UpdateHealthBar()
    {
        if (displayHealthBar)
        {
            hpBar.SetActive(true);
            hpFillBar.fillAmount = (float)hp / maxHp;
        }
    }

    private void CheckForNewColliders()
    {
        if (animalObjectRoot != null)
        {
            colliders = animalObjectRoot.GetComponentsInChildren<Collider2D>();
            //Debug.Log("Checked colliders from object root");
        }
        else
        {
            colliders = GetComponentsInChildren<Collider2D>();
            //Debug.Log("Checked colliders from this object");
        }
    }

    /*private void OnTriggerStay2D(Collider2D other)
    {
        //Debug.Log($"Collided with {other.gameObject.name}");
        if (other.CompareTag("DrillZone") && timeSinceLastDamaged >= canBeDamagedCooldown)
        {
            TakeDamage(PlayerStats.Instance.drillDamage);
            timeSinceLastDamaged = 0;
        }
    }*/
}
