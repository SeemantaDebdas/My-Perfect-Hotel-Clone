using System;
using TMPro;
using UnityEngine;

public class UpgradeStationUI : MonoBehaviour
{
    [SerializeField] private UpgradeStation upgradeStation = null;
    [SerializeField] private TextMeshProUGUI cashText = null;

    private void OnEnable()
    {
        upgradeStation.OnAmountChanged += UpgradeStation_OnAmountChanged;
        upgradeStation.OnUpgrade.AddListener(() => gameObject.SetActive(false));
    }

    private void OnDisable()
    {
        upgradeStation.OnAmountChanged -= UpgradeStation_OnAmountChanged;
    }

    void UpgradeStation_OnAmountChanged()
    {
        cashText.text = upgradeStation.CurrentUpgradeAmount.ToString();
    }
}
