using System;
using UnityEngine;

public class Animal : Unit
{
    public float moveSpeed;
    
    public int atk;
    public int atkRange;
    public int atkCooldown;

    private Vector3 moveTarget;
    private Transform targetPosition;
    private bool hasTarget = false;
    private bool useTransformTarget = false;

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
        SetState(new IdleState());
    }

    protected override void Update()
    {
        base.Update();
        currentState?.Update(this);
        HandleMovement();
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
}
