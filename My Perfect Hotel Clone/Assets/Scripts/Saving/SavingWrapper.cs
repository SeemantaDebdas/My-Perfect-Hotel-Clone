using System;
using System.Collections;
using UnityEngine;

namespace MPH.Saving
{
    public class SavingWrapper : MonoBehaviour
    {
        const string defaultSaveFile = "save";
        SavingSystem savingSystem = null;

        [SerializeField] float fadeInTime = 0.2f;

        private void Awake()
        {
            savingSystem = GetComponent<SavingSystem>();    
        }

        private void Start()
        {
            //yield return savingSystem.LoadLastScene(defaultSaveFile);
            Load();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                print("Saving");
                Save();
            }
        }

        public void Load()
        {
            savingSystem.Load(defaultSaveFile);
        }

        public void Save()
        {
            savingSystem.Save(defaultSaveFile);
        }
    }
}
