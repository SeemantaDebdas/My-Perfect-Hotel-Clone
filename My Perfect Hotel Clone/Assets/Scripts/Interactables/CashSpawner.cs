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

    private void Awake()
    {
        interactor = GetComponent<Interactor>(); 
        purse = GetComponent<Purse>();
    }

    private void OnEnable()
    {
        if (interactor == null)
            return;

        interactor.OnInteract += Interactor_OnInteract;
        interactor.OnInteractionEnded += Interactor_OnInteractionEnded;
    }
    
    private void OnDisable()
    {
        if (interactor == null)
            return;

        interactor.OnInteract -= Interactor_OnInteract;
        interactor.OnInteractionEnded -= Interactor_OnInteractionEnded;
    }


    private void Interactor_OnInteract(Interactable interactable)
    {
        if (interactable.TryGetComponent(out ICashCollector cashCollector))
        {
            print("I cash Collector found: " + interactable.name);
            if (cashSpawnerCoroutine == null)
            {
                cashSpawnerCoroutine = StartCoroutine(SendCashToDestinationOverTime(interactable.transform.position));
            }
        }
    }
    
    private void Interactor_OnInteractionEnded(Interactable interactable)
    {
        if (!interactable.TryGetComponent(out ICashCollector cashCollector))
            return;
        
        if (cashSpawnerCoroutine == null)
            return;
        
        StopCoroutine(cashSpawnerCoroutine);
        cashSpawnerCoroutine = null;
    }
    
    private IEnumerator SendCashToDestinationOverTime(Vector3 destination)
    {
        while (purse.CurrentAmount > 0)
        {
            print("Sending cash");
            purse.Debit(cashUnitPrefab.CashValue);
            SendCashToDestination(Instantiate(cashUnitPrefab, transform.position + Vector3.up * 2f, Quaternion.identity), destination);
            yield return new WaitForSeconds(timeBetweenTravels); 
        }

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
