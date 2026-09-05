using UnityEngine;

public class MoveSpeedItem : Item
{
    [SerializeField] private float _moveSpeedBonus = 1f;

    protected override void ApplyEffect(Collider2D playerCollider)
    {
        if (!playerCollider.TryGetComponent(out PlayerMove playerMove))
        {
            throw new MissingComponentException($"{nameof(PlayerMove)} is required on {nameof(Player)}");
        }

        playerMove.MoveSpeedUp(_moveSpeedBonus);
    }
}