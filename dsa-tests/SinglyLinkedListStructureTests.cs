using NUnit.Framework;
using data_structures;
using System;
using NUnit.Framework;
using data_structures;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace dsa_tests;

[TestFixture]
public class SinglyLinkedListStructureTests
{
    [Test]
    public void AddFirst_OnEmptyList_CreatesSingleNode()
    {
        var list = new SinglyLinkedList<int>();

        list.AddFirst(42);

        Assert.That(list.IsEmpty, Is.False);
        Assert.That(list.Count, Is.EqualTo(1));
        Assert.That(list.RemoveFirst(), Is.EqualTo(42));
        Assert.That(list.IsEmpty, Is.True);
    }

    [Test]
    public void AddLast_OnEmptyList_CreatesSingleNode()
    {
        var list = new SinglyLinkedList<int>();

        list.AddLast(42);

        Assert.That(list.IsEmpty, Is.False);
        Assert.That(list.Count, Is.EqualTo(1));
        Assert.That(list.RemoveLast(), Is.EqualTo(42));
        Assert.That(list.IsEmpty, Is.True);
    }

    [Test]
    public void AddFirst_Twice_PreservesCorrectOrderViaRemoval()
    {
        var list = new SinglyLinkedList<int>();

        list.AddFirst(1);
        list.AddFirst(2);

        Assert.That(list.Count, Is.EqualTo(2));
        Assert.That(list.RemoveFirst(), Is.EqualTo(2));
        Assert.That(list.RemoveFirst(), Is.EqualTo(1));
        Assert.That(list.IsEmpty, Is.True);
    }

    [Test]
    public void AddLast_Twice_PreservesCorrectOrderViaRemoval()
    {
        var list = new SinglyLinkedList<int>();

        list.AddLast(1);
        list.AddLast(2);

        Assert.That(list.Count, Is.EqualTo(2));
        Assert.That(list.RemoveFirst(), Is.EqualTo(1));
        Assert.That(list.RemoveFirst(), Is.EqualTo(2));
        Assert.That(list.IsEmpty, Is.True);
    }

    [Test]
    public void Mixed_Adds_DoNotCorruptStructure()
    {
        var list = new SinglyLinkedList<int>();

        list.AddFirst(2);
        list.AddLast(3);
        list.AddFirst(1);

        Assert.That(list.Count, Is.EqualTo(3));
        Assert.That(list.RemoveFirst(), Is.EqualTo(1));
        Assert.That(list.RemoveFirst(), Is.EqualTo(2));
        Assert.That(list.RemoveFirst(), Is.EqualTo(3));
        Assert.That(list.IsEmpty, Is.True);
    }

    [Test]
    public void Tail_IsAlwaysTerminal_AfterAdds()
    {
        var list = new SinglyLinkedList<int>();

        list.AddLast(1);
        list.AddLast(2);
        list.AddLast(3);

        Assert.That(list.RemoveLast(), Is.EqualTo(3));
        Assert.That(list.RemoveLast(), Is.EqualTo(2));
        Assert.That(list.RemoveLast(), Is.EqualTo(1));
        Assert.That(list.IsEmpty, Is.True);
    }
}
