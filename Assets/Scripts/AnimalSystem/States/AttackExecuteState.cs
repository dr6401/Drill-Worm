using UnityEngine;

public class AttackExecuteState : IState
{

    private float attackSpeed = 10f;
    private float attackTimer;
    private float attackDuration = 0.25f;

    private Vector3 attackStartPosition;
    private Vector3 attackTargetPosition;
    
    public void Enter(Animal animal)
    {
        attackTimer = 0;
        //animal.atkCooldownTimer = 0;
        attackStartPosition = animal.transformToMove.position;

        Vector3 targetDirection = animal.transformToMove.up.normalized;
        attackTargetPosition = attackStartPosition + targetDirection * animal.atkRange;
    }

    public void Update(Animal animal)
    {
        attackTimer += Time.deltaTime;
        float t = Mathf.Clamp01(attackTimer / attackDuration);

        animal.transformToMove.position =
            Vector3.Lerp(animal.transformToMove.position, attackTargetPosition, attackSpeed * 0.01f * t);
        
        if (t >= 1f)
        {
            animal.SetStateIfNotCurrent(new ChaseState());
        }

    }

    public void Exit(Animal animal)
    {
        //animal.HideAttackZoneIndicator();
        animal.atkCooldownTimer = 0;
        attackTimer = 0;
    }
}
