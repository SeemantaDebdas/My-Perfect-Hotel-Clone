namespace RPG.Core
{
    public interface IEnumerableValueSetter<T> : IValueSetter<T>
    {
        void AddItem(T item);
        abstract void RemoveItem(T item);
        void ClearEnumerable();
    }
}