using System;
using System.Collections;
using System.Collections.Generic;

namespace data_structures;

public class SinglyLinkedList<T> : IEnumerable<T>
{
    public int Count { get; private set; }
    public bool IsEmpty => Count == 0;

    private Node? head;
    private Node? tail;

    private class Node
    {
        public T Value;
        public Node? Next;

        public Node(T value)
        {
            Value = value;
        }
    }

    public void AddFirst(T value)
    {
        Node newNode = new Node(value);

        if (head == null)
        {
            head = newNode;
            tail = newNode;
        }
        else
        {
            newNode.Next = head;
            head = newNode;
        }

        Count++;
    }

    public void AddLast(T value)
    {
        Node newNode = new Node(value);
        
        if(tail == null || head == null)
        {
            head = newNode;
            tail = newNode;
        }
        else
        {
            tail.Next = newNode;
            tail = newNode;
        } 

        Count++;
    }

    public T RemoveFirst()
    {
        if (head == null)
        {
            throw new InvalidOperationException();
        }

        var val = head.Value;

        if (head == tail)
        {
            head = null;
            tail = null;
        }
        else
        {
            head = head.Next;
        }

        Count--;

        return val;
    }

    public T RemoveLast()
    {
        if (tail == null || head == null)
        {
            throw new InvalidOperationException();
        }

        var val = tail.Value;
        
        if (head == tail)
        {
            head = null;
            tail = null;
        }
        else
        {
            Node iter = head;
            while (iter.Next != null && iter.Next != tail)
            {
                iter = iter.Next;
            }

            iter.Next = null;
            tail = iter;
        }

        Count--;

        return val;
    }

    public bool Remove(T value)
    {
        if (head == null)
        {
            return false;
        }
        
        if (EqualityComparer<T>.Default.Equals(head.Value, value))
        {
            RemoveFirst();
            return true;
        }

        if (EqualityComparer<T>.Default.Equals(tail.Value, value))
        {
            RemoveLast();
            return true;
        }

        var iter = head;
        while (iter != null && iter.Next != null)
        {
            if (EqualityComparer<T>.Default.Equals(iter.Next.Value, value))
            {
                iter.Next = iter.Next.Next;
                return true;
            }

            iter = iter.Next;
        }

        return false;
    }

    public bool Contains(T value)
    {
        if (head == null)
        {
            return false;
        } 
        
        Node? iter = head;
        while (iter != null)
        {
            if (EqualityComparer<T>.Default.Equals(iter.Value, value))
            {
                return true;
            }

            iter = iter.Next;
        }
        

        return false;
    }

    public T GetAt(int index)
    {
        if (index > (Count - 1) || index < 0)
        {
            throw new ArgumentOutOfRangeException();
        }
        
        var iter = head;
        for (var i = 0; i < index; i++)
        {
            iter = iter?.Next;
        }
        
        return iter!.Value;
    }

    public void InsertAt(int index, T value)
    {
        if (index == 0)
        {
            AddFirst(value);
            return;
        }
        
        if (index > (Count) || index < 0)
        {
            throw new ArgumentOutOfRangeException();
        }

        
        var iter = head;
        for (var i = 0; i < index - 1; i++)
        {
            iter = iter?.Next;
        }

        Node newNode = new Node(value);
        newNode.Next = iter.Next;
        iter.Next = newNode;
    }

    public void Clear()
    {
        head = null;
        tail = null;
        Count = 0;
    }

    public IEnumerator<T> GetEnumerator()
    {
        var current = head;
        while (current != null)
        {
            yield return current.Value;
            current = current.Next;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}