using UnityEngine;

public class HomingEnemy : Enemy
{
    protected override void UpdateDirection()
    {
        MoveDirection = Player.position - transform.position;
        MoveDirection.Normalize();
    }
}