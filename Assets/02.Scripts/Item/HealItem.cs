using UnityEngine;

public class HealItem : Item
{
    [SerializeField] private float _healAmount = 1f;

    protected override void ApplyEffect(Collider2D playerCollider)
    {
        if (!playerCollider.TryGetComponent(out Player player))
        {
            Debug.LogError($"{nameof(Player)} is required on {nameof(Player)}");
            return;
        }

        player.Heal(_healAmount);
    }
}