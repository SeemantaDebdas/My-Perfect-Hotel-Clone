using System;
using UnityEngine;

public class Prop : MonoBehaviour
{
    [SerializeField] private GameObject dirtyProp, cleanProp;

    public event Action OnPropInteracted;

    private Interactable interactable = null;
    private void Awake()
    {
        interactable = GetComponent<Interactable>();
        Dirty();
    }

    private void OnEnable()
    {
        interactable.OnInteract += OnPropInteracted;
        interactable.OnInteractionEnded += Interactable_OnInteractionEnded;
    }

    private void OnDisable()
    {
        interactable.OnInteract -= OnPropInteracted;
        interactable.OnInteractionEnded -= Interactable_OnInteractionEnded;
    }

    public float TimeCleaned => interactable.InteractionTime;
    public float MaxCleanTime => interactable.MaxInteractionTime;
    
    public bool IsClean() => cleanProp.activeSelf;

    private void Interactable_OnInteractionEnded()
    {
        Clean();
    }

    void Dirty()
    {
        dirtyProp.SetActive(true);
        cleanProp.SetActive(false);
    }

    void Clean()
    {
        dirtyProp.SetActive(false);
        cleanProp.SetActive(true);
    }
}
