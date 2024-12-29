using System;
using System.Collections.Generic;
using UnityEngine;

namespace MPH.Core
{
    public interface IQueueValueSetter<T> : IEnumerableValueSetter<T>
    {
    }

    //probably have a base state for IEnumerables. list, queue, stack...
    public class ScriptableQueue<T> : ScriptableVariable<Queue<T>>
    {
        public event Action<T> OnItemAdded;
        public event Action OnListCleared, OnItemRemoved;
        public int Count => value.Count;

        public T GetAndRemoveFirstElement()
        {
            T returnValue = value.Dequeue();
            OnItemRemoved?.Invoke();
            
            return returnValue;
        }

        private void OnEnable()
        {
            value ??= new Queue<T>();
        }

        public void AddItem(T item, UnityEngine.Object caller)
        {
            if (caller is IQueueValueSetter<T> setter)
            {
                if (value.Contains(item))
                    return;

                value.Enqueue(item);
                OnItemAdded?.Invoke(item);
            }
            else
            {
                Debug.LogWarning($"{caller.name} is not authorized to modify this list!");
            }

            //Debug.Log(value.Count);
        }

        public void RemoveLastItem(UnityEngine.Object caller)
        {
            if (caller is IQueueValueSetter<T> setter)
            {
                value.Dequeue();
                OnItemRemoved?.Invoke();
            }
            else
            {
                Debug.LogWarning($"{caller.name} is not authorized to modify this list!");
            }
        }

        public void ClearQueue(UnityEngine.Object caller)
        {
            if (caller is IQueueValueSetter<T> setter)
            {
                value.Clear();
                OnListCleared?.Invoke();
            }
            else
            {
                Debug.LogWarning($"{caller.name} is not authorized to modify this list!");
            }
        }
        
        /// <summary>
        /// Sorts the queue using a comparator function.
        /// </summary>
        /// <param name="comparator">The function used to compare two elements.</param>
        public void Sort(Comparison<T> comparator)
        {
            // Convert the queue to a list
            List<T> list = new List<T>(value);

            // Sort the list using the provided comparator
            list.Sort(comparator);

            // Recreate the queue from the sorted list
            value = new Queue<T>(list);
        }
    }
}
