using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class EnemySpawner : MonoBehaviour
{
    [Header("스폰 주기")]
    [SerializeField] private float _maxSpawnInterval = 3f;
    [SerializeField] private float _minSpawnInterval = 1f;

    [Header("스폰할 적 프리팹 및 비율")]
    [SerializeField] private List<Enemy> _enemyPrefabs = new();

    private float _timer;
    private float _spawnInterval = 3f;

    private Dictionary<Enemy, ObjectPool<Enemy>> _enemyPoolMap = new();

    private float _totalSpawnWeight;

    private void Awake()
    {
        foreach (Enemy enemy in _enemyPrefabs)
        {
            ObjectPool<Enemy> enemyPool = new ObjectPool<Enemy>(
                () => SpawnEnemy(enemy),
                OnGetEnemy,
                OnReleaseEnemy,
                OnDestroyEnemy,
                true,
                10,
                20);
            _enemyPoolMap.Add(enemy, enemyPool);
        }

        foreach (Enemy enemy in _enemyPrefabs)
        {
            _totalSpawnWeight += enemy.SpawnWeight;
        }
    }

    private void Update()
    {
        _timer += Time.deltaTime;

        if (_timer >= _spawnInterval)
        {
            _timer = 0f;
            _spawnInterval = Random.Range(_minSpawnInterval, _maxSpawnInterval);
            _enemyPoolMap[SelectRandomEnemy()].Get();
        }
    }

    private Enemy SelectRandomEnemy()
    {
        float totalSpawnWeight = _totalSpawnWeight;
        float randomValue = Random.value * totalSpawnWeight;

        foreach (Enemy enemy in _enemyPrefabs)
        {
            randomValue -= enemy.SpawnWeight;

            if (randomValue <= 0f)
            {
                return enemy;
            }
        }

        return _enemyPrefabs[^1];
    }

    // TODO: ScriptableObject를 사용해서 리팩토링

    private Enemy SpawnEnemy(Enemy enemyPrefab)
    {
        Enemy enemy = Instantiate(enemyPrefab, transform.position, transform.rotation);
        enemy.SetPool(_enemyPoolMap[enemyPrefab]);

        return enemy;
    }

    private void OnGetEnemy(Enemy enemy)
    {
        enemy.transform.SetPositionAndRotation(transform.position, transform.rotation);
        enemy.gameObject.SetActive(true);
        enemy.OnSpawn();
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