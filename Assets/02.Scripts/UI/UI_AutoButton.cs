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

    private Vector3 _defaultScale;
    private float _pulseScale = 0.1f;
    private float _pulseSpeed = 2f;

    private void Awake()
    {
        _image = GetComponent<Image>();
        _audioSource = GetComponent<AudioSource>();

        _player = FindAnyObjectByType<Player>();
        _playerFire = _player.GetComponent<PlayerFire>();
        _playerAutoMove = _player.GetComponent<PlayerAutoMove>();

        _defaultScale = transform.localScale;
    }

    private void Start()
    {
        _isAuto = _playerFire.IsAuto;
    }

    private void Update()
    {
        PlayButtonPulse();
    }

    private void PlayButtonPulse()
    {
        transform.localScale = _defaultScale + _pulseScale * Mathf.Sin(_pulseSpeed * Time.time) * Vector3.one;
    }

    public void AutoToggle()
    {
        _isAuto = !_isAuto;
        _playerFire.ToggleAuto();
        _playerAutoMove.enabled = _isAuto;
        _image.sprite = _isAuto ? _onSprite : _offSprite;
        _audioSource.Play();
    }
}