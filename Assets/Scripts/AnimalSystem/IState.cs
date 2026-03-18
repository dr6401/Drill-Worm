using UnityEngine;

public interface IState
{
    
    void Enter(Animal animal);
    void Update(Animal animal);
    void Exit(Animal animal);
}
