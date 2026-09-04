using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _health = 100f;
    [SerializeField] private PlayerFire _playerFire;
    [SerializeField] private PlayerMove _playerMove;

    private void Awake()
    {
        _playerFire ??= GetComponent<PlayerFire>();
        _playerMove ??= GetComponent<PlayerMove>();
    }

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

    public void AttackSpeedUp(float value)
    {
        _playerFire.AttackSpeedUp(value);
    }

    public void SpeedUp(float value)
    {
        _playerMove.SpeedUp(value);
    }
}