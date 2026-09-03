using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _enemyHealth = 3f;
    [SerializeField] private float _enemySpeed = 1f;

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        transform.Translate(_enemySpeed * Time.deltaTime * Vector3.down);
    }

    public void DecreaseHealth(float amount)
    {
        _enemyHealth -= amount;

        if (_enemyHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}