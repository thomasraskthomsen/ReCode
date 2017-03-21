using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReCode.Parsers.Generation.Data
{
    /// <summary>
    /// A parser LR State (representing a set of <see cref="LrItem"/>)
    /// </summary>
    public class LrState : IComparable<LrState>
    {
        /// <summary>
        /// A string that uniquely identifies the state (created using <see cref="MakeId"/>).
        /// </summary>
        public readonly string Id;
        /// <summary>
        /// The unique index of the state (enumerated by <see cref="LrStateCollection"/>).
        /// </summary>
        public readonly int Index;
        /// <summary>
        /// The underlying items.
        /// </summary>
        public IReadOnlyList<LrItem> Items;
        /// <summary>
        /// The associated parser Goto's (one for each symbol - nullable).
        /// </summary>
        public IReadOnlyList<LrState> Gotos { get; set; }
        /// <summary>
        /// The associated parser Goto priorities (one for each symbol - nullable).
        /// </summary>
        public IReadOnlyList<ushort?> GotoPrecedences { get; set; }
        /// <summary>
        /// The associated parser Actions (one for each symbol - nullable).
        /// </summary>
        public IReadOnlyList<LrAction> Actions { get; set; } 

        /// <summary>
        /// Creates a new state from the specified paramters.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="items"></param>
        public LrState(int index, SortedSet<LrItem> items)
        {
            Id = MakeId(items);
            Index = index;
            Items = items.ToArray();
        }

        /// <summary>
        /// Creates a new 
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public static string MakeId(SortedSet<LrItem> items)
        {
            var sb = new StringBuilder();
            var first = true;
            foreach (var item in items)
            {
                if (first)
                    first = false;
                else
                    sb.Append(' ');
                sb.Append(item.Id);
            }
            return sb.ToString();
        }

        int IComparable<LrState>.CompareTo(LrState other)
        {
            return string.Compare(Id, other.Id, StringComparison.Ordinal);
        }

        /// <summary>
        /// A humanly readable repesentation of the item.
        /// </summary>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append($"{Index}-");
            sb.Append("[ ");
            var first = true;
            foreach (var item in Items)
            {
                if (first)
                    first = false;
                else
                    sb.Append(" | ");
                sb.Append(item);
            }
            sb.Append(']');
            return sb.ToString();
        }
    }
}
