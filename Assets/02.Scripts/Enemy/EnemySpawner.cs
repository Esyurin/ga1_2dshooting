using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class EnemySpawner : MonoBehaviour
{
    [Header("스폰 주기")]
    [SerializeField] private float _spawnInterval = 3f;

    [Header("스폰할 적 프리팹 및 비율")]
    [SerializeField] private List<Enemy> _enemyPrefabs = new();

    private float _timer;
    private Dictionary<Enemy, ObjectPool<Enemy>> _enemyPoolMap = new();

    private List<Enemy> _shuffledEnemies = new();
    private int _spawnCount;

    private void Awake()
    {
        PrepareShuffledEnemies();

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
    }

    private void Update()
    {
        _timer += Time.deltaTime;

        if (_timer >= _spawnInterval)
        {
            if (_spawnCount >= _shuffledEnemies.Count)
            {
                _spawnCount = 0;
                PrepareShuffledEnemies();
            }

            _timer = 0f;
            _spawnInterval = Random.Range(1f, 3f);
            _enemyPoolMap[_shuffledEnemies[_spawnCount]].Get();
            _spawnCount++;
        }
    }

    private void PrepareShuffledEnemies()
    {
        _shuffledEnemies.Clear();

        foreach (Enemy enemy in _enemyPrefabs)
        {
            for (int i = 0; i <= enemy.SpawnProbability * 100; i++)
            {
                _shuffledEnemies.Add(enemy);
            }
        }

        for (int i = _shuffledEnemies.Count - 1; i >= 0; i--)
        {
            int j = Random.Range(0, i + 1);
            (_shuffledEnemies[i], _shuffledEnemies[j]) = (_shuffledEnemies[j], _shuffledEnemies[i]);
        }
    }

    private Enemy SpawnEnemy(Enemy enemyPrefab)
    {
        Enemy enemy = Instantiate(enemyPrefab, transform.position, transform.rotation);
        enemy.SetPool(_enemyPoolMap[enemyPrefab]);
        enemy.OnSpawn();

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