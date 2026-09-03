using System;
using UnityEngine;

public class AimedEnemy : Enemy
{
    private Transform _player;
    private Vector3 _moveDirection;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _moveDirection = _player.position - transform.position;
        _moveDirection.Normalize();

        Rotate(_moveDirection);
    }

    protected override void Move()
    {
        transform.Translate(_speed * Time.deltaTime * _moveDirection, Space.World);
    }
}