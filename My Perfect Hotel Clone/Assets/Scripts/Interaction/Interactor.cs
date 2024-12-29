using System;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    [SerializeField] private float interactionSpeed = 1f;
    
    Interactable currentInteractable;
    
    public event Action<Interactable> OnInteract, OnInteractionStarted, OnInteractionEnded;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent(out Interactable interactable)) 
            return;

        OnInteractionStarted?.Invoke(interactable);
        interactable.Interact(this, interactionSpeed);
    }

    private void OnTriggerStay(Collider other)
    {
        if (!other.TryGetComponent(out Interactable interactable)) 
            return;
        
        //print("Interacting with: " + interactable.name);

        interactable.OnDisabled -= Interactable_OnDisabled;
        interactable.OnDisabled += Interactable_OnDisabled;
        
        OnInteract?.Invoke(interactable);
        interactable.Interact(this, interactionSpeed);
    }

    private void Interactable_OnDisabled(Interactable interactable)
    {
        print("Disabled");
        interactable.StopInteraction();
        OnInteractionEnded?.Invoke(interactable);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.TryGetComponent(out Interactable interactable)) 
            return;
        
        OnInteractionEnded?.Invoke(interactable);
        interactable.StopInteraction();
    }
}
