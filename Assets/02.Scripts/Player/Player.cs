using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject _deathEffectPrefab;

    [SerializeField] private float _health = 100f;

    public void TakeDamage(float damage)
    {
        _health -= damage;

        if (_health <= 0f)
        {
            Instantiate(_deathEffectPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    public void Heal(float value)
    {
        _health += value;
    }
}