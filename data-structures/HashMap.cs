using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using NullReferenceException = System.NullReferenceException;

namespace data_structures;

public class HashMap<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>>
{
    
    private class Entry
    {
        public TKey Key { get; }
        public TValue Value { get; }

        public Entry(TKey key, TValue value)
        {
            Key = key;
            Value = value;
        }
    }

    public int Count = 0;
    public bool IsEmpty => Count == 0;
    
    List<Entry>[] buckets = new List<Entry>[16];

    public HashMap()
    {
        for (var i = 0; i < buckets.Length; i++)
        {
            buckets[i] = new List<Entry>();
        }
    }

    public void Add(TKey key, TValue value)
    {
        if (key == null)
        {
            throw new ArgumentNullException();
        }

        var index = Hash(key);
        var bucket = buckets[index];
        
        foreach(Entry entry in bucket)
        {
            if (EqualityComparer<TKey>.Default.Equals(entry.Key, key))
            {
                throw new InvalidOperationException();
            }
        }
        
        bucket.Add(new Entry(key, value));
        Count++;
    }

    public bool Remove(TKey key)
    {
        var index = Hash(key);
        var bucket = buckets[index];

        for (int i = 0; i < bucket.Count; ++i)
        {
            if (EqualityComparer<TKey>.Default.Equals(bucket[i].Key, key))
            {
                bucket.RemoveAt(i);
                Count--;
                return true;
            } 
        }

        return false;
    }

    public bool ContainsKey(TKey key)
    {
        var index = Hash(key);
        var bucket = buckets[index];

        foreach (var entry in bucket)
        {
            if (EqualityComparer<TKey>.Default.Equals(entry.Key, key))
            {
                return true;
            }
        }
        
        return false;
    }

    public TValue Get(TKey key)
    {
        var index = Hash(key);
        var bucket = buckets[index];

        foreach (var entry in bucket)
        {
            if (EqualityComparer<TKey>.Default.Equals(entry.Key, key))
            {
                return entry.Value;
            }
        }

        throw new InvalidOperationException();
    }

    public void Clear()
    {
        foreach (var bucket in buckets)
        {
            bucket.Clear();
        }

        Count = 0;
    }

    public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
    {
        foreach (var bucket in buckets)
        {
            foreach (var entry in bucket)
            {
                yield return new KeyValuePair<TKey, TValue>(entry.Key, entry.Value);
            }
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    private int Hash(TKey key)
    {
        if (key == null)
        {
            throw new ArgumentNullException();
        }
        
        var hash = key.GetHashCode();
        var index = Int32.Abs(hash % buckets.Length);

        return index;
    }
}