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
        throw new NotImplementedException();
    }

    public bool Contains(T value)
    {
        throw new NotImplementedException();
    }

    public T GetAt(int index)
    {
        throw new NotImplementedException();
    }

    public void InsertAt(int index, T value)
    {
        throw new NotImplementedException();
    }

    public void Clear()
    {
        throw new NotImplementedException();
    }

    public IEnumerator<T> GetEnumerator()
    {
        throw new NotImplementedException();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}