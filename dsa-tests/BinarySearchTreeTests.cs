using System;
using System.Linq;
using NUnit.Framework;
using data_structures;

namespace dsa_tests;

[TestFixture]
public class BinarySearchTreeTests
{
    [Test]
    public void NewTree_IsEmpty()
    {
        var bst = new BinarySearchTree<int>();
        Assert.That(bst.IsEmpty, Is.True);
        Assert.That(bst.Count, Is.EqualTo(0));
    }

    [Test]
    public void Insert_SingleValue()
    {
        var bst = new BinarySearchTree<int>();
        bst.Insert(10);

        Assert.That(bst.IsEmpty, Is.False);
        Assert.That(bst.Count, Is.EqualTo(1));
        Assert.That(bst.Contains(10), Is.True);
    }

    [Test]
    public void Insert_MultipleValues()
    {
        var bst = new BinarySearchTree<int>();

        bst.Insert(10);
        bst.Insert(5);
        bst.Insert(15);
        bst.Insert(3);
        bst.Insert(7);

        Assert.That(bst.Count, Is.EqualTo(5));
        Assert.That(bst.Contains(3), Is.True);
        Assert.That(bst.Contains(7), Is.True);
        Assert.That(bst.Contains(15), Is.True);
    }

    [Test]
    public void Insert_Duplicate_Ignored()
    {
        var bst = new BinarySearchTree<int>();

        bst.Insert(10);
        bst.Insert(10);
        bst.Insert(10);

        Assert.That(bst.Count, Is.EqualTo(1));
    }

    [Test]
    public void Contains_ValueNotPresent()
    {
        var bst = new BinarySearchTree<int>();

        bst.Insert(10);
        bst.Insert(5);

        Assert.That(bst.Contains(99), Is.False);
    }

    [Test]
    public void InOrderTraversal_ReturnsSortedSequence()
    {
        var bst = new BinarySearchTree<int>();

        bst.Insert(10);
        bst.Insert(5);
        bst.Insert(15);
        bst.Insert(3);
        bst.Insert(7);

        var result = bst.InOrderTraversal().ToArray();

        Assert.That(result, Is.EqualTo(new[] { 3, 5, 7, 10, 15 }));
    }

    [Test]
    public void PreOrderTraversal_ReflectsStructure()
    {
        var bst = new BinarySearchTree<int>();

        bst.Insert(10);
        bst.Insert(5);
        bst.Insert(15);
        bst.Insert(3);
        bst.Insert(7);

        var result = bst.PreOrderTraversal().ToArray();

        Assert.That(result, Is.EqualTo(new[] { 10, 5, 3, 7, 15 }));
    }

    [Test]
    public void PostOrderTraversal_ReflectsStructure()
    {
        var bst = new BinarySearchTree<int>();

        bst.Insert(10);
        bst.Insert(5);
        bst.Insert(15);
        bst.Insert(3);
        bst.Insert(7);

        var result = bst.PostOrderTraversal().ToArray();

        Assert.That(result, Is.EqualTo(new[] { 3, 7, 5, 15, 10 }));
    }

    [Test]
    public void Remove_LeafNode()
    {
        var bst = new BinarySearchTree<int>();

        bst.Insert(10);
        bst.Insert(5);
        bst.Insert(15);

        Assert.That(bst.Remove(5), Is.True);
        Assert.That(bst.Contains(5), Is.False);
        Assert.That(bst.Count, Is.EqualTo(2));
    }

    [Test]
    public void Remove_NodeWithOneChild()
    {
        var bst = new BinarySearchTree<int>();

        bst.Insert(10);
        bst.Insert(5);
        bst.Insert(3);

        Assert.That(bst.Remove(5), Is.True);
        Assert.That(bst.Contains(5), Is.False);
        Assert.That(bst.Contains(3), Is.True);
        Assert.That(bst.Count, Is.EqualTo(2));
    }

    [Test]
    public void Remove_NodeWithTwoChildren()
    {
        var bst = new BinarySearchTree<int>();

        bst.Insert(10);
        bst.Insert(5);
        bst.Insert(15);
        bst.Insert(12);
        bst.Insert(18);

        Assert.That(bst.Remove(15), Is.True);
        Assert.That(bst.Contains(15), Is.False);
        Assert.That(bst.Contains(12), Is.True);
        Assert.That(bst.Contains(18), Is.True);
        Assert.That(bst.Count, Is.EqualTo(4));
    }

    [Test]
    public void Remove_RootNode()
    {
        var bst = new BinarySearchTree<int>();

        bst.Insert(10);
        bst.Insert(5);
        bst.Insert(15);

        Assert.That(bst.Remove(10), Is.True);
        Assert.That(bst.Contains(10), Is.False);
        Assert.That(bst.Count, Is.EqualTo(2));
    }

    [Test]
    public void Remove_ValueNotPresent_ReturnsFalse()
    {
        var bst = new BinarySearchTree<int>();

        bst.Insert(10);
        bst.Insert(5);

        Assert.That(bst.Remove(99), Is.False);
        Assert.That(bst.Count, Is.EqualTo(2));
    }

    [Test]
    public void Height_EmptyTree_IsMinusOne()
    {
        var bst = new BinarySearchTree<int>();
        Assert.That(bst.Height(), Is.EqualTo(-1));
    }

    [Test]
    public void Height_SingleNode_IsZero()
    {
        var bst = new BinarySearchTree<int>();
        bst.Insert(10);

        Assert.That(bst.Height(), Is.EqualTo(0));
    }

    [Test]
    public void Height_UnbalancedTree()
    {
        var bst = new BinarySearchTree<int>();

        bst.Insert(10);
        bst.Insert(9);
        bst.Insert(8);
        bst.Insert(7);

        Assert.That(bst.Height(), Is.EqualTo(3));
    }

    [Test]
    public void Enumerator_UsesInOrderTraversal()
    {
        var bst = new BinarySearchTree<int>();

        bst.Insert(2);
        bst.Insert(1);
        bst.Insert(3);

        var result = bst.ToArray();

        Assert.That(result, Is.EqualTo(new[] { 1, 2, 3 }));
    }

    [Test]
    public void Traversal_StableAfterRemovals()
    {
        var bst = new BinarySearchTree<int>();

        bst.Insert(10);
        bst.Insert(5);
        bst.Insert(15);
        bst.Insert(3);
        bst.Insert(7);

        bst.Remove(5);

        var result = bst.InOrderTraversal().ToArray();

        Assert.That(result, Is.EqualTo(new[] { 3, 7, 10, 15 }));
    }
}
