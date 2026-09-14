using System;
using UnityEngine;
using UnityEngine.UI;

public class UI_ButtonClick : MonoBehaviour
{
    private AudioSource _audioSource;
    private Button _button;

    private Vector3 _defaultScale;
    private bool _isBump;
    private float _bumpScale = 1.1f;
    [SerializeField] private float _bumpSpeed = 2f;
    private float _bumpTime;

    private void Awake()
    {
        _defaultScale = transform.localScale;
        _audioSource = GetComponent<AudioSource>();
        _button = GetComponent<Button>();
        _button.onClick.AddListener(PlayButtonBump);
        _button.onClick.AddListener(PlayButtonSound);
    }

    private void Update()
    {
        if (_isBump)
        {
            _bumpTime += Mathf.Sin(Mathf.PI * Time.deltaTime * _bumpSpeed);
            transform.localScale = Vector3.Lerp(_defaultScale, _bumpScale * _defaultScale, _bumpTime);
        }

        if (_bumpTime > Mathf.PI)
        {
            _isBump = false;
            _bumpTime = 0;
        }
    }

    private void PlayButtonBump()
    {
        _isBump = true;
        transform.localScale = _defaultScale;
    }

    private void PlayButtonSound()
    {
        _audioSource.Play();
    }
}