using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _bulletAttackPower = 1f;
    [SerializeField] private float _bulletSpeed = 5f;

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        transform.Translate(_bulletSpeed * Time.deltaTime * Vector3.up);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(gameObject);

        if (other.gameObject.CompareTag("Enemy"))
        {
            if (!other.gameObject.TryGetComponent(out Enemy enemy))
            {
                Debug.LogError("Enemy doesn't have a Enemy Script Component");
                return;
            }

            enemy.TakeDamage(_bulletAttackPower);
        }
    }
}