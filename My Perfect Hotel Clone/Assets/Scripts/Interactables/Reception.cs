using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Reception : MonoBehaviour
{
    [SerializeField] private ScriptableQueue_Guest guestQueue;
    [SerializeField] private ScriptableList_Room availableRoomList;
    [SerializeField] private float spaceInQueue = 0.5f;
    [SerializeField] private CashStacker cashStacker;

    Interactable interactable;
    private Guest lastGuest;

    private void Awake()
    {
        interactable = GetComponent<Interactable>();
    }

    private void OnEnable()
    {
        interactable.OnInteractionEnded += Interactable_OnInteractionEnded;
        availableRoomList.OnItemAdded += RoomList_OnItemAdded;

        guestQueue.OnItemAdded += GuestQueue_OnItemAdded;
        guestQueue.OnItemRemoved += GuestQueue_OnItemRemoved;
    }

    private void OnDisable()
    {
        interactable.OnInteractionEnded -= Interactable_OnInteractionEnded;
        availableRoomList.OnItemAdded -= RoomList_OnItemAdded;

        guestQueue.OnItemAdded -= GuestQueue_OnItemAdded;
        guestQueue.OnItemRemoved -= GuestQueue_OnItemRemoved;
    }

    private void RoomList_OnItemAdded(Room room)
    {
        EvaluateIfReceptionIsInteractable();
    }
    
    private void EvaluateIfReceptionIsInteractable()
    {
        if (availableRoomList.Count == 0)
        {
            interactable.DisableInteraction();
            //Debug.LogError("No rooms found");
            return;
        }

        if (GetAvailableRoom() == null || guestQueue.Count == 0)
        {
            interactable.DisableInteraction();
            return;
        }

        interactable.EnableInteraction();
    }

    public void AssignRoomToGuest()
    {
        Guest guest = guestQueue.GetAndRemoveFirstElement();
        Room availableRoom = GetAvailableRoom();
        guest.AssignRoom(availableRoom);
        availableRoom.SetAsOccupied();
        
        cashStacker.AddCashToStack();
        
        EvaluateIfReceptionIsInteractable();
    }

    /// <summary>
    /// 1. setup room observable list.
    /// 2. every time room is added or removed from clean list, update canInteract in Interactable
    /// </summary>
    /// <returns></returns>
    private Room GetAvailableRoom()
    {
        print(availableRoomList.Count);
        
        foreach (Room room in availableRoomList.Value)
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
    
    void GuestQueue_OnItemRemoved()
    {
        List<Guest> remainingGuests = guestQueue.Value.ToList();
        for (int i = 0; i < remainingGuests.Count; i++)
        {
            Vector3 startPosition = transform.position + transform.forward * spaceInQueue;
            remainingGuests[i].AssignQueuePosition(startPosition + i * transform.forward * spaceInQueue);
        }
        
        EvaluateIfReceptionIsInteractable();
    }
}
