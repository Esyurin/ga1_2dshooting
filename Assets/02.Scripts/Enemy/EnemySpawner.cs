using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class EnemySpawner : MonoBehaviour
{
    [Header("스폰 주기")]
    [SerializeField] private float _maxSpawnInterval = 3f;
    [SerializeField] private float _minSpawnInterval = 1f;

    [Header("Enemy Data Table")]
    [SerializeField] private EnemyDataTableSO _enemyDataTable;

    private float _timer;
    private float _spawnInterval = 3f;

    private readonly List<Enemy> _enemies = new();
    private readonly Dictionary<Enemy, ObjectPool<Enemy>> _enemyPoolMap = new();

    private float _totalSpawnWeight;

    private void Awake()
    {
        foreach (EnemyData data in _enemyDataTable.Datas)
        {
            GameObject enemyPrefab = data.Prefab;

            if (!enemyPrefab.TryGetComponent(out Enemy enemy))
            {
                Debug.LogError($"Enemy 컴포넌트가 없습니다: {enemyPrefab.name}", enemyPrefab);
                continue;
            }

            _enemies.Add(enemy);

            ObjectPool<Enemy> enemyPool = new(() => SpawnEnemy(enemy),
                OnGetEnemy,
                OnReleaseEnemy,
                OnDestroyEnemy,
                true,
                10,
                20);

            _enemyPoolMap.Add(enemy, enemyPool);
        }

        foreach (EnemyData data in _enemyDataTable.Datas)
        {
            _totalSpawnWeight += data.SpawnWeight;
        }
    }

    private void Update()
    {
        _timer += Time.deltaTime;

        if (!(_timer >= _spawnInterval)) return;
        _timer = 0f;
        _spawnInterval = Random.Range(_minSpawnInterval, _maxSpawnInterval);
        _enemyPoolMap[SelectRandomEnemy()].Get();
    }

    private Enemy SelectRandomEnemy()
    {
        float totalSpawnWeight = _totalSpawnWeight;
        float randomValue = Random.value * totalSpawnWeight;

        for (int i = 0; i < _enemies.Count; i++)
        {
            randomValue -= _enemyDataTable.Datas[i].SpawnWeight;
            if (randomValue <= 0f) return _enemies[i];
        }

        return _enemies[^1];
    }

    private Enemy SpawnEnemy(Enemy enemyPrefab)
    {
        Enemy enemy = Instantiate(enemyPrefab, transform.position, transform.rotation, transform);
        enemy.SetPool(_enemyPoolMap[enemyPrefab]);

        return enemy;
    }

    private void OnGetEnemy(Enemy enemy)
    {
        enemy.transform.position = transform.position;
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