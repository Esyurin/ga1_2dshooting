using UnityEngine;

[CreateAssetMenu(fileName = "EnemyDataTableSO", menuName = "Scriptable Objects/EnemyDataTableSO")]
public class EnemyDataTableSO : ScriptableObject
{
    public EnemyData[] Datas;
}