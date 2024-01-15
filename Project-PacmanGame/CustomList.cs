using System.Collections.Generic;

public class CustomList<T>
{
    private List<T> items = new List<T>();

    public void Add(T item)
    {
        items.Add(item);
    }

    public bool Remove(T item)
    {
        return items.Remove(item);
    }

    public int Count
    {
        get { return items.Count; }
    }

    public T this[int index]
    {
        get { return items[index]; }
        set { items[index] = value; }
    }

    public void Clear()
    {
        items.Clear();
    }

    public IEnumerator<T> GetEnumerator()
    {
        return items.GetEnumerator();
    }
}
