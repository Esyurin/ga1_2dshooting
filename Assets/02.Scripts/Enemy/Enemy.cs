using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using Random = UnityEngine.Random;

public abstract class Enemy : MonoBehaviour
{
    private static readonly int IsHit = Animator.StringToHash("isHit");

    [Header("Stats")]
    [SerializeField] private float _maxHealth = 3f;
    [SerializeField] protected float _speed = 1f;
    [SerializeField] private float _attackPower = 10f;
    [SerializeField] private int _score;

    [Header("References")]
    [SerializeField] private List<Item> _items = new();
    [SerializeField] private GameObject _deathEffectPrefab;

    private const float ForwardAngleOffset = 90f;
    private const float ItemDropProbability = 0.3f;

    private Animator _animator;
    private AudioSource _damagedAudioSource;

    private float _health;

    private IObjectPool<Enemy> _pool;
    private bool _isReleased;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _damagedAudioSource = GetComponent<AudioSource>();
    }

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
            Release();
        }
    }

    public void TakeDamage(float amount)
    {
        if (_isReleased) return;

        _health -= amount;
        _animator.SetTrigger(IsHit);
        _damagedAudioSource.Play();

        if (_health <= 0)
        {
            _isReleased = true;
            DropItem();

            ScoreManager.Instance.AddScore(_score);

            Release();
        }
    }

    public void SetPool(IObjectPool<Enemy> pool)
    {
        _pool = pool;
    }

    public void OnSpawn()
    {
        _isReleased = false;
        _health = _maxHealth;
    }

    public void Release()
    {
        Instantiate(_deathEffectPrefab, transform.position, transform.rotation);
        _isReleased = true;
        _pool.Release(this);
    }

    public void DropItem()
    {
        if (Random.value > ItemDropProbability) return;

        int randomItemIndex = Random.Range(0, _items.Count);
        Item item = _items[randomItemIndex];
        Instantiate(item, transform.position, transform.rotation);
    }
}