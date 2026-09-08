using System;
using UnityEngine;

public class PlayerBomb : MonoBehaviour
{
    [SerializeField] private GameObject _bombPrefab;

    [SerializeField] private float _coolTime = 10f;

    private float _timer;
    private bool _bombReady = true;

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
        if (!_bombReady)
        {
            _timer += Time.deltaTime;
        }
        else
        {
            _timer = 0f;
        }
    }

    private void DropBomb()
    {
        _bombReady = false;
        Instantiate(_bombPrefab, transform.position, Quaternion.identity);
    }
}