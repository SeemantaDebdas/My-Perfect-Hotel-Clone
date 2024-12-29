using System;
using MPH.Saving;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace MPH.Data
{
    public class Purse : MonoBehaviour, ICashCollector, ISaveable
    {
        [SerializeField] private int initialAmount = 10;
        public int CurrentAmount { get; private set; }

        public event Action OnAmountUpdated;

        private void Awake()
        {
            CurrentAmount = initialAmount;
            OnAmountUpdated?.Invoke();
        }

        public void Debit(int amount)
        {
            CurrentAmount = Mathf.Max(CurrentAmount - amount, 0);
            OnAmountUpdated?.Invoke();
        }

        public void Credit(int amount)
        {
            CurrentAmount += amount;
            OnAmountUpdated?.Invoke();
        }

        #region ICashCollector 

        public void CollectCash(int amount)
        {
            Credit(amount);
        }

        public int RequiredCash(){return CurrentAmount;}
        

        #endregion
        public JToken CaptureAsJToken()
        {
            print("Saving Purse");
            return JToken.FromObject(CurrentAmount);
        }

        public void RestoreFromJToken(JToken state)
        {
            print("Restoring Purse");
            
            CurrentAmount = state.ToObject<int>();
            OnAmountUpdated?.Invoke();
        }
    }
}