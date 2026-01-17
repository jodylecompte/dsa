using System;

namespace data_structures;

public class Queue<T>
{
    private Stack<T> inStack;
    private Stack<T> outStack;
    public int Count => inStack.Count + outStack.Count;
    public bool IsEmpty => Count == 0;

    public Queue()
    {
        inStack = new Stack<T>();
        outStack = new Stack<T>();
    }

    public void Enqueue(T value)
    {
        inStack.Push(value);
    }

    public T Dequeue()
    {
        if (outStack.Count == 0)
        {
            TransferStack();
        }
        
        return outStack.Pop();
    }

    public T Peek()
    {
        if (outStack.Count == 0)
        {
            TransferStack();
        }
        
        return outStack.Peek();
    }

    private void TransferStack()
    {
        while (!inStack.IsEmpty)
        {
            outStack.Push(inStack.Pop());
        }
    }

    public void Clear()
    {
        inStack.Clear();
        outStack.Clear();
    }
}