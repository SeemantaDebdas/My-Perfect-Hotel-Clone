using UnityEngine;

public class Guest : MonoBehaviour
{
    private Room room = null;
    public void AssignRoom(Room room)
    {
        this.room = room;
    }
}
