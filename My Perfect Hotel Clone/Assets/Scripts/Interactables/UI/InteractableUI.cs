using System;
using UnityEngine;
using UnityEngine.UI;

public class InteractableUI : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] Interactable interactable;

    private void OnEnable()
    {
        interactable.OnInteract += Interactable_OnInteract;
        interactable.OnReset += Interactable_OnReset;
    }

    private void OnDisable()
    {
        interactable.OnInteract -= Interactable_OnInteract;
        interactable.OnReset -= Interactable_OnReset;
    }

    void Interactable_OnInteract(Interactor _)
    {
        UpdateUI();
    }
    void Interactable_OnReset()
    {
        UpdateUI();    
    }

    void UpdateUI()
    {
        slider.value = interactable.InteractionTime / interactable.MaxInteractionTime;
    }
}
