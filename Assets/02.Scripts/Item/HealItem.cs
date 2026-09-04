using UnityEngine;

public class HealItem : Item
{
    [SerializeField] private float _healAmount = 1f;

    protected override void ApplyEffect(Player player)
    {
        player.Heal(_healAmount);
    }
}