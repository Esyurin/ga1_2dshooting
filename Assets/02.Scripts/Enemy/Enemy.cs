using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] private float _health = 3f;
    [SerializeField] protected float _speed = 1f;
    [SerializeField] private float _attackPower = 10f;

    private const float ForwardAngleOffset = -90f;

    private void Update()
    {
        Move();
    }

    protected abstract void Move();

    protected void Rotate(Vector3 moveDirection)
    {
        float forwardAngle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg + ForwardAngleOffset;
        transform.rotation = Quaternion.Euler(0f, 0f, forwardAngle);
    }

    public void TakeDamage(float amount)
    {
        _health -= amount;

        if (_health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (!other.TryGetComponent(out Player player))
            {
                Debug.LogError("Player doesn't have a Player Script Component");
                return;
            }

            player.TakeDamage(_attackPower);
            Destroy(gameObject);
        }
    }
}