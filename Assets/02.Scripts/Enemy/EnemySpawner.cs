using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("스폰 주기")]
    [SerializeField] private float _spawnInterval = 3f;

    [Header("스폰할 적 프리팹")]
    [SerializeField] private Enemy _enemyPrefab;

    private float _timer;

    private void Update()
    {
        _timer += Time.deltaTime;

        if (_timer >= _spawnInterval)
        {
            _timer = 0f;
            Spawn();
        }
    }

    private void Spawn()
    {
        Instantiate(_enemyPrefab, transform.position, transform.rotation);
    }
}