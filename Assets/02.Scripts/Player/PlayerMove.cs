using System;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private static readonly int X = Animator.StringToHash("x");

    [Header("References")]
    [SerializeField] private BoxCollider2D _moveZone;
    [SerializeField] private ReplayRecorder _replayRecorder;
    [SerializeField] private Animator _animator;

    [Header("Speed")]
    [SerializeField] private float _speed = 3f;
    [SerializeField] private float _speedMin = 0.1f;
    [SerializeField] private float _speedMax = 10f;

    private float _upMovementLimit;
    private float _downMovementLimit;
    private float _leftMovementLimit;
    private float _rightMovementLimit;

    private bool _isWarp = false;

    private float _cumulativeTime = 0f;
    private const float RecordTimeThreshold = 0.1f;
    private Vector3 _recordStartPosition;


    private void Awake()
    {
        Bounds bounds = _moveZone.bounds;
        _upMovementLimit = bounds.max.y;
        _downMovementLimit = bounds.min.y;
        _leftMovementLimit = bounds.min.x;
        _rightMovementLimit = bounds.max.x;
    }

    private void Start()
    {
        _recordStartPosition = transform.position;
    }

    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Move(horizontalInput, verticalInput);
        UpdateAnimation(horizontalInput);
    }

    public void Move(float horizontalInput, float verticalInput)
    {
        Vector2 direction = new(horizontalInput, verticalInput);
        direction.Normalize();

        float newPositionX = transform.position.x + direction.x * (_speed * Time.deltaTime);
        if (newPositionX < _leftMovementLimit)
        {
            newPositionX = _rightMovementLimit;
            CreateCommand();
            _isWarp = true;
        }

        if (newPositionX > _rightMovementLimit)
        {
            newPositionX = _leftMovementLimit;
            CreateCommand();
            _isWarp = true;
        }

        float newPositionY = transform.position.y + direction.y * (_speed * Time.deltaTime);
        newPositionY = Mathf.Clamp(newPositionY, _downMovementLimit, _upMovementLimit);
        transform.position = new Vector3(newPositionX, newPositionY, 0);

        if (_isWarp)
        {
            _recordStartPosition = transform.position;
            _isWarp = false;
        }

        if (!_replayRecorder.IsReplaying)
        {
            _cumulativeTime += Time.deltaTime;
            if (_cumulativeTime > RecordTimeThreshold)
            {
                CreateCommand();
            }
        }
    }

    private void CreateCommand()
    {
        _replayRecorder.AddMoveCommands(gameObject, _recordStartPosition, transform.position, _cumulativeTime);
        _cumulativeTime = 0f;
        _recordStartPosition = transform.position;
    }

    private void UpdateAnimation(float horizontalInput)
    {
        int x = horizontalInput switch
        {
            > 0f => 1,
            < 0f => -1,
            _ => 0
        };

        if (_animator.GetInteger(X) != x)
        {
            _animator.SetInteger(X, x);
        }
    }

    public void MoveSpeedUp(float value)
    {
        _speed = Mathf.Clamp(_speed + value * Time.deltaTime, _speedMin, _speedMax);
    }

    public void MoveSpeedDown(float value)
    {
        _speed = Mathf.Clamp(_speed - value * Time.deltaTime, _speedMin, _speedMax);
    }
}