using System;
using UnityEngine;
using System.Collections.Generic;
using MPH.Data;
using UnityEngine.Serialization;

public class CashStacker : MonoBehaviour
{
    [Header("Cash Stack Settings")]
    public GameObject cashPrefab;       
    public Vector3 stackStartPositionOffset;  
    public Vector3 unitSize = new Vector3(1f, 0.2f, 1f);

    [Header("Stack Dimensions")]
    public int stackWidth = 5;  
    public int stackHeight = 10;
    public int stackDepth = 5; 

    private int currentX = 0;
    private int currentY = 0;
    private int currentZ = 0;

    private List<Transform> cashUnits = new List<Transform>();

    private Interactable interactable = null;
    CashSpawner cashSpawner = null;

    private void Awake()
    {
        interactable = GetComponent<Interactable>();
        cashSpawner = GetComponent<CashSpawner>();
        
        interactable.DisableInteraction();
    }

    private void OnEnable()
    {
        interactable.OnInteract += Interactable_OnInteract;
    }

    private void OnDisable()
    {
        interactable.OnInteract -= Interactable_OnInteract;
    }

    private void Interactable_OnInteract(Interactor interactor)
    {
        SendCashToDestination(interactor.transform.position + Vector3.up * 1f);
    }

    private void SendCashToDestination(Vector3 destination)
    {
        if (cashUnits.Count == 0)
        {
            Debug.LogWarning("No cash units in the stack to send.");
            return;
        }
        
        Transform lastCashUnit = cashUnits[cashUnits.Count - 1];
        CashUnit cashUnit = lastCashUnit.GetComponent<CashUnit>();

        if (cashUnit != null)
        {
            cashSpawner.SendCashToDestination(cashUnit, destination);
            cashUnits.Remove(lastCashUnit);
            UpdateStackIndices();
        }
    }

    public void AddCashToStack()
    {
        if (currentY >= stackHeight)
        {
            Debug.LogWarning("Cash stack is full! Cannot add more cash.");
            return;
        }

        Vector3 newPosition = transform.position + stackStartPositionOffset +
                              new Vector3(currentX * unitSize.x, currentY * unitSize.y, currentZ * unitSize.z);

        GameObject cash = Instantiate(cashPrefab, newPosition, Quaternion.identity, transform);
        cashUnits.Add(cash.transform);

        AdvanceStackIndices();
        interactable.EnableInteraction();
    }
    
    private void UpdateStackIndices()
    {
        if (cashUnits.Count == 0)
        {
            currentX = 0;
            currentY = 0;
            currentZ = 0;
            
            interactable.DisableInteraction();
            return;
        }

        int lastIndex = cashUnits.Count - 1;
        currentX = lastIndex % stackWidth;
        currentZ = (lastIndex / stackWidth) % stackDepth;
        currentY = lastIndex / (stackWidth * stackDepth);
    }

    private void AdvanceStackIndices()
    {
        currentX++;
        if (currentX >= stackWidth)
        {
            currentX = 0;
            currentZ++;
            if (currentZ >= stackDepth)
            {
                currentZ = 0;
                currentY++;
            }
        }
    }
}
