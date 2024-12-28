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
    }

    private void OnDisable()
    {
        interactable.OnInteract -= Interactable_OnInteract;
    }

    private void Interactable_OnInteract()
    {
        slider.value = interactable.InteractionTime / interactable.MaxInteractionTime;
        //print(slider.value);
    }
}
