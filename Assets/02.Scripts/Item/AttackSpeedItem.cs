using UnityEngine;

public class AttackSpeedItem : Item
{
    [SerializeField] private float _attackSpeedBonus = 0.01f;

    protected override void ApplyEffect(Collider2D playerCollider)
    {
        if (!playerCollider.TryGetComponent(out PlayerFire playerFire))
        {
            Debug.LogError($"{nameof(PlayerMove)} is required on {nameof(Player)}");
            return;
        }

        playerFire.AttackSpeedUp(_attackSpeedBonus);
    }
}