using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace DeveReproduceXmlSerializerBug.Collections.Concurrent
{
    public class ConcurrentHashSet<T> : IEnumerable<T> where T : notnull
    {
        private readonly ConcurrentDictionary<T, byte> _internalDict;
        private const byte DefaultDictValue = 0;

        public ConcurrentHashSet()
        {
            _internalDict = new ConcurrentDictionary<T, byte>();
        }

        public ConcurrentHashSet(IEnumerable<T> values)
        {
            _internalDict = new ConcurrentDictionary<T, byte>(values.Select(t => new KeyValuePair<T, byte>(t, DefaultDictValue)));
        }

        public bool Add(T value)
        {
            return TryAdd(value);
        }

        public bool TryAdd(T value)
        {
            return _internalDict.TryAdd(value, DefaultDictValue);
        }

        public bool Contains(T value)
        {
            return _internalDict.ContainsKey(value);
        }

        public IEnumerator<T> GetEnumerator()
        {
            foreach (var item in _internalDict)
            {
                yield return item.Key;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
