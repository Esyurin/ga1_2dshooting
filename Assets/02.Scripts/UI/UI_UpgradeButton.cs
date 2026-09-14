using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_UpgradeButton : MonoBehaviour
{
    [SerializeField] private UpgradeManager _upgradeManager;
    [SerializeField] private UpgradeType _upgradeType;

    [SerializeField] private TextMeshProUGUI _titleText;
    [SerializeField] private TextMeshProUGUI _valueText;
    [SerializeField] private TextMeshProUGUI _costText;

    private Button _button;
    private Button _upgradeLevel;

    private void Awake()
    {
        if (!TryGetComponent(out _button))
        {
            Debug.LogError($"{gameObject.name}: Button Component not found");
        }
    }

    private void Start()
    {
        Refresh(_upgradeManager.Upgrades[(int)_upgradeType]);
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(Upgrade);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(Upgrade);
    }

    private void Upgrade()
    {
        _upgradeManager.Upgrade(_upgradeType);
        Refresh(_upgradeManager.Upgrades[(int)_upgradeType]);
    }

    private void Refresh(Upgrade upgrade)
    {
        _titleText.text = $"{upgrade.Name} Lv.{upgrade.Level}";
        _valueText.text = $"+{upgrade.UpgradeAmount:F} → +{upgrade.NextUpgradeAmount:F}";
        _costText.text = $"코스트: {upgrade.Cost}";
    }
}