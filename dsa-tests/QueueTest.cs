using System;
using NUnit.Framework;
using data_structures;

namespace dsa_tests;

[TestFixture]
public class QueueTests
{
    [Test]
    public void New_queue_is_empty()
    {
        var queue = new data_structures.Queue<int>();

        Assert.That(queue.Count, Is.EqualTo(0));
        Assert.That(queue.IsEmpty, Is.True);
    }

    [Test]
    public void Enqueue_increases_count_and_makes_queue_non_empty()
    {
        var queue = new data_structures.Queue<int>();

        queue.Enqueue(1);

        Assert.That(queue.Count, Is.EqualTo(1));
        Assert.That(queue.IsEmpty, Is.False);
    }

    [Test]
    public void Peek_returns_front_item_without_removing_it()
    {
        var queue = new data_structures.Queue<int>();

        queue.Enqueue(1);
        queue.Enqueue(2);

        var value = queue.Peek();

        Assert.That(value, Is.EqualTo(1));
        Assert.That(queue.Count, Is.EqualTo(2));
    }

    [Test]
    public void Dequeue_returns_front_item_and_removes_it()
    {
        var queue = new data_structures.Queue<int>();

        queue.Enqueue(1);
        queue.Enqueue(2);

        var value = queue.Dequeue();

        Assert.That(value, Is.EqualTo(1));
        Assert.That(queue.Count, Is.EqualTo(1));
        Assert.That(queue.Peek(), Is.EqualTo(2));
    }

    [Test]
    public void Dequeue_on_empty_queue_throws()
    {
        var queue = new data_structures.Queue<int>();

        Assert.That(() => queue.Dequeue(), Throws.InvalidOperationException);
    }

    [Test]
    public void Peek_on_empty_queue_throws()
    {
        var queue = new data_structures.Queue<int>();

        Assert.That(() => queue.Peek(), Throws.InvalidOperationException);
    }

    [Test]
    public void Enqueue_dequeue_sequence_preserves_FIFO_order()
    {
        var queue = new data_structures.Queue<int>();

        queue.Enqueue(1);
        queue.Enqueue(2);
        queue.Enqueue(3);

        Assert.That(queue.Dequeue(), Is.EqualTo(1));
        Assert.That(queue.Dequeue(), Is.EqualTo(2));
        Assert.That(queue.Dequeue(), Is.EqualTo(3));
        Assert.That(queue.IsEmpty, Is.True);
    }

    [Test]
    public void Interleaved_enqueue_and_dequeue_behaves_correctly()
    {
        var queue = new data_structures.Queue<int>();

        queue.Enqueue(1);
        Assert.That(queue.Dequeue(), Is.EqualTo(1));

        queue.Enqueue(2);
        queue.Enqueue(3);
        Assert.That(queue.Dequeue(), Is.EqualTo(2));

        queue.Enqueue(4);
        Assert.That(queue.Dequeue(), Is.EqualTo(3));
        Assert.That(queue.Dequeue(), Is.EqualTo(4));
    }

    [Test]
    public void Count_is_correct_after_multiple_operations()
    {
        var queue = new data_structures.Queue<int>();

        queue.Enqueue(1);
        queue.Enqueue(2);
        queue.Enqueue(3);

        queue.Dequeue();
        queue.Dequeue();

        queue.Enqueue(4);

        Assert.That(queue.Count, Is.EqualTo(2));
    }

    [Test]
    public void Clear_empties_the_queue()
    {
        var queue = new data_structures.Queue<int>();

        queue.Enqueue(1);
        queue.Enqueue(2);
        queue.Enqueue(3);

        queue.Clear();

        Assert.That(queue.Count, Is.EqualTo(0));
        Assert.That(queue.IsEmpty, Is.True);
        Assert.That(() => queue.Dequeue(), Throws.InvalidOperationException);
    }

    [Test]
    public void Queue_handles_reference_types()
    {
        var queue = new data_structures.Queue<string>();

        queue.Enqueue("a");
        queue.Enqueue("b");

        Assert.That(queue.Dequeue(), Is.EqualTo("a"));
        Assert.That(queue.Dequeue(), Is.EqualTo("b"));
    }

    [Test]
    public void Queue_handles_duplicate_values()
    {
        var queue = new data_structures.Queue<int>();

        queue.Enqueue(1);
        queue.Enqueue(1);
        queue.Enqueue(1);

        Assert.That(queue.Dequeue(), Is.EqualTo(1));
        Assert.That(queue.Dequeue(), Is.EqualTo(1));
        Assert.That(queue.Dequeue(), Is.EqualTo(1));
    }

    [Test]
    public void Repeated_emptying_and_refilling_queue_is_stable()
    {
        var queue = new data_structures.Queue<int>();

        for (var i = 0; i < 10; i++)
        {
            queue.Enqueue(i);
        }

        for (var i = 0; i < 10; i++)
        {
            Assert.That(queue.Dequeue(), Is.EqualTo(i));
        }

        Assert.That(queue.IsEmpty, Is.True);

        for (var i = 0; i < 5; i++)
        {
            queue.Enqueue(i);
        }

        Assert.That(queue.Dequeue(), Is.EqualTo(0));
        Assert.That(queue.Count, Is.EqualTo(4));
    }
}
