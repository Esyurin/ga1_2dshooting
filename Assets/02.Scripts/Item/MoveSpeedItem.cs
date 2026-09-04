using UnityEngine;

public class MoveSpeedItem : Item
{
    [SerializeField] private float _moveSpeedBonus = 1f;

    protected override void ApplyEffect(Player player)
    {
        player.SpeedUp(_moveSpeedBonus);
    }
}