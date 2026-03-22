using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Food : MonoBehaviour
{
    public int xpOnConsume;
    public int hpOnConsume;
    private Collider2D collider;

    private void Start()
    {
        collider = GetComponent<Collider2D>();
        StartCoroutine(DisablePickupForAWEhile());
    }

    private IEnumerator DisablePickupForAWEhile()
    {
        collider.enabled = false;
        yield return new WaitForSeconds(0.3f);
        collider.enabled = true;
    }

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
