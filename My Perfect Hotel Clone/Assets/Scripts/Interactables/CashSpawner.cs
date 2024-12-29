using System;
using System.Collections;
using MPH.Data;
using UnityEngine;

public class CashSpawner : MonoBehaviour
{
    [SerializeField] private CashUnit cashUnitPrefab;
    
    public float travelDuration = 1.5f; 
    public float arcHeight = 2f;
    public float timeBetweenTravels = 1f;
    
    Interactor interactor;
    private Purse purse;
    
    private Coroutine cashSpawnerCoroutine;
    ICashCollector cashCollector;

    private void Awake()
    {
        interactor = GetComponent<Interactor>(); 
        purse = GetComponent<Purse>();
    }

    private void OnEnable()
    {
        if (interactor == null)
            return;

        interactor.OnInteractionStarted += Interactor_OnInteractionStarted;
        interactor.OnInteractionEnded += Interactor_OnInteractionEnded;
    }
    
    private void OnDisable()
    {
        if (interactor == null)
            return;

        interactor.OnInteract -= Interactor_OnInteractionStarted;
        interactor.OnInteractionEnded -= Interactor_OnInteractionEnded;
    }


    private void Interactor_OnInteractionStarted(Interactable interactable)
    {
        Debug.Log("Interactor_OnInteract called");
        if (interactable.TryGetComponent(out ICashCollector cashCollector))
        {
            if (cashSpawnerCoroutine == null)
            {
                cashSpawnerCoroutine = StartCoroutine(SendCashToDestinationOverTime(cashCollector, interactable.transform.position));
            }
        }
    }

    private void Interactor_OnInteractionEnded(Interactable interactable)
    {
        Debug.Log("Interactor_OnInteractionEnded called");
        if (cashSpawnerCoroutine != null)
        {
            StopAllCoroutines();
            Debug.Log("All coroutines stopped");
            cashSpawnerCoroutine = null;
        }
    }
    
    private IEnumerator SendCashToDestinationOverTime(ICashCollector cashCollector, Vector3 destination)
    {
        int turns = (int)Mathf.Ceil((float)cashCollector.RequiredCash() / cashUnitPrefab.CashValue);
        print($"Turns: {turns}, Cash Req: {cashCollector.RequiredCash()}/Cash Val: {cashUnitPrefab.CashValue}");
        while (purse.CurrentAmount > 0 && turns > 0)
        {
            purse.Debit(cashUnitPrefab.CashValue);
            SendCashToDestination(Instantiate(cashUnitPrefab, transform.position + Vector3.up * 2f, Quaternion.identity), destination);
            turns--;
            
            yield return new WaitForSeconds(timeBetweenTravels);
        }
        Debug.Log("Coroutine ended");
        cashSpawnerCoroutine = null;
    }

    public void SendCashToDestination(CashUnit cashUnit, Vector3 destination)
    {
        if (cashUnit != null)
        {
            cashUnit.Initialize(destination, travelDuration, arcHeight);
        }
    }
}
