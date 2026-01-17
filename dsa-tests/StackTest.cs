using data_structures;
namespace dsa_tests;
using Stack = data_structures.Stack<int>;

[TestFixture]
public class StackTests
{
    [Test]
    public void New_stack_is_empty()
    {
        var stack = new data_structures.Stack<int>();

        Assert.That(stack.Count, Is.EqualTo(0));
        Assert.That(stack.IsEmpty, Is.True);
    }

    [Test]
    public void Push_increases_count_and_makes_stack_non_empty()
    {
        var stack = new data_structures.Stack<int>();

        stack.Push(10);

        Assert.That(stack.Count, Is.EqualTo(1));
        Assert.That(stack.IsEmpty, Is.False);
    }

    [Test]
    public void Peek_returns_top_item_without_removing_it()
    {
        var stack = new data_structures.Stack<int>();

        stack.Push(1);
        stack.Push(2);

        var value = stack.Peek();

        Assert.That(value, Is.EqualTo(2));
        Assert.That(stack.Count, Is.EqualTo(2));
    }

    [Test]
    public void Pop_returns_top_item_and_removes_it()
    {
        var stack = new data_structures.Stack<int>();

        stack.Push(1);
        stack.Push(2);

        var value = stack.Pop();

        Assert.That(value, Is.EqualTo(2));
        Assert.That(stack.Count, Is.EqualTo(1));
        Assert.That(stack.Peek(), Is.EqualTo(1));
    }

    [Test]
    public void Pop_on_empty_stack_throws()
    {
        var stack = new data_structures.Stack<int>();

        Assert.That(() => stack.Pop(), Throws.InvalidOperationException);
    }

    [Test]
    public void Peek_on_empty_stack_throws()
    {
        var stack = new data_structures.Stack<int>();

        Assert.That(() => stack.Peek(), Throws.InvalidOperationException);
    }

    [Test]
    public void Push_pop_sequence_preserves_LIFO_order()
    {
        var stack = new data_structures.Stack<int>();

        stack.Push(1);
        stack.Push(2);
        stack.Push(3);

        Assert.That(stack.Pop(), Is.EqualTo(3));
        Assert.That(stack.Pop(), Is.EqualTo(2));
        Assert.That(stack.Pop(), Is.EqualTo(1));
        Assert.That(stack.IsEmpty, Is.True);
    }

    [Test]
    public void Interleaved_push_and_pop_behaves_correctly()
    {
        var stack = new data_structures.Stack<int>();

        stack.Push(1);
        Assert.That(stack.Pop(), Is.EqualTo(1));

        stack.Push(2);
        stack.Push(3);
        Assert.That(stack.Pop(), Is.EqualTo(3));

        stack.Push(4);
        Assert.That(stack.Pop(), Is.EqualTo(4));
        Assert.That(stack.Pop(), Is.EqualTo(2));
    }

    [Test]
    public void Count_is_correct_after_multiple_operations()
    {
        var stack = new data_structures.Stack<int>();

        stack.Push(1);
        stack.Push(2);
        stack.Push(3);

        stack.Pop();
        stack.Pop();

        stack.Push(4);

        Assert.That(stack.Count, Is.EqualTo(2));
    }

    [Test]
    public void Clear_empties_the_stack()
    {
        var stack = new data_structures.Stack<int>();

        stack.Push(1);
        stack.Push(2);
        stack.Push(3);

        stack.Clear();

        Assert.That(stack.Count, Is.EqualTo(0));
        Assert.That(stack.IsEmpty, Is.True);
        Assert.That(() => stack.Pop(), Throws.InvalidOperationException);
    }

    [Test]
    public void Stack_handles_reference_types()
    {
        var stack = new data_structures.Stack<string>();

        stack.Push("a");
        stack.Push("b");

        Assert.That(stack.Pop(), Is.EqualTo("b"));
        Assert.That(stack.Pop(), Is.EqualTo("a"));
    }

    [Test]
    public void Stack_handles_duplicate_values()
    {
        var stack = new data_structures.Stack<int>();

        stack.Push(1);
        stack.Push(1);
        stack.Push(1);

        Assert.That(stack.Pop(), Is.EqualTo(1));
        Assert.That(stack.Pop(), Is.EqualTo(1));
        Assert.That(stack.Pop(), Is.EqualTo(1));
    }

    [Test]
    public void Repeated_emptying_and_refilling_stack_is_stable()
    {
        var stack = new data_structures.Stack<int>();

        for (var i = 0; i < 10; i++)
        {
            stack.Push(i);
        }

        for (var i = 9; i >= 0; i--)
        {
            Assert.That(stack.Pop(), Is.EqualTo(i));
        }

        Assert.That(stack.IsEmpty, Is.True);

        for (var i = 0; i < 5; i++)
        {
            stack.Push(i);
        }

        Assert.That(stack.Pop(), Is.EqualTo(4));
        Assert.That(stack.Count, Is.EqualTo(4));
    }
}
