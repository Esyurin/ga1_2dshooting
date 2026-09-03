using UnityEngine;

public class StraightEnemy : Enemy
{
    private Vector3 _moveDirection;

    private void Start()
    {
        _moveDirection = Vector3.down;
        float forwardAngle = Mathf.Atan2(_moveDirection.y, _moveDirection.x) * Mathf.Rad2Deg - 90f;
        transform.rotation = Quaternion.Euler(0f, 0f, forwardAngle);
    }

    protected override void Move()
    {
        transform.Translate(_speed * Time.deltaTime * _moveDirection, Space.World);
    }
}