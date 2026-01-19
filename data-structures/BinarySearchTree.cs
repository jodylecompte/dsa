using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.AccessControl;

namespace data_structures;

public class BinarySearchTree<T> : IEnumerable<T>
    where T : IComparable<T>
{
    public class Node
    {
        public T Value;
        public Node? Left;
        public Node? Right;

        public Node(T value)
        {
            Value = value;
        }
    }

    public int Count = 0;
    public bool IsEmpty => Count < 1;

    private Node? root;

    public void Insert(T value)
    {
        var node = new Node(value);

        if (root == null)
        {
            root = node;
            Count++;
            return;
        }

        var iter = root;

        while (true)
        {
            var comparison = iter.Value.CompareTo(value);

            if (comparison == 0)
            {
                return;
            }

            if (comparison < 0)
            {
                if (iter.Right == null)
                {
                    iter.Right = node;
                    Count++;
                    return;
                }

                iter = iter.Right;
            }
            else
            {
                if (iter.Left == null)
                {
                    iter.Left = node;
                    Count++;
                    return;
                }

                iter = iter.Left;
            }
        }
    }

    public bool Contains(T value)
    {
        var iter = root;
        while (iter != null)
        {
            if (iter.Value.CompareTo(value) == 0)
            {
                return true;
            }

            if (iter.Value.CompareTo(value) < 0)
            {
                iter = iter.Right;
            }
            else if (iter.Value.CompareTo(value) > 0)
            {
                iter = iter.Left;
            }
            
        }

        return false;
    }

    public bool Remove(T value)
    {
        Node? parent = null;
        Node? current = root;

        while (current != null && current.Value.CompareTo(value) != 0)
        {
            parent = current;
            if (value.CompareTo(current.Value) < 0)
            {
                current = current.Left;
            }
            else
            {
                current = current.Right;
            }
        }

        if (current == null)
        {
            return false;
        }

        if (current.Left != null && current.Right != null)
        {
            var successorParent = current;
            var successor = current.Right;

            while (successor.Left != null)
            {
                successorParent = successor;
                successor = successor.Left;
            }

            current.Value = successor.Value;
            current = successor;
            parent = successorParent;
        }

        Node? child = current.Left ?? current.Right;

        if (parent == null)
        {
            root = child;
        }
        else if (parent.Left == current)
        {
            parent.Left = child;
        }
        else
        {
            parent.Right = child;
        }

        Count--;
        return true;
    }


    public int Height()
    {
        return HeightNode(root);
    }

    private int HeightNode(Node? node)
    {
        if (node == null) return -1;
        if (node.Left == null && node.Right == null) return 0;

        return 1 + Math.Max(HeightNode(node.Left), HeightNode(node.Right));
    }

    private IEnumerable<T> TraverseInOrder(Node? node)
    {
        if (node == null)
        {
            yield break;
        }

        foreach (var value in TraverseInOrder(node.Left))
        {
            yield return value;
        }

        yield return node.Value;

        foreach (var value in TraverseInOrder(node.Right))
        {
            yield return value;
        }
    }
    
    private IEnumerable<T> TraversePreOrder(Node? node)
    {
        if (node == null)
        {
            yield break;
        }
        
        yield return node.Value;

        foreach (var value in TraversePreOrder(node.Left))
        {
            yield return value;
        }

        foreach (var value in TraversePreOrder(node.Right))
        {
            yield return value;
        }
    }
    
    private IEnumerable<T> TraversePostOrder(Node? node)
    {
        if (node == null)
        {
            yield break;
        }

        foreach (var value in TraversePostOrder(node.Left))
        {
            yield return value;
        }

        foreach (var value in TraversePostOrder(node.Right))
        {
            yield return value;
        }
        
        yield return node.Value;
    }
    
    public IEnumerable<T> InOrderTraversal()
    { 
        return TraverseInOrder(root);
    }

    public IEnumerable<T> PreOrderTraversal()
    {
        return TraversePreOrder(root);
    }

    public IEnumerable<T> PostOrderTraversal()
    {
        return TraversePostOrder(root);
    }

    public IEnumerator<T> GetEnumerator()
    {
        return InOrderTraversal().GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}