using System;
using UnityEngine;

public class Animal : MonoBehaviour
{
    public int hp;
    public int maxHp;

    public float moveSpeed;
    
    public int atk;
    public int atkRange;
    public int atkCooldown;

    private Vector3 moveTarget;
    private Transform targetPosition;
    private bool hasTarget = false;
    private bool useTransformTarget = false;

    private float randomFoodSpawnOffset = 0.5f;
    [SerializeField] private GameObject foodSpawnPrefab;
    
    public float knockbackForce = 5f;
    
    private float canBeDamagedCooldown = 0.5f;
    private float timeSinceLastDamaged; 
    
    private IState currentState;

    public void SetState(IState state)
    {
        currentState?.Exit(this);
        currentState = state;
        currentState?.Enter(this);
    }

    private void Start()
    {
        SetState(new IdleState());
        hp = maxHp;
    }

    void Update()
    {
        currentState?.Update(this);
        HandleMovement();
        timeSinceLastDamaged += Time.deltaTime;
    }

    private void HandleMovement()
    {
        if (!hasTarget) return;

        transform.position = Vector3.MoveTowards(
            transform.position,
            GetTargetPosition(),
            moveSpeed * Time.deltaTime
        );
    }

    private Vector3 GetTargetPosition()
    {
        if (useTransformTarget && targetPosition != null)
        {
            return targetPosition.position;
        }
        return moveTarget;
    }

    public void SetMoveTarget(Vector3 target)
    {
        moveTarget = target;
        useTransformTarget = false;
        hasTarget = true;
    }
    
    public void SetTarget(Transform target)
    {
        targetPosition = target;
        useTransformTarget = true;
        hasTarget = target != null;
    }

    public float DistanceToTarget()
    {
        if (!hasTarget) return Mathf.Infinity;
        if (useTransformTarget && targetPosition != null)
        {
            return Vector3.Distance(transform.position, targetPosition.position);
        }
        return Vector3.Distance(transform.position, moveTarget);
    }

    public bool IsTargetInRange(float range)
    {
        return DistanceToTarget() <= range;
    }
    
    public void TakeDamage(int damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Vector3 offset = new Vector3(
            UnityEngine.Random.Range(-randomFoodSpawnOffset, randomFoodSpawnOffset),
            UnityEngine.Random.Range(-randomFoodSpawnOffset, randomFoodSpawnOffset),
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
            //Debug.Log($"Took {PlayerStats.Instance.drillDamage} damage");
        }
    }
}
