using UnityEngine;

public class ChaseState : IState
{
    private Vector2 targetPos;
    
    public void Enter(Animal animal)
    {
        Debug.Log($"Entered ChaseState");
    }

    public void Update(Animal animal)
    {
        // Move animal
        Debug.Log($"Updating ChaseState");
    }

    public void Exit(Animal animal)
    {
        Debug.Log($"Exited ChaseState");
    }
}
