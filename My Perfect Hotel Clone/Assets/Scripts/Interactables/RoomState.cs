using System;
using System.Collections.Generic;
using MPH.Saving;
using Newtonsoft.Json.Linq;
using UnityEngine;

public class RoomState : MonoBehaviour, ISaveable
{
    [SerializeField] private bool lockedAtStart = true;
    [SerializeField] private GameObject lockedState = null;

    [SerializeField] private List<GameObject> roomLevelList = null;
    private int currentStateIndex = -1;

    private void Awake()
    {
        DisableAllRooms();
        
        if (lockedAtStart)
        {
            lockedState.SetActive(true);
        }

        else
        {
            lockedState.SetActive(false);
            roomLevelList[++currentStateIndex].SetActive(true);
        }
    }

    void DisableAllRooms()
    {
        lockedState.SetActive(false);
        foreach (GameObject roomLevel in roomLevelList)
        {
            roomLevel.SetActive(false);
        }
    }


    public void UpgradeState()
    {
        DisableAllRooms();

        roomLevelList[++currentStateIndex].SetActive(true);
    }

    public JToken CaptureAsJToken()
    {
        return JToken.FromObject(currentStateIndex);
    }

    public void RestoreFromJToken(JToken state)
    {
        currentStateIndex = state.ToObject<int>();
        
        DisableAllRooms();

        if (currentStateIndex == -1)
        {
            lockedState.SetActive(false);
        }
        else
        {
            roomLevelList[currentStateIndex].SetActive(true);
        }
    }
}
