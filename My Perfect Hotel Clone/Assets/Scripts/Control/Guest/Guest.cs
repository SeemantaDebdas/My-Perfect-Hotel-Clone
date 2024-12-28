using System;
using RPG.Core;
using UnityEngine;
using UnityEngine.Serialization;

public class Guest : MonoBehaviour, IQueueValueSetter<Guest>
{
    [SerializeField] private ScriptableQueue<Guest> guestQueue;
    [field: SerializeField] public Transform ExitTransform { get; private set; }
    public Room Room { get; private set; }
    public event Action<Room> OnRoomAssigned;
    public event Action<Vector3> OnQueuePositionSet;
    public bool HasVisited { get; private set; } = false;

    private Vector3 queuePosition = Vector3.zero;

    private void Start()
    {
        AddItem(this);
    }

    public void AssignRoom(Room room)
    {
        if (room == null)
        {
            Debug.LogError("Room assigned is null!");
            return;
        }
        
        Debug.Log("Room Assigned");
        Room = room;
        OnRoomAssigned?.Invoke(room);
    }

    public void UnassignRoom()
    {
        Room.UnassignGuest();
        Room = null;
        HasVisited = true;
    }

    public void AssignQueuePosition(Vector3 position)
    {
        Debug.Log("Queue Position set");
        queuePosition = position;
        OnQueuePositionSet?.Invoke(queuePosition);
    }
    
    public Vector3 GetQueuePosition() => queuePosition;
    
    #region IQueueValueSetter

    public void SetValue(Guest value)
    {
    }

    public void AddItem(Guest item)
    {
        guestQueue.AddItem(item, this);
    }

    public void RemoveItem(Guest item){}

    public void ClearEnumerable()
    {
        
    }
    
    #endregion
}
