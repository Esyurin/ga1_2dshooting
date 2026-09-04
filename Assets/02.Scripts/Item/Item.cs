using UnityEngine;

public abstract class Item : MonoBehaviour
{
    [SerializeField] private float _stationaryDuration = 10f;
    [SerializeField] private float _speed = 10f;

    private Transform _player;
    private float _timer;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (_timer < _stationaryDuration)
        {
            _timer += Time.deltaTime;
        }
        else
        {
            Move();
        }
    }

    private void Move()
    {
        Vector3 moveDirection = _player.position - transform.position;
        moveDirection.Normalize();

        transform.Translate(_speed * Time.deltaTime * moveDirection, Space.World);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        if (!other.TryGetComponent(out Player player))
        {
            Debug.LogError("Player doesn't have a Player Script Component");
            return;
        }

        ApplyEffect(player);
        Destroy(gameObject);
    }

    protected abstract void ApplyEffect(Player player);
}