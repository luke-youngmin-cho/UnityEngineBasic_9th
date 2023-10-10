using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct UKeyValuePair<TKey, TValue>
{
    public TKey key;
    public TValue value;

    public UKeyValuePair(TKey key, TValue value)
    {
        this.key = key;
        this.value = value;
    }
}

[Serializable]
public class UDictionary<TKey, TValue> : ISerializationCallbackReceiver
{
    public TValue this[TKey key]
    {
        get => _dictionary[key];
        set => _dictionary[key] = value;
    }
    [SerializeField] private List<UKeyValuePair<TKey, TValue>> _list;
    private Dictionary<TKey, TValue> _dictionary;

    public void Add(TKey key, TValue value) => _dictionary.Add(key, value);
    public bool TryGetValue(TKey key, out TValue value) => _dictionary.TryGetValue(key, out value);

    public void OnBeforeSerialize()
    {
    }

    public void OnAfterDeserialize()
    {
        _dictionary = new Dictionary<TKey, TValue>();
        foreach (var pair in _list)
        {
            _dictionary.Add(pair.key, pair.value);
        }
    }
}
