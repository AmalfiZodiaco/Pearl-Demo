using System.Collections.Generic;

namespace Pearl
{
    /// <summary>
    /// A dictionary that can be read either using the first parameter as a key 
    /// and the socond as a value, either using the second parameter as a key 
    /// and the first as a value.
    /// </summary>
    public class Bictionary<T1, T2>
    {
        public readonly Dictionary<T1, T2> keyToValue;
        public readonly Dictionary<T2, T1> valueToKey;


        public Bictionary()
        {
            keyToValue = new Dictionary<T1, T2>();
            valueToKey = new Dictionary<T2, T1>();
        }

        public void Remove(T1 key, T2 value)
        {
            keyToValue.Remove(key);
            valueToKey.Remove(value);
        }

        public void Add(T1 key, T2 value)
        {
            keyToValue.Add(key, value);
            valueToKey.Add(value, key);
        }

        public T2 this[T1 index]
        {
            get
            {
                return keyToValue[index];
            }
            set
            {
                if (keyToValue.ContainsKey(index))
                    Remove(index, keyToValue[index]);

                Add(index, value);
            }
        }

        public T1 this[T2 index]
        {
            get
            {
                return valueToKey[index];
            }
            set
            {
                if (keyToValue.ContainsKey(value))
                    Remove(value, keyToValue[value]);

                Add(value, index);
            }
        }
    }
}
