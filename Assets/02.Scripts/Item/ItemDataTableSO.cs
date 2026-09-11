using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemDataTableSO", menuName = "Scriptable Objects/ItemDataTableSO")]
public class ItemDataTableSO : ScriptableObject
{
    [SerializeField] private List<ItemData> _data;

    public IReadOnlyList<ItemData> Data => _data;
}