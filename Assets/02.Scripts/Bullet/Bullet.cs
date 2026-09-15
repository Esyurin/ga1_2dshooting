using UnityEngine;
using UnityEngine.Pool;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _defaultAttackDamage = 1f;
    [SerializeField] private float _moveSpeed = 5f;

    private AudioSource _audioSource;

    private float _damage;

    private IObjectPool<Bullet> _pool;
    private bool _isReleased;

    private void Awake()
    {
        _damage = _defaultAttackDamage;
        _audioSource = GetComponent<AudioSource>();
        _audioSource.pitch = Random.Range(0.9f, 1.1f);
        _audioSource.Play();
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        transform.Translate(_moveSpeed * Time.deltaTime * Vector3.up);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            if (!other.gameObject.TryGetComponent(out Enemy enemy))
            {
                Debug.LogError("Enemy doesn't have a Enemy Script Component");
                return;
            }

            enemy.TakeDamage(_damage);
            Release();
        }
    }

    public void SetPool(IObjectPool<Bullet> pool)
    {
        _pool = pool;
    }

    public void OnSpawn(float attackDamageBonus = 0f)
    {
        _damage = _defaultAttackDamage + attackDamageBonus;
        _isReleased = false;
    }

    public void Release()
    {
        if (_isReleased) return;

        _pool.Release(this);
        _isReleased = true;
    }

    public void IncreaseAttackDamage(float amount)
    {
        _damage += amount;
    }
}
