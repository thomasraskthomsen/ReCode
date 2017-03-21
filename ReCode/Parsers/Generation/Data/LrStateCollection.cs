using System.Collections;
using System.Collections.Generic;

namespace ReCode.Parsers.Generation.Data
{
    /// <summary>
    /// The global collection of LR states.
    /// </summary>
    public class LrStateCollection : IReadOnlyCollection<LrState>
    {
        private readonly SortedDictionary<string, LrState> _items = new SortedDictionary<string, LrState>();

        /// <summary>
        /// Gets or creates a state representing the specified parameters.
        /// </summary>
        public LrState GetOrAdd(SortedSet<LrItem> items)
        {
            bool added;
            return GetOrAdd(items, out added);
        }

        /// <summary>
        /// Gets or creates a state representing the specified parameters.
        /// </summary>
        public LrState GetOrAdd(SortedSet<LrItem> items, out bool added)
        {
            var id = LrState.MakeId(items);
            LrState res;
            if (_items.TryGetValue(id, out res))
            {
                added = false;
                return res;
            }
            res = new LrState(_items.Count, items);
            _items.Add(id, res);
            added = true;
            return res;
        }

        IEnumerator<LrState> IEnumerable<LrState>.GetEnumerator()
        {
            return _items.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _items.Values.GetEnumerator();
        }

        /// <summary>
        /// The total number of states.
        /// </summary>
        public int Count => _items.Count;
    }
}
