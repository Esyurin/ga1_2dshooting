// 데이터 클래스: 순수하게 데이터를 보관하고 전달하는 목적으로 생성하는 클래스

using UnityEngine;

[System.Serializable]
public class EnemyData
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private float _maxHealth;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _attackPower;
    [SerializeField] private float _score;
    [SerializeField] private float _spawnWeight;

    public GameObject Prefab => _prefab;
    public float MaxHealth => _maxHealth;
    public float MoveSpeed => _moveSpeed;
    public float AttackPower => _attackPower;
    public float Score => _score;
    public float SpawnWeight => _spawnWeight;

    public EnemyData(GameObject prefab, float maxHealth, float moveSpeed, float attackPower, float score,
        float spawnWeight)
    {
        _prefab = prefab;
        _maxHealth = maxHealth;
        _moveSpeed = moveSpeed;
        _attackPower = attackPower;
        _score = score;
        _spawnWeight = spawnWeight;
    }
}