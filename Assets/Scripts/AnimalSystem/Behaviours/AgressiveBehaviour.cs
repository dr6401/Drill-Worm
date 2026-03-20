using System;
using UnityEngine;

public class AgressiveBehaviour : IAnimalBehaviour
{
    public float playerDetectionRange = 5f;
    public float chasingRange = 8f;
    public override void Tick(Animal animal)
    {
        Transform player = PlayerStats.Instance.transform;
        if (player == null) return;
        
        float sqrDistance = (player.position - animal.transform.position).sqrMagnitude;

        if (sqrDistance <= playerDetectionRange * playerDetectionRange)
        {
            animal.SetTarget(player);
            animal.SetStateIfNotCurrent(new ChaseState());
            Debug.Log($"Set state to ChaseState");
        }else if (sqrDistance >= chasingRange * chasingRange)
        {
            animal.SetStateIfNotCurrent(new WanderState());
            animal.SetTarget(null);
        }
        Debug.Log($"Distance to player: {Mathf.Sqrt(sqrDistance)}");
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, playerDetectionRange);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, chasingRange);
        Transform player = PlayerStats.Instance.transform;
        if (player == null) return;
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, player.position);
    }
}
