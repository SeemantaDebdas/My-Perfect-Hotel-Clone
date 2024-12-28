using System;
using System.Collections.Generic;
using RPG.Core;
using UnityEngine;
using UnityEngine.Serialization;

public class Room : MonoBehaviour, IListValueSetter<Room>
{
    [SerializeField] private bool isLocked = true;
    
    [Space]
    [SerializeField] ScriptableList_Room availableRoomList;
    [SerializeField] List<Prop> propList;
    [SerializeField] private Transform restTransform = null, exitTransform = null;
    Guest guest = null;
    
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


    public bool IsEmpty() => guest == null;
    
    public void AssignGuest(Guest guest)
    {
        RemoveItem(this);
        this.guest = guest;
    }

    public void UnassignGuest()
    {
        guest = null;
        foreach (Prop prop in propList)
        {
            prop.Dirty();
        }
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
                print(prop.name);
                return false;
            }
        }

        return true;
    }
    
    void Prop_OnClean()
    {
        if(IsClean())
            AddItem(this);
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
