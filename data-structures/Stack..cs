using System;

namespace data_structures;

public class Stack<T>
{
    public int Count => _list.Count();
    public bool IsEmpty => Count == 0;

    private SinglyLinkedList<T> _list;

    public Stack()
    {
        _list = new SinglyLinkedList<T>();
    }

    public void Push(T value)
    {
        _list.AddFirst(value);
    }

    public T Pop()
    {
        if (Count == 0)
        {
            throw new InvalidOperationException();
        }

        return _list.RemoveFirst();
    }

    public T Peek()
    {
        if (Count == 0)
        {
            throw new InvalidOperationException();
        }

        return _list.GetAt(0);
    }

    public void Clear()
    {
        _list.Clear();
    }
}