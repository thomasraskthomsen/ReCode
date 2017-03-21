using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ReCode.RegularExpressions.Util
{
    /// <summary>
    /// A collection that map keys to multiple values invalues in sorted order.
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    public class SortedMultiMap<TKey,TValue> : IEnumerable<KeyValuePair<TKey, TValue>>
    {
        private readonly SortedDictionary<TKey,SortedSet<TValue>> _dict = new SortedDictionary<TKey, SortedSet<TValue>>();

        /// <summary>
        /// Associates the specified value with the specified key.
        /// </summary>
        public void Add(TKey key, TValue value)
        {
            SortedSet<TValue> res;
            if (!_dict.TryGetValue(key, out res))
            {
                res = new SortedSet<TValue>();
                _dict.Add(key, res);
            }
            res.Add(value);
        }

        /// <summary>
        /// Returns all key value pairs (with duplicate keys when same key is associated with multiple values.
        /// </summary>
        /// <returns></returns>
        private IEnumerable<KeyValuePair<TKey, TValue>> GetAllPairs()
        {
            return from pair in _dict
                   from item in pair.Value
                   select new KeyValuePair<TKey, TValue>(pair.Key, item);
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return GetAllPairs().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetAllPairs().GetEnumerator();
        }

        public IEnumerable<KeyValuePair<TKey, ICollection<TValue>>> GroupedPairs
        {
            get {
                return _dict.Select(pair => new KeyValuePair<TKey, ICollection<TValue>>(pair.Key, pair.Value));
            }
        } 
    }
}
