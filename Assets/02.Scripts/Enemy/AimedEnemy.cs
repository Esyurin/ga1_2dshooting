using System;
using UnityEngine;

public class AimedEnemy : Enemy
{
    protected override void OnStart()
    {
        MoveDirection = Player.position - transform.position;
        MoveDirection.Normalize();
    }
}