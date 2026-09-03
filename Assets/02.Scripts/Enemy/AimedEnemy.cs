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

        float forwardAngle = Mathf.Atan2(_moveDirection.y, _moveDirection.x) * Mathf.Rad2Deg - 90f;
        transform.rotation = Quaternion.Euler(0f, 0f, forwardAngle);
    }

    protected override void Move()
    {
        transform.Translate(_speed * Time.deltaTime * _moveDirection, Space.World);
    }
}