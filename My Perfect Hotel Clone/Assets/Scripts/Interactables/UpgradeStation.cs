using System;
using System.Collections;
using MPH.Data;
using UnityEngine;
using UnityEngine.Events;

public class UpgradeStation : Interactable
{
    [field: SerializeField] public int MaxUpgradeAmount = 100;
    [SerializeField] private int extractRate = 10;
    [field: SerializeField] public UnityEvent OnUpgrade;

    public event Action OnAmountChanged;

    private Purse purse = null;
    public int CurrentUpgradeAmount { get; private set; } = 0;

    private Coroutine extractionCoroutine;

    private void Start()
    {
        CurrentUpgradeAmount = MaxUpgradeAmount;
        OnAmountChanged?.Invoke();
    }

    public override void Interact(Interactor interactor, float interactionSpeed)
    {
        if (purse == null)
        {
            purse = interactor.GetComponent<Purse>();
        }

        if (purse.CurrentAmount < extractRate || CurrentUpgradeAmount <= 0)
        {
            return;
        }
        
        if (extractionCoroutine == null)
        {
            extractionCoroutine = StartCoroutine(ExtractUpgrade());
        }
    }

    public override void StopInteraction()
    {
        base.StopInteraction();
        
        if(extractionCoroutine != null)
            StopCoroutine(extractionCoroutine);
        
        extractionCoroutine = null;
    }

    private IEnumerator ExtractUpgrade()
    {
        while (CurrentUpgradeAmount > 0 && purse.CurrentAmount >= extractRate)
        {
            purse.Debit(extractRate); 
            CurrentUpgradeAmount -= extractRate;
            OnAmountChanged?.Invoke();

            if (CurrentUpgradeAmount <= 0)
            {
                CurrentUpgradeAmount = 0;
                DisableInteraction();
                OnUpgrade.Invoke();
                break;
            }

            yield return new WaitForSeconds(1f); 
        }
        
        extractionCoroutine = null;
    }
}
