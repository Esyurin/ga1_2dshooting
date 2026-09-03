using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _health = 3f;
    [SerializeField] private float _speed = 1f;

    protected Transform Player;
    protected Vector3 MoveDirection = Vector3.down;

    private const float ForwardAngleOffset = 90f;

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        OnStart();
    }

    protected virtual void OnStart()
    {
    }

    private void Update()
    {
        UpdateDirection();
        Move();
    }

    protected virtual void UpdateDirection()
    {
    }

    private void Move()
    {
        float forwardAngle = Mathf.Atan2(MoveDirection.y, MoveDirection.x) * Mathf.Rad2Deg + 90f;
        transform.rotation = Quaternion.Euler(0f, 0f, forwardAngle);
        transform.Translate(_speed * Time.deltaTime * MoveDirection);
    }

    public void DecreaseHealth(float amount)
    {
        _health -= amount;

        if (_health <= 0)
        {
            Destroy(gameObject);
        }
    }
}