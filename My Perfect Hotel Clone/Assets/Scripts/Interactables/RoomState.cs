using System;
using System.Collections.Generic;
using UnityEngine;

public class RoomState : MonoBehaviour
{
    [SerializeField] private bool lockedAtStart = true;
    [SerializeField] private GameObject lockedState = null;

    [SerializeField] private List<GameObject> roomLevelList = null;
    private int currentStateIndex = 0;

    private void Awake()
    {
        foreach (GameObject roomLevel in roomLevelList)
        {
            roomLevel.SetActive(false);
        }
        
        if (lockedAtStart)
        {
            lockedState.SetActive(true);
        }

        else
        {
            lockedState.SetActive(false);
            roomLevelList[currentStateIndex].SetActive(true);
        }
    }
    
    
}
