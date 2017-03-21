using System.Collections;
using System.Collections.Generic;
using ReCode.Parsers.Grammatics;

namespace ReCode.Parsers.Generation.Data
{
    /// <summary>
    /// The global collection of LR items.
    /// </summary>
    public class LrItemCollection : IReadOnlyCollection<LrItem>
    {
        private readonly SortedDictionary<string, LrItem> _items = new SortedDictionary<string, LrItem>();

        /// <summary>
        /// Gets or creates an item representing the specified parameters.
        /// </summary>
        public LrItem GetOrAdd(Production prod, int productionProgress, int lookAheadSymbol)
        {
            var id = LrItem.MakeId(prod, productionProgress, lookAheadSymbol);
            LrItem res;
            if (_items.TryGetValue(id, out res))
                return res;
            res = new LrItem(prod, productionProgress, lookAheadSymbol);
            _items.Add(id, res);
            return res;
        }

        IEnumerator<LrItem> IEnumerable<LrItem>.GetEnumerator()
        {
            return _items.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _items.Values.GetEnumerator();
        }

        /// <summary>
        /// The total number of items.
        /// </summary>
        public int Count => _items.Count;

    }
}
