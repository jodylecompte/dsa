using System;
using System.Collections.Generic;
using NUnit.Framework;
using data_structures;

namespace dsa_tests;

[TestFixture]
public class HashMapTests
{
    [Test]
    public void New_map_is_empty()
    {
        var map = new HashMap<string, int>();

        Assert.That(map.Count, Is.EqualTo(0));
        Assert.That(map.IsEmpty, Is.True);
    }

    [Test]
    public void Add_single_item_increases_count()
    {
        var map = new HashMap<string, int>();

        map.Add("a", 1);

        Assert.That(map.Count, Is.EqualTo(1));
        Assert.That(map.IsEmpty, Is.False);
    }

    [Test]
    public void Add_duplicate_key_throws()
    {
        var map = new HashMap<string, int>();

        map.Add("a", 1);

        Assert.That(() => map.Add("a", 2), Throws.InvalidOperationException);
    }

    [Test]
    public void Get_returns_value_for_existing_key()
    {
        var map = new HashMap<string, int>();

        map.Add("a", 1);
        map.Add("b", 2);

        Assert.That(map.Get("a"), Is.EqualTo(1));
        Assert.That(map.Get("b"), Is.EqualTo(2));
    }

    [Test]
    public void Get_on_missing_key_throws()
    {
        var map = new HashMap<string, int>();

        Assert.That(() => map.Get("missing"), Throws.InvalidOperationException);
    }

    [Test]
    public void ContainsKey_returns_true_for_existing_key()
    {
        var map = new HashMap<string, int>();

        map.Add("a", 1);

        Assert.That(map.ContainsKey("a"), Is.True);
        Assert.That(map.ContainsKey("b"), Is.False);
    }

    [Test]
    public void Remove_existing_key_removes_and_decrements_count()
    {
        var map = new HashMap<string, int>();

        map.Add("a", 1);
        map.Add("b", 2);

        var removed = map.Remove("a");

        Assert.That(removed, Is.True);
        Assert.That(map.Count, Is.EqualTo(1));
        Assert.That(map.ContainsKey("a"), Is.False);
    }

    [Test]
    public void Remove_missing_key_returns_false()
    {
        var map = new HashMap<string, int>();

        Assert.That(map.Remove("missing"), Is.False);
    }

    [Test]
    public void Hash_collision_keys_are_handled_correctly()
    {
        var map = new HashMap<int, string>();

        var key1 = 1;
        var key2 = 17; // assume small bucket count, force collision

        map.Add(key1, "one");
        map.Add(key2, "seventeen");

        Assert.That(map.Get(key1), Is.EqualTo("one"));
        Assert.That(map.Get(key2), Is.EqualTo("seventeen"));
    }

    [Test]
    public void Removing_one_key_does_not_affect_other_colliding_keys()
    {
        var map = new HashMap<int, string>();

        map.Add(1, "one");
        map.Add(17, "seventeen");

        map.Remove(1);

        Assert.That(map.ContainsKey(17), Is.True);
        Assert.That(map.Get(17), Is.EqualTo("seventeen"));
    }

    [Test]
    public void Clear_resets_map_to_empty_state()
    {
        var map = new HashMap<string, int>();

        map.Add("a", 1);
        map.Add("b", 2);

        map.Clear();

        Assert.That(map.Count, Is.EqualTo(0));
        Assert.That(map.IsEmpty, Is.True);
        Assert.That(map.ContainsKey("a"), Is.False);
    }

    [Test]
    public void Map_handles_reference_type_values()
    {
        var map = new HashMap<int, List<int>>();

        var list = new List<int> { 1, 2, 3 };

        map.Add(1, list);

        Assert.That(map.Get(1), Is.SameAs(list));
    }

    [Test]
    public void Enumerator_returns_all_key_value_pairs()
    {
        var map = new HashMap<string, int>();

        map.Add("a", 1);
        map.Add("b", 2);
        map.Add("c", 3);

        var results = new Dictionary<string, int>();

        foreach (var kvp in map)
        {
            results.Add(kvp.Key, kvp.Value);
        }

        Assert.That(results.Count, Is.EqualTo(3));
        Assert.That(results["a"], Is.EqualTo(1));
        Assert.That(results["b"], Is.EqualTo(2));
        Assert.That(results["c"], Is.EqualTo(3));
    }
}
