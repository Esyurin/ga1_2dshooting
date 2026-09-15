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
    public float UpgradeAmount => _upgradeAmount + (_level - 1) * _upgradeAmountIncrease;
    public float NextUpgradeAmount => UpgradeAmount + _upgradeAmountIncrease;
    public int Cost => _cost + (_level - 1) * _costIncrease;
    public int Level => _level;

    public float TotalUpgradeAmount =>
        (_level - 1) * (_upgradeAmount + (_level - 2) * _upgradeAmountIncrease * 0.5f);

    public void Initialize(int level)
    {
        _level = Mathf.Max(1, level);
    }

    public void IncreaseLevel()
    {
        _level++;
    }
}
