using System;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    // 목적: 키보드 입력에 따라서 플레이어 이동 처리를 하고 싶다.

    // 필요 필드:
    [SerializeField] private ReplayRecorder _replayRecorder;
    
    public float Speed = 3f;
    public float SpeedDelta = 1f;
    public float SpeedMin = 0.1f;
    public float SpeedMax = 10f;
    
    public float UpMovementLimit = -1f;
    public float DownMovementLimit = -4.5f;
    public float LeftMovementLimit = -2.4f;
    public float RightMovementLimit = 2.4f;

    private bool isWarp = false;
    
    private float _cumulativeTime = 0f;
    private const float RecordTimeThreshold = 0.1f;
    private Vector3 _recordStartPosition;


    private void Start()
    {
        _recordStartPosition = transform.position;
    }

    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Move(horizontalInput, verticalInput);

        if (Input.GetKey(KeyCode.E))
        {
            IncreaseSpeed();
        }

        if (Input.GetKey(KeyCode.Q))
        {
            DecreaseSpeed();
        }
    }
    
    public void Move(float horizontalInput, float verticalInput)
    {
        Vector2 direction = new(horizontalInput, verticalInput);
        direction.Normalize();

        float newPositionX = transform.position.x + direction.x * (Speed * Time.deltaTime);
        if (newPositionX < LeftMovementLimit)
        {
            newPositionX = RightMovementLimit;
            CreateCommand();
            isWarp = true;
        }

        if (newPositionX > RightMovementLimit)
        {
            newPositionX = LeftMovementLimit;
            CreateCommand();
            isWarp = true;
        }

        float newPositionY = transform.position.y + direction.y * (Speed * Time.deltaTime);
        newPositionY = Mathf.Clamp(newPositionY, DownMovementLimit, UpMovementLimit);
        transform.position = new Vector3(newPositionX, newPositionY, 0);
        
        if (isWarp)
        {
            _recordStartPosition = transform.position;
            isWarp = false;
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

    public void CreateCommand()
    {
        _replayRecorder.AddMoveCommands(gameObject, _recordStartPosition, transform.position, _cumulativeTime);
        _cumulativeTime = 0f;
        _recordStartPosition = transform.position;
    }

    public void IncreaseSpeed()
    {
        Speed = Mathf.Clamp(Speed + SpeedDelta * Time.deltaTime, SpeedMin, SpeedMax);
    }

    public void DecreaseSpeed()
    {
        Speed = Mathf.Clamp(Speed - SpeedDelta * Time.deltaTime, SpeedMin, SpeedMax);
    }
}
