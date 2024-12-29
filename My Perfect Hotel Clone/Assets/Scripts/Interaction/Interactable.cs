using System;
using DG.Tweening;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [field: SerializeField] public float MaxInteractionTime { get; private set; } = 2f;
    [SerializeField] bool resetOnEnd = false;
    public float InteractionTime { get; private set; } = 0f;

    public event Action<Interactor> OnInteract;
    public event Action OnInteractionEnded, OnReset, OnInteractionStop;
    public event Action<Interactable> OnDisabled;

    private bool canInteract = true;
    
    public bool CanInteract() => canInteract;

    Interactor interactor = null;

    public void EnableInteraction()
    {
        canInteract = true;
    }

    public void DisableInteraction()
    {
        canInteract = false;
        InteractionTime = 0f;
        OnReset?.Invoke();
    }

    public virtual void  Interact(Interactor interactor, float interactionSpeed)
    {
        if (canInteract == false)
            return;
        
        InteractionTime += interactionSpeed * Time.deltaTime;
        
        if (Mathf.Approximately(InteractionTime, MaxInteractionTime))
        {
            OnInteractionEnded?.Invoke();
            if (resetOnEnd)
            {
                InteractionTime = 0f;
                OnReset?.Invoke();
            }
            return;
        }
        
        OnInteract?.Invoke(interactor);
    }

    public virtual void StopInteraction()
    {
        OnInteractionStop?.Invoke();
    }

    private void OnDisable()
    {
        //print("Disabled Interactable");
        OnDisabled?.Invoke(this);
    }
}
