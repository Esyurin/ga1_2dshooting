using System;
using UnityEngine;

public class PlayerBomb : MonoBehaviour
{
    [SerializeField] private GameObject _bombPrefab;

    [SerializeField] private float _coolTime = 10f;

    private float _timer = 10f;
    private bool _bombReady = true;

    public float Timer => _timer;
    public float CoolTime => _coolTime;

    private void Awake()
    {
        Debug.Assert(_bombPrefab != null, "(PlayerBomb) Bomb Prefab is null");
    }

    private void Update()
    {
        CheckCoolTime();

        if (_bombReady && Input.GetKeyDown(KeyCode.B))
        {
            DropBomb();
        }
    }

    private void CheckCoolTime()
    {
        if (_bombReady) return;

        _timer += Time.deltaTime;

        if (_timer >= _coolTime)
        {
            _bombReady = true;
        }
    }

    private void DropBomb()
    {
        _bombReady = false;
        _timer = 0f;
        Instantiate(_bombPrefab, transform.position, Quaternion.identity);
    }
}