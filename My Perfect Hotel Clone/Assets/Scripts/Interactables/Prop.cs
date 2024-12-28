using System;
using UnityEngine;
using UnityEngine.Events;

public class Prop : MonoBehaviour
{
    [SerializeField] private GameObject dirtyProp, cleanProp;
    [SerializeField] private UnityEvent onClean, onDirty;

    private Interactable interactable = null;

    private void Awake()
    {
        interactable = GetComponent<Interactable>();
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
        
        onDirty.Invoke();
    }

    public void Clean()
    {
        dirtyProp.SetActive(false);
        cleanProp.SetActive(true);
        
        onClean.Invoke();
    }
}
