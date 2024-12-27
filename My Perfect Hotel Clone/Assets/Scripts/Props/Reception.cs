using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using UnityEngine;

public class Reception : MonoBehaviour
{
    private Guest lastGuest;
    private Queue<Guest> guestQueue;
    
    Interactable interactable;

    private ObservableCollection<Room> roomList = new();

    private void Awake()
    {
        interactable = GetComponent<Interactable>();
    }

    private void OnEnable()
    {
        interactable.OnInteract += Interactable_OnInteract;

        roomList.CollectionChanged += RoomList_CollectionChanged;
    }

    private void OnDisable()
    {
        interactable.OnInteract -= Interactable_OnInteract;
        roomList.CollectionChanged -= RoomList_CollectionChanged;
    }

    private void RoomList_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
        if (roomList.Count == 0)
        {
            Debug.LogError("No rooms found");
            return;
        }

        if (GetAvailableRoom() == null)
        {
            interactable.DisableInteraction();
            return;
        }
        
        interactable.EnableInteraction();
        return;
    }

    public void Register(Guest guest)
    {
        guestQueue.Enqueue(guest);
    }
    
    public void AssignRoomToGuest()
    {
        Guest guest = guestQueue.Dequeue();
        Room availableRoom = GetAvailableRoom();
        guest.AssignRoom(availableRoom);
    }

    /// <summary>
    /// 1. setup room observable list.
    /// 2. every time room is added or removed from clean list, update canInteract in Interactable
    /// </summary>
    /// <returns></returns>
    private Room GetAvailableRoom()
    {
        foreach (Room room in roomList)
        {
            if (room.IsEmpty() && room.IsClean())
                return room;
        }
        
        return null;
    }

    public Vector3 GetQueuePosition(Guest guest)
    {
        if (lastGuest == null)
        {
            return transform.forward * 2f;
        }
        
        Vector3 queuePosition = lastGuest.transform.position + transform.forward * 2f;
        lastGuest = guest;

        return queuePosition;
    }
    
    void Interactable_OnInteract()
    {
        
    }
}
