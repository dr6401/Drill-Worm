using UnityEngine;

public class WanderState : IState
{
    private Vector3 moveTarget;
    private float maxWanderRadius = 20f;
    private float minWanderRadius = 10f;

    private float minDistanceToTarget = 0.1f;

    public void Enter(Animal animal)
    {
        Vector2 randomDirection = Random.insideUnitCircle * Random.Range(minWanderRadius, maxWanderRadius);
        moveTarget = animal.transform.position + new Vector3(randomDirection.x, randomDirection.y);
        
        animal.SetMoveDestination(moveTarget);
        //Debug.Log($"Entering WanderState, target: {moveTarget}");
    }

    public void Update(Animal animal)
    {
        if (animal.IsTargetInRange(minDistanceToTarget))
        {
            animal.SetState(new IdleState());
        }
        //Debug.Log($"Updating WanderState");
    }

    public void Exit(Animal animal)
    {
        //Debug.Log($"Exiting WanderState");
    }
}
