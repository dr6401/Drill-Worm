using UnityEngine;
using UnityEngine.UI;

public class Unit : MonoBehaviour
{
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

    private float canBeDamagedCooldown = 0.5f;
    private float timeSinceLastDamaged; 
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        hp = maxHp;
        hpBar.SetActive(false);
    }
    protected virtual void Update()
    {
        timeSinceLastDamaged += Time.deltaTime;
        if (hpBar.activeSelf)
        {
            Vector3 worldPos = transform.position + hpBarOffset;
            //Vector3 screenPost = Camera.main.WorldToScreenPoint(worldPos);
            //hpBar.transform.position = transform.position + screenPost;
            hpBar.transform.position = worldPos;
            hpBar.transform.rotation = Quaternion.identity;
        }
    }
    
    protected virtual void TakeDamage(int damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            GetDestroyed();
            return;
        }
        UpdateHealthBar();
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

    protected void UpdateHealthBar()
    {
        if (displayHealthBar)
        {
            hpBar.SetActive(true);
            hpFillBar.fillAmount = (float)hp / maxHp;
        }
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
