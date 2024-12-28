using System;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    [SerializeField] private float interactionSpeed = 1f;
    
    public event Action<Interactable> OnInteract;
    public event Action<Interactable> OnInteractionEnded;
    
    private void OnTriggerStay(Collider other)
    {
        if (!other.TryGetComponent(out Interactable interactable)) 
            return;
        
        OnInteract?.Invoke(interactable);
        interactable.Interact(this, interactionSpeed);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.TryGetComponent(out Interactable interactable)) 
            return;
        
        OnInteractionEnded?.Invoke(interactable);
        interactable.StopInteraction();
    }
}
