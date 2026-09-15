using UnityEngine;

[CreateAssetMenu(fileName = "EnemyBalanceDataTableSO", menuName = "Scriptable Objects/EnemyBalanceDataTableSO")]
public class EnemyBalanceDataTableSO : ScriptableObject
{
    [SerializeField] EnemyBalanceData[] _datas;

    public EnemyBalanceData[] Datas => _datas;
}