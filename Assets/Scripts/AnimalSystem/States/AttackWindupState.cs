using UnityEngine;

public class AttackWindupState : IState
{
    private float attackWindupTimer;

    private float attackWindupDuration = 2f;

    public void Enter(Animal animal)
    {
        // Stop moving
        attackWindupDuration = animal.attackWindupTime;
        animal.SetTarget(null);
        attackWindupTimer = 0;
        animal.ShowAttackZoneIndicator();
    }

    public void Update(Animal animal)
    {
        attackWindupTimer += Time.deltaTime;
        if (attackWindupTimer >= attackWindupDuration)
        {
            animal.SetStateIfNotCurrent(new AttackExecuteState());
        }
    }

    public void Exit(Animal animal)
    {
        animal.HideAttackZoneIndicator();
    }
}
