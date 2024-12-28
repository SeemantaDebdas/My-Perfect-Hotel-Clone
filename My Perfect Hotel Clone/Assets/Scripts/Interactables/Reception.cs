using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using UnityEngine;

public class Reception : MonoBehaviour
{
    private Guest lastGuest;
    [SerializeField] private ScriptableQueue_Guest guestQueue;

    Interactable interactable;

    [SerializeField] private float spaceInQueue = 0.5f;
    public ObservableCollection<Room> RoomList { get; private set; } = new();

    private void Awake()
    {
        interactable = GetComponent<Interactable>();
    }

    private void OnEnable()
    {
        interactable.OnInteractionEnded += Interactable_OnInteractionEnded;
        RoomList.CollectionChanged += RoomList_CollectionChanged;

        guestQueue.OnItemAdded += GuestQueue_OnItemAdded;
    }

    private void OnDisable()
    {
        interactable.OnInteractionEnded -= Interactable_OnInteractionEnded;
        RoomList.CollectionChanged -= RoomList_CollectionChanged;

        guestQueue.OnItemAdded -= GuestQueue_OnItemAdded;
    }

    private void RoomList_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
        if (RoomList.Count == 0)
        {
            Debug.LogError("No rooms found");
            return;
        }

        if (GetAvailableRoom() == null || guestQueue.Count == 0)
        {
            interactable.DisableInteraction();
            return;
        }

        interactable.EnableInteraction();
        return;
    }

    public void AssignRoomToGuest()
    {
        Guest guest = guestQueue.FirstElement;
        Room availableRoom = GetAvailableRoom();
        guest.AssignRoom(availableRoom);
        availableRoom.AssignGuest(guest);
    }

    /// <summary>
    /// 1. setup room observable list.
    /// 2. every time room is added or removed from clean list, update canInteract in Interactable
    /// </summary>
    /// <returns></returns>
    private Room GetAvailableRoom()
    {
        print(RoomList.Count);
        
        foreach (Room room in RoomList)
        {
            if (room.IsEmpty() && room.IsClean())
                return room;
        }
        
        return null;
    }
    
    void Interactable_OnInteractionEnded()
    {
        AssignRoomToGuest();
    }
    
    private void GuestQueue_OnItemAdded(Guest guest)
    {
        Debug.Log("On Item Added");
        
        //Assign position to guest
        Vector3 startPosition = transform.position + transform.forward * spaceInQueue;
        int guestCount = guestQueue.Count;
        guest.AssignQueuePosition(startPosition + transform.forward * spaceInQueue * (guestCount - 1));
    }
}
