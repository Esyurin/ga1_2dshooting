using UnityEngine;

public class PlayerAutoMove : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float _ignoreDistanceThreshold;
    [SerializeField] private float _ignoreAngleThreshold;
    [SerializeField] private float _retreatTriggerDistance;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _maxMoveY;

    private void Update()
    {
        GameObject targetEnemy = TargetEnemy(out float minDistance);

        if (!targetEnemy) return;

        Move(targetEnemy, minDistance);
    }

    private GameObject TargetEnemy(out float minDistance)
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject targetEnemy = null;
        minDistance = float.MaxValue;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            Vector3 direction = enemy.transform.position - transform.position;
            float angle = Vector3.Angle(Vector3.up, direction);

            if (distance < _ignoreDistanceThreshold && angle >= _ignoreAngleThreshold)
            {
                continue;
            }

            if (distance < minDistance)
            {
                minDistance = distance;
                targetEnemy = enemy;
            }
        }

        return targetEnemy;
    }

    private void Move(GameObject targetEnemy, float minDistance)
    {
        float moveDirectionX = targetEnemy.transform.position.x - transform.position.x;
        float deltaY = targetEnemy.transform.position.y - transform.position.y;

        float moveDirectionY = minDistance < _retreatTriggerDistance
            ? -Mathf.Max(Mathf.Abs(deltaY), 1f)
            : deltaY;

        Vector3 moveDirection = new(moveDirectionX, moveDirectionY, 0);
        moveDirection.Normalize();
        Vector3 nextPosition = transform.position + Time.deltaTime * _moveSpeed * moveDirection;
        nextPosition.y = Mathf.Min(nextPosition.y, _maxMoveY);
        transform.position = nextPosition;
    }
}