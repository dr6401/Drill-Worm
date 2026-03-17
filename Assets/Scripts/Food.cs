using System;
using Unity.VisualScripting;
using UnityEngine;

public class Food : MonoBehaviour
{
    public int xpOnConsume;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"Collided with {other.gameObject.name}");
        if (other.CompareTag("Head"))
        {
            PlayerStats.Instance.Consume(xpOnConsume);
            Destroy(gameObject);
        }
    }
}
