using UnityEngine;

[System.Serializable]
public class Upgrade
{
    [SerializeField] private UpgradeType _type;
    [SerializeField] private string _name;
    [SerializeField] private float _upgradeAmount;
    [SerializeField] private float _upgradeAmountIncrease;
    [SerializeField] private int _cost;
    [SerializeField] private int _costIncrease;

    private int _level = 1;

    public UpgradeType Type => _type;
    public string Name => _name;
    public float UpgradeAmount => _upgradeAmount;
    public float NextUpgradeAmount => _upgradeAmount + _upgradeAmountIncrease;
    public int Cost => _cost;
    public int Level => _level;

    public void IncreaseLevel()
    {
        _level++;
        _upgradeAmount += _upgradeAmountIncrease;
        _cost += _costIncrease;
    }
}