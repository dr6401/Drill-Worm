using UnityEngine;

public class IdleState : IState
{
    private float idleStateTimer = 3;
    private float idleStateCurrentTimer = 0;
    public void Enter(Animal animal)
    {
        //Debug.Log($"Entered IdleState");
    }

    public void Update(Animal animal)
    {
        idleStateCurrentTimer += Time.deltaTime;
        if (idleStateCurrentTimer >= idleStateTimer)
        {
            idleStateCurrentTimer = 0;
            animal.SetState(new WanderState());
        }
        //Debug.Log($"Updating IdleState");
    }

    public void Exit(Animal animal)
    {
        //Debug.Log($"Exited IdleState");
    }
}
