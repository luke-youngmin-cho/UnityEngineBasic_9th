using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collections
{
    internal class MyHashtable<TKey, TValue>
    {
        public TValue this[TKey key]
        {
            get
            {
                int index = Hash(key);
                for (int i = 0; i < buckets[index].Count; i++)
                {
                    if (Comparer<TKey>.Default.Compare(buckets[index][i].Key, key) == 0)
                        return buckets[index][i].Value;
                }

                throw new Exception($"[MyHashtable] : {key} is not exist");
            }
            set
            {
                int index = Hash(key);
                for (int i = 0; i < buckets[index].Count; i++)
                {
                    if (Comparer<TKey>.Default.Compare(buckets[index][i].Key, key) == 0)
                        buckets[index][i] = new KeyValuePair(key, value);
                }

                throw new Exception($"[MyHashtable] : {key} is not exist");
            }
        }

        private struct KeyValuePair : IComparable<KeyValuePair>
        {
            public TKey Key;
            public TValue Value;

            public KeyValuePair(TKey key, TValue value)
            {
                Key = key;
                Value = value;
            }

            public int CompareTo(MyHashtable<TKey, TValue>.KeyValuePair other)
            {
                return Comparer<KeyValuePair>.Default.Compare(this, other);
            }
        }

        private MyDynamicArray<KeyValuePair>[] buckets;
        private int _capacity;
        public MyHashtable(int capacity)
        {
            _capacity = capacity;
            buckets = new MyDynamicArray<KeyValuePair>[capacity];
            for (int i = 0; i < capacity; i++)
            {
                buckets[i] = new MyDynamicArray<KeyValuePair>();
            }
        }

        public void Add(TKey key, TValue value)
        {
            int index = Hash(key);
            for (int i = 0; i < buckets[index].Count; i++)
            {
                if (Comparer<TKey>.Default.Compare(buckets[index][i].Key, key) == 0)
                    throw new ArgumentException($"[MyHashtable] : {key} has already added");
            }

            buckets[index].Add(new KeyValuePair(key, value));
        }

        public bool Contains(TKey key)
        {
            int index = Hash(key);
            for (int i = 0; i < buckets[index].Count; i++)
            {
                if (Comparer<TKey>.Default.Compare(buckets[index][i].Key, key) == 0)
                    return true;
            }
            return false;
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            value = default(TValue);
            int index = Hash(key);
            for (int i = 0; i < buckets[index].Count; i++)
            {
                if (Comparer<TKey>.Default.Compare(buckets[index][i].Key, key) == 0)
                {
                    value = buckets[index][i].Value;
                    return true;
                }
            }
            return false;
        }

        public bool Remove(TKey key)
        {
            int index = Hash(key);
            for (int i = 0; i < buckets[index].Count; i++)
            {
                if (Comparer<TKey>.Default.Compare(buckets[index][i].Key, key) == 0)
                {
                    buckets[index].RemoveAt(i);
                    return true;
                }
            }
            return false;
        }

        public int Hash(TKey key)
        {
            string keyName = key.ToString();
            int hashValue = 0;
            for (int i = 0; i < keyName.Length; i++)
            {
                hashValue += keyName[i];
            }
            return hashValue %= _capacity;
        }
    }
}
