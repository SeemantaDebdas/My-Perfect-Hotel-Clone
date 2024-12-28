using System;
using System.Collections;
using MPH.Data;
using UnityEngine;
using UnityEngine.Events;

public class UpgradeStation : Interactable, ICashCollector
{
    [field: SerializeField] public int MaxUpgradeAmount = 100;
    [SerializeField] private int extractRate = 10;
    [field: SerializeField] public UnityEvent OnUpgrade;
    public event Action OnAmountChanged;
    public int CurrentUpgradeAmount { get; private set; } = 0;


    private void Start()
    {
        CurrentUpgradeAmount = MaxUpgradeAmount;
        OnAmountChanged?.Invoke();
    }

    public void CollectCash(int amount)
    {
        CurrentUpgradeAmount -= amount;

        if (CurrentUpgradeAmount <= 0)
        {
            OnUpgrade?.Invoke();
            gameObject.SetActive(false);
            return;
        }
        
        OnAmountChanged?.Invoke();
    }
}
