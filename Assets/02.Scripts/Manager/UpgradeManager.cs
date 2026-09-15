using System;
using UnityEngine;

public enum UpgradeType
{
    AttackPower,
    AttackSpeed,
    MoveSpeed
}

public class UpgradeManager : MonoBehaviour
{
    private const string UpgradeSaveDataKey = "UpgradeSaveData";

    [SerializeField] private Upgrade[] _upgrades;

    private PlayerFire _playerFire;
    private PlayerMove _playerMove;

    public Upgrade[] Upgrades => _upgrades;

    private void Awake()
    {
        _playerFire = GetComponent<PlayerFire>();
        _playerMove = GetComponent<PlayerMove>();
        Load();
    }

    public void Upgrade(UpgradeType type)
    {
        ScoreManager scoreManager = ScoreManager.Instance;
        Upgrade upgrade = _upgrades[(int)type];
        if (scoreManager.CurrentScore < upgrade.Cost) return;

        ApplyEffect(upgrade.Type, upgrade.UpgradeAmount);
        scoreManager.SpendScore(upgrade.Cost);
        upgrade.IncreaseLevel();
        Save();
    }

    private void ApplyEffect(UpgradeType type, float amount)
    {
        switch (type)
        {
            case UpgradeType.AttackPower:
                _playerFire.IncreaseAttackDamage(amount);
                break;
            case UpgradeType.AttackSpeed:
                _playerFire.AttackSpeedUp(amount);
                break;
            case UpgradeType.MoveSpeed:
                _playerMove.MoveSpeedUp(amount);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }
    }

    private void Save()
    {
        UpgradeSaveData saveData = new(_upgrades.Length);
        for (int i = 0; i < _upgrades.Length; i++)
        {
            saveData.Name[i] = _upgrades[i].Name;
            saveData.Level[i] = _upgrades[i].Level;
        }

        string json = JsonUtility.ToJson(saveData);
        PlayerPrefs.SetString(UpgradeSaveDataKey, json);
        PlayerPrefs.Save();
    }

    private void Load()
    {
        string json = PlayerPrefs.GetString(UpgradeSaveDataKey, null);
        if (string.IsNullOrEmpty(json))
        {
            Debug.Log("Upgrade save data not found");
            return;
        }

        UpgradeSaveData saveData = JsonUtility.FromJson<UpgradeSaveData>(json);
        if (saveData?.Level == null) return;

        for (int i = 0; i < Mathf.Min(_upgrades.Length, saveData.Level.Length); i++)
        {
            _upgrades[i].Initialize(saveData.Level[i]);
            if (_upgrades[i].Level > 1)
            {
                ApplyEffect(_upgrades[i].Type, _upgrades[i].TotalUpgradeAmount);
            }
        }
    }
}
