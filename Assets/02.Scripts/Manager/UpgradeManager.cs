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
    }

    private void Start()
    {
        Load();
    }

    public void Upgrade(UpgradeType type)
    {
        ScoreManager scoreManager = ScoreManager.Instance;
        Upgrade upgrade = _upgrades[(int)type];
        switch (type)
        {
            case UpgradeType.AttackPower:
                if (scoreManager.CurrentScore < upgrade.Cost) return;
                scoreManager.SpendScore(upgrade.Cost);
                _playerFire.IncreaseAttackDamage(upgrade.UpgradeAmount);
                break;
            case UpgradeType.AttackSpeed:
                if (scoreManager.CurrentScore < upgrade.Cost) return;
                scoreManager.SpendScore(upgrade.Cost);
                _playerFire.AttackSpeedUp(upgrade.UpgradeAmount);
                break;
            case UpgradeType.MoveSpeed:
                if (scoreManager.CurrentScore < upgrade.Cost) return;
                scoreManager.SpendScore(upgrade.Cost);
                _playerMove.MoveSpeedUp(upgrade.UpgradeAmount);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }
        upgrade.IncreaseLevel();
        Save();
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
        for (int i = 0; i < _upgrades.Length; i++)
        {
            _upgrades[i].Initialize(saveData.Level[i]);
        }
    }
}