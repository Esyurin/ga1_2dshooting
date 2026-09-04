using System;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (!other.TryGetComponent(out Enemy enemy))
            {
                Debug.LogError("Enemy doesn't have a Enemy Script Component");
                return;
            }

            enemy.Release();
            return;
        }

        Destroy(other.gameObject);
    }
}