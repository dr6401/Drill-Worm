using UnityEngine;

public class ChaseState : IState
{
    private Vector2 targetPos;
    private float lookForTargetInterval = 0.75f;
    private float timeSinceLookedForTarget;
    
    public void Enter(Animal animal)
    {
        Debug.Log($"Entered ChaseState");
    }

    public void Update(Animal animal)
    {
        timeSinceLookedForTarget += Time.deltaTime;
        if (timeSinceLookedForTarget >= lookForTargetInterval)
        {
            Transform playerPos = PlayerStats.Instance.transform;
            animal.SetTarget(playerPos);
        }
        //Debug.Log($"Updating ChaseState");
    }

    public void Exit(Animal animal)
    {
        Debug.Log($"Exited ChaseState");
    }
}
