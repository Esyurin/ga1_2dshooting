using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class EnemySpawner : MonoBehaviour
{
    [Header("스폰 주기")]
    [SerializeField] private float _spawnInterval = 3f;

    [Header("스폰할 적 프리팹")]
    [SerializeField] private Enemy _enemyPrefab;

    private float _timer;
    private ObjectPool<Enemy> _enemyPool;

    private void Awake()
    {
        _enemyPool = new ObjectPool<Enemy>(Spawn, OnGetEnemy, OnReleaseEnemy, OnDestroyEnemy, true, 10, 20);
    }

    private void Update()
    {
        _timer += Time.deltaTime;

        if (_timer >= _spawnInterval)
        {
            _timer = 0f;
            _spawnInterval = Random.Range(1f, 3f);
            _enemyPool.Get();
        }
    }

    private Enemy Spawn()
    {
        Enemy enemy = Instantiate(_enemyPrefab, transform.position, transform.rotation);
        enemy.SetPool(_enemyPool);

        return enemy;
    }

    private void OnGetEnemy(Enemy enemy)
    {
        enemy.transform.SetPositionAndRotation(transform.position, transform.rotation);
        enemy.gameObject.SetActive(true);
    }

    private void OnReleaseEnemy(Enemy enemy)
    {
        enemy.gameObject.SetActive(false);
    }

    private void OnDestroyEnemy(Enemy enemy)
    {
        Destroy(enemy.gameObject);
    }
}