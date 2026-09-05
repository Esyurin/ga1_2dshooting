using UnityEngine;

public class HomingEnemy : Enemy
{
    private Transform _player;

    private void Start()
    {
        _player = GameManager.Instance.Player.transform;
    }

    protected override void Move()
    {
        if (_player == null) return;

        Vector3 moveDirection = _player.position - transform.position;
        moveDirection.Normalize();

        Rotate(moveDirection);

        transform.Translate(_speed * Time.deltaTime * moveDirection, Space.World);
    }
}