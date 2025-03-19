using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CityBuilder
{
    [Serializable]
    public class SerializeDictionary<TKey, TValue> : IDictionary<TKey, TValue>, ISerializationCallbackReceiver //Unity serializer can't serialize dictionaries
    {
        [SerializeField] private List<TKey> keys = new();
        [SerializeField] private List<TValue> values = new();

        private readonly Dictionary<TKey, TValue> _dictionary = new();

        public SerializeDictionary()
        {
        }

        public SerializeDictionary(Dictionary<TKey, TValue> dictionary)
        {
            foreach (var (key, value) in dictionary)
            {
                Add(key, value);
            }
        }

        public void OnBeforeSerialize()
        {
            keys.Clear();
            values.Clear();
            foreach (var kvp in _dictionary)
            {
                keys.Add(kvp.Key);
                values.Add(kvp.Value);
            }
        }

        public void OnAfterDeserialize()
        {
            _dictionary.Clear();
            for (int i = 0; i < keys.Count; i++)
            {
                _dictionary[keys[i]] = values[i];
            }
        }
        
        public void Clear()
        {
            keys.Clear();
            values.Clear();
            
            _dictionary.Clear();
        }

        public TValue this[TKey key]
        {
            get => _dictionary[key];
            set => _dictionary[key] = value;
        }

        public ICollection<TKey> Keys => _dictionary.Keys;
        public ICollection<TValue> Values => _dictionary.Values;
        public int Count => _dictionary.Count;
        public bool IsReadOnly => false;
        public void Add(TKey key, TValue value) => _dictionary.Add(key, value);
        public bool ContainsKey(TKey key) => _dictionary.ContainsKey(key);
        public bool Remove(TKey key) => _dictionary.Remove(key);
        public bool TryGetValue(TKey key, out TValue value) => _dictionary.TryGetValue(key, out value);
        public void Add(KeyValuePair<TKey, TValue> item) => _dictionary.Add(item.Key, item.Value);
        
        public bool Contains(KeyValuePair<TKey, TValue> item) =>
            _dictionary.ContainsKey(item.Key) &&
            EqualityComparer<TValue>.Default.Equals(_dictionary[item.Key], item.Value);

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex) => throw new NotImplementedException();
        public bool Remove(KeyValuePair<TKey, TValue> item) => _dictionary.Remove(item.Key);
        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator() => _dictionary.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public TValue GetValueOrDefault(TKey key, TValue value) => _dictionary.GetValueOrDefault(key, value);
    }
}