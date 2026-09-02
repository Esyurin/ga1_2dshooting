using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    // 목적: 키보드 입력에 따라서 플레이어 이동 처리를 하고 싶다.

    // 필요 필드:
    public float Speed;
    public float UpMovementLimit;
    public float DownMovementLimit;
    public float LeftMovementLimit;
    public float RightMovementLimit;
    
    // 매 프레임마다 실행된다.
    // 초당 프레임 실행 횟수는: 별다른 설정이 없을 경우 가능한 많이
    
    /*
    // 1. 키보드 입력을 받는다.
    if (Input.GetKey(KeyCode.LeftArrow))
    {
        Debug.Log("왼쪽 방향키를 누르는 중");
    
        // 2. 키보드 입력에 따라 방향을 구한다.
        // 게임에는 벡터라는 타입이 있다. 벡터는 크기와 방향을 의미한다.
        Vector2 leftDirection = new Vector2(-1, 0); // 왼쪽 방향
        // Vector2 direction = Vector2.left;
        
        // 3. 방향과 속력에 따라 이동한다.
        // 속도 = 방향 * 속력
        // 매직 넘버란: 보는 사람에 따라 의미가 달라질 수 있는 헷갈리는 숫자
        transform.Translate(leftDirection * (Speed * Time.deltaTime));
        // deltaTime: 이전 프레임으로부터 지금 프레임까지 시간이 얼마나 지났는지 ms로 반환
    }
    */
    
    private void Update()
    {
        // TODO: 특정 영역 내부에서만 이동 가능하되 좌우 이동 범위의 끝에 도달하면 반대 방향에서 나타나도록 구현
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector2 direction = new(h, v);
        direction.Normalize();

        float newX = transform.position.x + direction.x * (Speed * Time.deltaTime);
        if (newX < LeftMovementLimit)
        {
            newX = RightMovementLimit;
        }

        if (newX > RightMovementLimit)
        {
            newX = LeftMovementLimit;
        }
        
        float newY = Mathf.Clamp(transform.position.y + direction.y * (Speed * Time.deltaTime), 
            DownMovementLimit, UpMovementLimit);
        transform.position = new Vector3(newX, newY, 0);
    }
}
