using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] private float _health = 3f;
    [SerializeField] protected float _speed = 1f;

    private const float ForwardAngleOffset = 90f;

    private void Update()
    {
        Move();
    }

    protected abstract void Move();

    public void DecreaseHealth(float amount)
    {
        _health -= amount;

        if (_health <= 0)
        {
            Destroy(gameObject);
        }
    }
}