using System;
using UnityEngine;

public class Animal : Unit
{
    public float moveSpeed;
    
    public int atk;
    public int atkRange;
    public int atkCooldown;

    public Vector3 moveTarget;
    public Transform targetPosition;
    public bool hasTarget = false;
    private bool useTransformTarget = false;

    public Transform transformToMove;

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
    }

    protected override void Update()
    {
        animalBehaviour?.Tick(this);
        base.Update();
        currentState?.Update(this);
        HandleMovement();
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
}
