using System;
using UnityEngine;
using UnityEngine.UI;

public class UI_AutoButton : MonoBehaviour
{
    [Header("UI Sprites")]
    [SerializeField] private Sprite _onSprite;
    [SerializeField] private Sprite _offSprite;

    private Image _image;
    private AudioSource _audioSource;

    private bool _isAuto;
    private Player _player;
    private PlayerFire _playerFire;
    private PlayerAutoMove _playerAutoMove;

    private void Awake()
    {
        _image = GetComponent<Image>();
        _audioSource = GetComponent<AudioSource>();

        _player = FindAnyObjectByType<Player>();
        _playerFire = _player.GetComponent<PlayerFire>();
        _playerAutoMove = _player.GetComponent<PlayerAutoMove>();
    }

    private void Start()
    {
        _isAuto = _playerFire.IsAuto;
    }

    public void AutoToggle()
    {
        _isAuto = !_isAuto;
        _playerFire.ToggleAuto();
        _playerAutoMove.enabled = _isAuto;
        _image.sprite = _isAuto ? _onSprite : _offSprite;
    }
}