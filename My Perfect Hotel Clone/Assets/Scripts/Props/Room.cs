using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] List<Prop> props;
    Guest guest = null;

    public bool IsEmpty() => guest != null;
    
    public void AssignGuest(Guest guest)
    {
        this.guest = guest;
    }

    public bool IsClean()
    {
        foreach (Prop prop in props)
        {
            if (!prop.IsClean())
                return false;
        }

        return true;
    }
}
