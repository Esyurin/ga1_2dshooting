using UnityEngine;

public class MoveSpeedItem : Item
{
    [SerializeField] private float _moveSpeedBonus = 1f;

    protected override void ApplyEffect(Collider2D playerCollider)
    {
        if (!playerCollider.TryGetComponent(out PlayerMove playerMove))
        {
            Debug.LogError($"{nameof(PlayerMove)} is required on {nameof(Player)}");
            return;
        }

        playerMove.MoveSpeedUp(_moveSpeedBonus);
    }
}