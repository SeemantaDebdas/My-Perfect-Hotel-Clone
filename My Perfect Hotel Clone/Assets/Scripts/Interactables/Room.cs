using System;
using System.Collections.Generic;
using RPG.Core;
using UnityEngine;
using UnityEngine.Events;

public class Room : MonoBehaviour, IListValueSetter<Room>
{
    [SerializeField] private bool isLocked = true;
    
    [Space]
    [SerializeField] ScriptableList_Room availableRoomList;
    [SerializeField] List<Prop> propList;
    [SerializeField] private Transform restTransform = null, exitTransform = null;

    [Space] 
    [SerializeField] private UnityEvent onClean = null;
    [SerializeField] private UnityEvent onDirty = null;
    [SerializeField] private UnityEvent onEmpty = null;

    private bool isEmpty = true;
    
    
    private void OnEnable()
    {
        AddItem(this);

        foreach (Prop prop in propList)
        {
            prop.OnClean.AddListener(Prop_OnClean);
        }
    }
    
    private void OnDisable()
    {
        RemoveItem(this);
        
        foreach (Prop prop in propList)
        {
            prop.OnClean.RemoveListener(Prop_OnClean);
        }
    }


    public bool IsEmpty() => isEmpty;
    
    public void SetAsOccupied()
    {
        RemoveItem(this);
        isEmpty = false;
    }

    public void SetAsEmpty()
    {
        isEmpty = true;
        onEmpty.Invoke();
        
        foreach (Prop prop in propList)
        {
            prop.Dirty();
        }
        onDirty?.Invoke();
    }

    public Transform GetRestTransform()
    {
        return restTransform;
    }

    public Transform GetExitTransform()
    {
        return exitTransform;
    }

    public bool IsClean()
    {
        foreach (Prop prop in propList)
        {
            if (!prop.IsClean())
            {
                //print(prop.name);
                return false;
            }
        }

        return true;
    }
    
    void Prop_OnClean()
    {
        if (IsClean())
        {
            AddItem(this);
            onClean?.Invoke();
        }
    }

    #region IEnumberableValueSetter

    public void SetValue(Room value)
    {
        
    }

    public void AddItem(Room item)
    {
        availableRoomList.AddItem(item, this);        
    }

    public void RemoveItem(Room item)
    {
        availableRoomList.RemoveItem(item, this);
    }

    public void ClearEnumerable()
    {
        
    }
    

    #endregion
}
