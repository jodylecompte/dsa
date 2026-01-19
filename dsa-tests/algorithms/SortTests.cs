namespace dsa_tests.algorithms;

using System.Collections.Generic;
using NUnit.Framework;
using data_structures.algorithms.sorting;

[TestFixture]
public class SortTests
{
    [Test]
    public void BubbleSort_SortsUnorderedList()
    {
        var list = new List<int> { 5, 1, 4, 2, 8 };

        BubbleSort.Sort(list);

        Assert.That(list, Is.EqualTo(new List<int> { 1, 2, 4, 5, 8 }));
    }
}
