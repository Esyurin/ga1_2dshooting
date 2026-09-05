using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _health = 100f;

    public void TakeDamage(float damage)
    {
        _health -= damage;

        if (_health <= 0f)
        {
            Destroy(gameObject);
        }
    }

    public void Heal(float value)
    {
        _health += value;
    }
}