using System;
using RPG.Core;
using UnityEngine;

public class Guest : MonoBehaviour, IQueueValueSetter<Guest>
{
    [SerializeField] private ScriptableQueue<Guest> guestQueue;
    [field: SerializeField] public Transform ExitTransform { get; private set; }
    public Room Room { get; private set; }
    public event Action<Room> OnRoomAssigned;
    public event Action<Vector3> OnQueuePositionSet;

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
        Room.SetAsEmpty();
        Room = null;
        AddItem(this);
    }

    public void AssignQueuePosition(Vector3 position)
    {
        Debug.Log("Queue Position set");
        queuePosition = position;
        OnQueuePositionSet?.Invoke(queuePosition);
    }
    
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
