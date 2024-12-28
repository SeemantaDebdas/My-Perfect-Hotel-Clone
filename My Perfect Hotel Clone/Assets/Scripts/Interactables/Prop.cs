using System;
using UnityEngine;
using UnityEngine.Events;

public class Prop : MonoBehaviour
{
    [SerializeField] private GameObject dirtyProp, cleanProp;
    [field: SerializeField] public UnityEvent OnClean { get; private set; }
    [field: SerializeField] public UnityEvent OnDirty { get; private set; }

    private Interactable interactable = null;

    private void Awake()
    {
        interactable = GetComponent<Interactable>();
    }

    private void Start()
    {
        Clean();
    }

    private void OnEnable()
    {
        interactable.OnInteractionEnded += Interactable_OnInteractionEnded;
    }

    private void OnDisable()
    {
        interactable.OnInteractionEnded -= Interactable_OnInteractionEnded;
    }
    
    public bool IsClean() => cleanProp.activeSelf;

    private void Interactable_OnInteractionEnded()
    {
        Clean();
    }

    public void Dirty()
    {
        dirtyProp.SetActive(true);
        cleanProp.SetActive(false);
        interactable.EnableInteraction();
        
        OnDirty.Invoke();
    }

    void Clean()
    {
        dirtyProp.SetActive(false);
        cleanProp.SetActive(true);
        interactable.DisableInteraction();
        
        OnClean.Invoke();
    }
}
