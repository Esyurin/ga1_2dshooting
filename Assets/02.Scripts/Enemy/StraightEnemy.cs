using UnityEngine;

public class StraightEnemy : Enemy
{
    [SerializeField] private Vector3 _moveDirection = Vector3.down;

    private void Start()
    {
        Rotate(_moveDirection);
    }

    protected override void Move()
    {
        transform.Translate(_speed * Time.deltaTime * _moveDirection, Space.World);
    }
}