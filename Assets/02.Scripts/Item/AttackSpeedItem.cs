using UnityEngine;

public class AttackSpeedItem : Item
{
    [SerializeField] private float _attackSpeedBonus = 0.01f;

    protected override void ApplyEffect(Player player)
    {
        player.AttackSpeedUp(_attackSpeedBonus);
    }
}