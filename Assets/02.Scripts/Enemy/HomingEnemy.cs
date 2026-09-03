using UnityEngine;

public class HomingEnemy : Enemy
{
    private Transform _player;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    protected override void Move()
    {
        Vector3 moveDirection = _player.position - transform.position;
        moveDirection.Normalize();

        float forwardAngle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg - 90f;
        transform.rotation = Quaternion.Euler(0f, 0f, forwardAngle);

        transform.Translate(_speed * Time.deltaTime * moveDirection, Space.World);
    }
}