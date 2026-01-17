using NUnit.Framework;
using data_structures;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace dsa_tests;

[TestFixture]
public class SinglyLinkedListProTests
{
    [Test]
    public void NewList_State()
    {
        var list = new SinglyLinkedList<int>();
        Assert.That(list.Count, Is.EqualTo(0));
        Assert.That(list.IsEmpty, Is.True);
        Assert.That(list.AsEnumerable(), Is.Empty);
    }

    [Test]
    public void AddFirst_Ordering()
    {
        var list = new SinglyLinkedList<int>();
        list.AddFirst(1);
        list.AddFirst(2);
        list.AddFirst(3);

        CollectionAssert.AreEqual(new[] { 3, 2, 1 }, list.AsEnumerable());
    }

    [Test]
    public void AddLast_Ordering()
    {
        var list = new SinglyLinkedList<int>();
        list.AddLast(1);
        list.AddLast(2);
        list.AddLast(3);

        CollectionAssert.AreEqual(new[] { 1, 2, 3 }, list.AsEnumerable());
    }

    [Test]
    public void Mixed_Adds_WorkCorrectly()
    {
        var list = new SinglyLinkedList<int>();
        list.AddLast(2);
        list.AddFirst(1);
        list.AddLast(3);

        CollectionAssert.AreEqual(new[] { 1, 2, 3 }, list.AsEnumerable());
    }

    [Test]
    public void RemoveFirst_AllCases()
    {
        var list = new SinglyLinkedList<int>();
        Assert.Throws<InvalidOperationException>(() => list.RemoveFirst());

        list.AddLast(1);
        Assert.That(list.RemoveFirst(), Is.EqualTo(1));
        Assert.That(list.IsEmpty, Is.True);

        list.AddLast(1);
        list.AddLast(2);
        Assert.That(list.RemoveFirst(), Is.EqualTo(1));
        CollectionAssert.AreEqual(new[] { 2 }, list.AsEnumerable());
    }

    [Test]
    public void RemoveLast_AllCases()
    {
        var list = new SinglyLinkedList<int>();
        Assert.Throws<InvalidOperationException>(() => list.RemoveLast());

        list.AddLast(1);
        Assert.That(list.RemoveLast(), Is.EqualTo(1));
        Assert.That(list.IsEmpty, Is.True);

        list.AddLast(1);
        list.AddLast(2);
        Assert.That(list.RemoveLast(), Is.EqualTo(2));
        CollectionAssert.AreEqual(new[] { 1 }, list.AsEnumerable());
    }

    [Test]
    public void Remove_ByValue_AllPositions()
    {
        var list = new SinglyLinkedList<int>();
        list.AddLast(1);
        list.AddLast(2);
        list.AddLast(3);

        Assert.That(list.Remove(1), Is.True);
        Assert.That(list.Remove(2), Is.True);
        Assert.That(list.Remove(3), Is.True);
        Assert.That(list.IsEmpty, Is.True);
    }

    [Test]
    public void Remove_Duplicates_RemovesFirstOnly()
    {
        var list = new SinglyLinkedList<int>();
        list.AddLast(1);
        list.AddLast(2);
        list.AddLast(2);
        list.AddLast(3);

        list.Remove(2);
        CollectionAssert.AreEqual(new[] { 1, 2, 3 }, list.AsEnumerable());
    }

    [Test]
    public void InsertAt_AllPositions()
    {
        var list = new SinglyLinkedList<int>();
        list.InsertAt(0, 2);
        list.InsertAt(0, 1);
        list.InsertAt(2, 4);
        list.InsertAt(2, 3);

        CollectionAssert.AreEqual(new[] { 1, 2, 3, 4 }, list.AsEnumerable());
    }

    [Test]
    public void InsertAt_Invalid_Throws()
    {
        var list = new SinglyLinkedList<int>();
        Assert.Throws<ArgumentOutOfRangeException>(() => list.InsertAt(1, 10));
    }

    [Test]
    public void GetAt_AllIndexes()
    {
        var list = new SinglyLinkedList<int>();
        for (int i = 0; i < 10; i++)
            list.AddLast(i);

        for (int i = 0; i < 10; i++)
            Assert.That(list.GetAt(i), Is.EqualTo(i));
    }

    [Test]
    public void GetAt_Invalid_Throws()
    {
        var list = new SinglyLinkedList<int>();
        Assert.Throws<ArgumentOutOfRangeException>(() => list.GetAt(0));
    }

    [Test]
    public void Clear_ResetsCompletely()
    {
        var list = new SinglyLinkedList<int>();
        list.AddLast(1);
        list.AddLast(2);

        list.Clear();

        Assert.That(list.IsEmpty, Is.True);
        list.AddLast(3);
        CollectionAssert.AreEqual(new[] { 3 }, list.AsEnumerable());
    }

    [Test]
    public void Enumeration_IsRepeatableAndStable()
    {
        var list = new SinglyLinkedList<int>();
        list.AddLast(1);
        list.AddLast(2);

        var first = list.AsEnumerable().ToList();
        var second = list.AsEnumerable().ToList();

        CollectionAssert.AreEqual(first, second);
    }

    [Test]
    public void ReferenceEquality_IsUsed()
    {
        var a = new object();
        var b = new object();

        var list = new SinglyLinkedList<object>();
        list.AddLast(a);
        list.AddLast(b);

        Assert.That(list.Contains(a), Is.True);
        Assert.That(list.Contains(new object()), Is.False);
    }

    [Test]
    public void Count_MatchesEnumeration_Always()
    {
        var list = new SinglyLinkedList<int>();
        var rand = new Random(123);

        for (int i = 0; i < 500; i++)
        {
            var op = rand.Next(4);

            if (op == 0)
                list.AddFirst(i);
            else if (op == 1)
                list.AddLast(i);
            else if (op == 2 && !list.IsEmpty)
                list.RemoveFirst();
            else if (op == 3 && !list.IsEmpty)
                list.RemoveLast();

            Assert.That(list.Count, Is.EqualTo(list.AsEnumerable().Count()));
        }
    }

    [Test]
    public void NoCycles_Ever()
    {
        var list = new SinglyLinkedList<int>();
        for (int i = 0; i < 1000; i++)
            list.AddLast(i);

        var seen = new HashSet<int>();
        foreach (var value in list.AsEnumerable())
            Assert.That(seen.Add(value), Is.True);
    }

    [Test]
    public void HeavyMutation_Integrity()
    {
        var list = new SinglyLinkedList<int>();

        for (int i = 0; i < 100; i++)
            list.AddLast(i);

        for (int i = 0; i < 50; i++)
            list.RemoveFirst();

        for (int i = 0; i < 25; i++)
            list.RemoveLast();

        CollectionAssert.AreEqual(
            Enumerable.Range(50, 25),
            list.AsEnumerable()
        );
    }

    [Test]
    public void RemoveLast_IsLinear_NotQuadratic()
    {
        var list = new SinglyLinkedList<int>();

        for (int i = 0; i < 50_000; i++)
            list.AddLast(i);

        var sw = Stopwatch.StartNew();
        list.RemoveLast();
        sw.Stop();

        Assert.That(sw.ElapsedMilliseconds, Is.LessThan(50));
    }
}
