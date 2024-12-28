using System;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    [SerializeField] private float interactionSpeed = 1f;
    
    private void OnTriggerStay(Collider other)
    {
        if (!other.TryGetComponent(out Interactable interactable)) 
            return;
        
        interactable.Interact(this, interactionSpeed);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.TryGetComponent(out Interactable interactable)) 
            return;
        
        interactable.StopInteraction();
    }
}
