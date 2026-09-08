using UnityEngine;

public class BombCoolTimeBar : MonoBehaviour
{
    [SerializeField] private PlayerBomb _playerBomb;
    [SerializeField] private Transform _bombCoolTimeBar;

    private float _scaleMultiplier = 1.5f;

    private void Update()
    {
        var scale = _bombCoolTimeBar.localScale;
        scale.x = _playerBomb.Timer / _playerBomb.CoolTime * _scaleMultiplier;
        _bombCoolTimeBar.localScale = scale;
    }
}