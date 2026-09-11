using UnityEngine;

[System.Serializable]
public class ItemData
{
    [SerializeField] private string _name;
    [SerializeField] private Item _prefab;
    [SerializeField] private float _dropWeight;

    public string Name => _name;
    public Item Prefab => _prefab;
    public float DropWeight => _dropWeight;
}