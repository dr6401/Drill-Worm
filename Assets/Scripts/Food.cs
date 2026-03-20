using System;
using Unity.VisualScripting;
using UnityEngine;

public class Food : MonoBehaviour
{
    public int xpOnConsume;
    public int hpOnConsume;

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log($"Collided with {other.gameObject.name}");
        if (other.CompareTag("Head"))
        {
            PlayerStats.Instance.Consume(xpOnConsume);
            PlayerStats.Instance.Heal(hpOnConsume);
            Destroy(gameObject);
        }
    }
}
