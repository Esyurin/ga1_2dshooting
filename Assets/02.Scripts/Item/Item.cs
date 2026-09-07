using Unity.Mathematics;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    [SerializeField] private float _stationaryDuration = 10f;
    [SerializeField] private float _speed = 10f;
    [SerializeField] private float _curveFadeDistance = 5f;
    [SerializeField] private float _followStrength = 3f;

    private Transform _player;
    private float _timer;

    private const float MidPointT = 0.5f;
    private float _curveSide = 1f;

    private void Start()
    {
        if (GameManager.Instance == null)
        {
            Debug.LogError("GameManager is null");
            return;
        }

        _player = GameManager.Instance.Player.transform;
        _curveSide = transform.position.x < _player.position.x ? -1f : 1f;
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
        if (!_player) return;

        Vector2 start = transform.position;
        Vector2 end = _player.position;

        Vector2 direction = (end - start);
        float distance = direction.magnitude;

        if (distance < Mathf.Epsilon) return;

        direction.Normalize();
        Vector2 perpendicular = new Vector2(-direction.y, direction.x) * _curveSide;

        float curveRatio = Mathf.Clamp01(distance / _curveFadeDistance); // 곡률 = 거리 / 보정값
        float currentCurveAmount = curveRatio * curveRatio; // 수직벡터 보정값

        Vector2 control = Vector2.Lerp(start, end, MidPointT) + perpendicular * currentCurveAmount;
        float followAmount = Mathf.Clamp01(_followStrength * Time.deltaTime); // 이동량
        transform.position = GetBezierPoint(start, control, end, followAmount);
    }

    private Vector2 GetBezierPoint(Vector2 start, Vector2 control, Vector2 end, float t)
    {
        float u = 1f - t;
        return u * u * start + 2f * u * t * control + t * t * end;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        ApplyEffect(other);
        Destroy(gameObject);
    }

    protected abstract void ApplyEffect(Collider2D playerCollider);
}