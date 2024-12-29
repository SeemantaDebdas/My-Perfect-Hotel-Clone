using System;
using MPH.Data;
using TMPro;
using UnityEngine;

public class PurseUI : MonoBehaviour
{
    [SerializeField] Purse purse;
    [SerializeField] private TextMeshProUGUI cashText;

    private void OnEnable()
    {
        purse.OnAmountUpdated += Purse_OnAmountUpdated;
    }
    
    private void OnDisable()
    {
        purse.OnAmountUpdated -= Purse_OnAmountUpdated;
    }

    private void Purse_OnAmountUpdated()
    {
        cashText.text = purse.CurrentAmount.ToString();
    }
}
