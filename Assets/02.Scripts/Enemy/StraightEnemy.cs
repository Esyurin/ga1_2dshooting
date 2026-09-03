using UnityEngine;

public class StraightEnemy : Enemy
{
    private Vector3 _moveDirection;

    private void Start()
    {
        _moveDirection = Vector3.down;
        Rotate(_moveDirection);
    }

    protected override void Move()
    {
        transform.Translate(_speed * Time.deltaTime * _moveDirection, Space.World);
    }
}