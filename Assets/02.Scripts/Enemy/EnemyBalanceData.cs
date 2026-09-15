using UnityEngine;

[System.Serializable]
public class EnemyBalanceData
{
    [SerializeField] private float _bestScore;
    [SerializeField] private float _enemyHealthMultiplier;

    public float BestScore => _bestScore;
    public float EnemyHealthMultiplier => _enemyHealthMultiplier;
}