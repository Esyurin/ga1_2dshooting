using UnityEngine;

public class HealItem : Item
{
    [SerializeField] private float _healAmount = 1f;

    protected override void ApplyEffect(Collider2D playerCollider)
    {
        if (!playerCollider.TryGetComponent(out Player player))
        {
            throw new MissingComponentException($"{nameof(Player)} is required on {nameof(Player)}");
        }

        player.Heal(_healAmount);
    }
}