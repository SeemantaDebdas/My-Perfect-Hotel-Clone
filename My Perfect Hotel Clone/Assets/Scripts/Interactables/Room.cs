using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Room : MonoBehaviour
{
    [SerializeField] private Reception reception = null;
    [FormerlySerializedAs("props")] [SerializeField] List<Prop> propList;
    [SerializeField] private Transform restTransform = null, exitTransform = null;
    Guest guest = null;
    
    private void OnDisable()
    {
        reception.RoomList.Remove(this);
    }

    private void Start()
    {
        reception.RoomList.Add(this);
        
        foreach (Prop prop in propList)
        {
            prop.Clean();
        }
    }

    public bool IsEmpty() => guest == null;
    
    public void AssignGuest(Guest guest)
    {
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
}
