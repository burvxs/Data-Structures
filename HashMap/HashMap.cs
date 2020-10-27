using System;
using System.Collections.Generic;

namespace HashMapStructure
{
    public class HashMap<K, V>
    {
        private readonly K key;
        private readonly V value;
        private readonly List<HashMap<K, V>> storage;

        public object this[object key]
        {
            get => LookUp(key);
            set => NewMap((K)key, (V)value);
        }

        public HashMap(K key, V value)
        {
            this.key = key;
            this.value = value;
            this.storage = new List<HashMap<K, V>>();
            InitialAdd();
        }

        public HashMap()
        {
            this.storage = new List<HashMap<K, V>>();
            InitialAdd();
        }

        public HashMap<K, V> First()
        {
            return storage[0];
        }

        private void InitialAdd()
        {
            if (this.storage.Count == 0)
            {
                this.storage.Add(this);
            }
        }

        public void Each(Action<K, V, int> iterator)
        {
            for (int i = 0; i < storage.Count; i++)
            {
                iterator.Invoke(storage[i].key, storage[i].value, i);
            }
        }

        public void Each(Action<K, int> iterator)
        {
            for (int i = 0; i < storage.Count; i++)
            {
                iterator.Invoke(storage[i].key, i);
            }
        }

        public void Each(Action<V, int> iterator)
        {
            for (int i = 0; i < storage.Count; i++)
            {
                iterator.Invoke(storage[i].value, i);
            }
        }

        public V LookUp(object key)
        {
            V result = default;
            for (int i = 0; i < storage.Count; i++)
            {
                object currentKey = storage[i].key;
                if (key == currentKey)
                {
                    result = storage[i].value;
                }
            }

            return result;
        }

        public HashMap<K, V> LookUpMap(object key)
        {
            HashMap<K, V> result = default;
            for (int i = 0; i < storage.Count; i++)
            {
                object currentKey = storage[i].key;
                if (key == currentKey)
                {
                    result = storage[i];
                }
            }

            return result;
        }

        public void Merge(HashMap<K, V> target)
        {
            storage.Add(target);
        }

        public bool ContainsValue(object value)
        {
            bool contains = false;
            Each((key, val, i) =>
            {
                object currentValue = val;
                if (currentValue == value)
                {
                    contains = true;
                }
            });

            return contains;
        }

        public bool ContainsKey(object key)
        {
            bool contains = false;
            Each((k, val, i) =>
            {
                object currentKey = k;
                if (currentKey == key)
                {
                    contains = true;
                }
            });
            return contains;
        }

        public List<K> KeySet()
        {
            if (storage.Count <= 0)
            {
                throw new Exception("This dictionary is empty please use the \n" + " NewMap method to fill it");
            }

            List<K> keySet = new List<K>();
            Each((key, value, index) =>
            {
                keySet.Add(key);
            });

            return keySet;
        }

        public List<V> ValueSet()
        {
            if (storage.Count <= 0)
            {
                throw new Exception("This dictionary is empty please use the \n" + " NewMap method to fill it");
            }

            List<V> valueSet = new List<V>();
            Each((key, value, index) =>
            {
                valueSet.Add(value);
            });

            return valueSet;
        }

        public void NewMap(K key, V value)
        {
            if (!ContainsKey(key))
            {
                storage.Add(new HashMap<K, V>(key, value));
            }
            else
            {
                storage[storage.FindIndex(predicate => predicate.key.Equals(key))] = new HashMap<K, V>(key, value);
            }
        }

        public void Remove(K key)
        {
            object hashMap = LookUpMap(key);
            storage.Remove((HashMap<K, V>)hashMap);
        }

        public int Size()
        {
            return storage.Count;
        }

        public void Clear()
        {
            storage.Clear();
        }


        public string toString()
        {
            string output = "";
            output += $"Key Type: {key.GetType()} Value Type: {value.GetType()} \n";
            Each((key, value, index) =>
            {
                output += $"K: {key} : V: {value} \n";
            });
            return output;
        }

        public HashMap<K, V> Clone()
        {
            HashMap<K, V> newMap = this;
            return newMap;
        }

        public static HashMap<K, V> operator +(HashMap<K, V> mergee){
            return new HashMap<K, V>(mergee.key, mergee.value);
        }
    }
}
