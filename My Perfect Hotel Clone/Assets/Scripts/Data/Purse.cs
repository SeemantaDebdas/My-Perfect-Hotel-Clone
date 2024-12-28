using System;
using UnityEngine;

namespace MPH.Data
{
    public class Purse : MonoBehaviour
    {
        [SerializeField] private int initialAmount = 10;
        public int CurrentAmount { get; private set; } 

        private void Awake()
        {
            CurrentAmount = initialAmount;
        }

        public void Debit(int amount)
        {
            CurrentAmount = Mathf.Max(CurrentAmount - amount, 0);
        }

        public void Credit(int amount)
        {
            CurrentAmount += amount;
        }
    }
}