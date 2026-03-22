using System;
using UnityEngine;
using UnityEngine.UI;

public class Animal : Unit
{
    public float moveSpeed;
    
    [Header("Attack")]
    public int atkDmg;
    public float atkRange;
    public float atkCooldown;
    public float atkCooldownTimer;
    public float attackWindupTime;

    public Vector3 moveTarget;
    public Transform targetPosition;
    public bool hasTarget = false;
    private bool useTransformTarget = false;

    public Transform transformToMove;
    
    [SerializeField] private GameObject attackZoneIndicator;

    private IAnimalBehaviour animalBehaviour;
    private IState currentState;

    public void SetState(IState state)
    {
        currentState?.Exit(this);
        currentState = state;
        currentState?.Enter(this);
    }

    protected override void Start()
    {
        base.Start();
        if (transformToMove == null) transformToMove = transform;
        SetState(new WanderState());
        animalBehaviour = GetComponent<IAnimalBehaviour>();
        if (attackZoneIndicator != null)
        {
            Transform indicatorImage = attackZoneIndicator.GetComponent<Transform>();
            Vector3 scale = indicatorImage.localScale;
            scale.y = atkRange;
            indicatorImage.localScale = scale;
        }
    }

    protected override void Update()
    {
        animalBehaviour?.Tick(this);
        base.Update();
        currentState?.Update(this);
        HandleMovement();
        atkCooldownTimer += Time.deltaTime;
    }

    private void HandleMovement()
    {
        if (!hasTarget) return;

        transformToMove.position = Vector3.MoveTowards(
            transformToMove.position,
            GetTargetPosition(),
            moveSpeed * Time.deltaTime
        );
        
        // Rotation
        Vector3 direction = GetTargetPosition() - transformToMove.position;
        if (direction.magnitude > 0.01f) // Prevent jittering
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            angle -= 90.0f; // Offset sprite rotation
            Quaternion targetRotation = Quaternion.Euler(0f, 0f, angle);

            transformToMove.rotation = Quaternion.RotateTowards(
                transformToMove.rotation,
                targetRotation,
                360 * Time.deltaTime
            );
        }
    }

    private Vector3 GetTargetPosition()
    {
        if (useTransformTarget && targetPosition != null)
        {
            return targetPosition.position;
        }
        return moveTarget;
    }

    public void SetMoveDestination(Vector3 target)
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
            return Vector3.Distance(transformToMove.position, targetPosition.position);
        }
        return Vector3.Distance(transformToMove.position, moveTarget);
    }

    public bool IsTargetInRange(float range)
    {
        return DistanceToTarget() <= range;
    }

    public void SetStateIfNotCurrent(IState newState)
    {
        if (currentState?.GetType() == newState.GetType()) return;
        SetState(newState);
    }

    public bool IsAttackOnCooldown()
    {
        return !(atkCooldownTimer >= atkCooldown);
    }

    public void ShowAttackZoneIndicator()
    {
        attackZoneIndicator?.SetActive(true);
    }
    public void HideAttackZoneIndicator()
    {
        attackZoneIndicator?.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!IsInState(new AttackExecuteState()) && !IsInState(new AttackWindupState()) && !IsInState(new ChaseState())) return;
        if (atkCooldownTimer >= atkCooldown)
        {
            if (other.CompareTag("Head"))
            {
                PlayerStats.Instance.TakeDamage(atkDmg);
                atkCooldownTimer = 0;   
                Debug.Log($"{name}: hit player");
            }
            else
            {
                other.TryGetComponent(out Unit unit);
                unit?.TakeDamage(atkDmg, false);
            }
        }
    }

    public bool IsInState(IState state)
    {
        if (currentState != null && currentState.GetType() == state.GetType()) return true;
        return false;
    }
}
