using System;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [field: SerializeField] public float MaxInteractionTime { get; private set; } = 2f;
        
    public float InteractionTime { get; private set; } = 0f;
    
    public event Action OnInteract, OnInteractionEnded;

    public void Interact(float interactionSpeed)
    {
        InteractionTime += interactionSpeed * Time.deltaTime;
        
        if (Mathf.Approximately(InteractionTime, MaxInteractionTime))
        {
            OnInteractionEnded?.Invoke();
            return;
        }
        
        OnInteract?.Invoke();
    }
}
