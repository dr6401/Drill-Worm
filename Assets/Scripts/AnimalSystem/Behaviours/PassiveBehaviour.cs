using UnityEngine;

public class PassiveBehaviour : IAnimalBehaviour
{
    public override void Tick(Animal animal)
    {
        // Idle for 3 s -> Choose destination -> Go towards it -> Idle for 3s -> repeat 
    }
}
