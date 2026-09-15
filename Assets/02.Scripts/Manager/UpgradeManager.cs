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
    [SerializeField] private Upgrade[] _upgrades;

    PlayerFire _playerFire;
    PlayerMove _playerMove;

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
        // 유의미한 정보(레벨)만 저장
        for (int i = 0; i < _upgrades.Length; i++)
        {
            PlayerPrefs.SetInt($"{_upgrades[i].Name} Upgrade Level", _upgrades[i].Level);
        }
        PlayerPrefs.Save();
    }

    private void Load()
    {
        for (int i = 0; i < _upgrades.Length; i++)
        {
            int level = PlayerPrefs.GetInt($"{_upgrades[i].Name} Upgrade Level", 1);
            _upgrades[i].Initialize(level);
        }
    }
}