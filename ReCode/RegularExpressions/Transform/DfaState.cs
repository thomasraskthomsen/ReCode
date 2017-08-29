using System.Collections.Generic;
using System.Text;
using ReCode.RegularExpressions.Evaluation;
using ReCode.RegularExpressions.Parsing.Nodes;
using ReCode.RegularExpressions.Util;

namespace ReCode.RegularExpressions.Transform
{

    /// <summary>
    /// Class that represents a node in the DFA graph
    /// </summary>
    public class DfaState
    {
        private struct FastHash
        {
            public ulong Hash;

            public void Apply(ulong value)
            {
                var key = Hash;
                key += value;
                key = (~key) + (key << 21); // key = (key << 21) - key - 1;
                key ^= (key >> 24);
                key += (key << 3) + (key << 8); // key * 265
                key ^= (key >> 14);
                key += (key << 2) + (key << 4); // key * 21
                key ^= (key >> 28);
                key += (key << 31);
                Hash = key;
            }
        }

        /// <summary>
        /// Calculates the id string for a state represented by the supplied underlying NDA noces.
        /// </summary>
        /// <param name="states">The collection of underlying NDA states.</param>
        /// <param name="acceptState">The largest processed accept state so far.</param>
        /// <returns>The id</returns>
        public static ulong GetDfaId(ushort? acceptState, ICollection<NfaState> states)
        {
            var hasher = new FastHash();
            foreach (var s in states)
                hasher.Apply((ulong)s.NfaId);
            hasher.Apply(0);
            if(acceptState.HasValue)
                hasher.Apply(acceptState.Value);
            return hasher.Hash;
        }

        /// <summary>
        /// The id of this state.
        /// </summary>
        public readonly int DfaId;

        /// <summary>
        /// The best accept state;
        /// </summary>
        public readonly ushort? SmallestAcceptState;
        /// <summary>
        /// The set of Nfa states represented by this DFA state.
        /// </summary>
        public readonly HashSet<NfaState> NfaStates;
        /// <summary>
        /// The set of Nfa nodes that can be reached from this DFA.
        /// </summary>
        public readonly List<KeyValuePair<RegExInputRange, NfaState>> NfaMap = new List<KeyValuePair<RegExInputRange, NfaState>>();
        /// <summary>
        /// The underlying evanluation node.
        /// </summary>
        public readonly RegExEvaluationNode EvaluationNode;

        /// <summary>
        /// Creates an instance of a DFA state object from the specified parameters.
        /// </summary>
        public DfaState(ushort? smallestAcceptState, ICollection<NfaState> nfaStates, int dfaId)
        {
            DfaId = dfaId;
            SmallestAcceptState = smallestAcceptState;
            NfaStates = new HashSet<NfaState>(nfaStates);
            EvaluationNode = new RegExEvaluationNode {Id = dfaId};
        }


        private static IEnumerable<RegExInputRange> SplitRange(RegExInputRange range, IEnumerable<char> splitPoints)
        {
            if (range.Min == range.Max)
            {
                yield return range;
                yield break;
            }
            var pending = range;
            foreach (var p in splitPoints)
            {
                if (p > pending.Max) break;
                if ((p > range.Min) && (p < pending.Max))
                {
                    var sub = new RegExInputRange((char)pending.Min, (char)(p - 1));
                    yield return sub;
                    pending = new RegExInputRange(p, (char)pending.Max);
                    if (pending.Min == pending.Max)
                    {
                        yield return pending;
                        yield break;
                    }

                }
            }
            yield return pending;
        }

        /// <summary>
        /// Gets the collection of non-epsilon transitions from this node grouped by non-overlapping ranges.
        /// </summary>
        public IEnumerable<KeyValuePair<RegExInputRange, ICollection<NfaState>>> GetGroupedTransition()
        {
            var splitPoints = new SortedSet<char>();
            foreach (var map in NfaMap)
            {
                if (map.Key == RegExInputRange.EPS) continue;
                if (map.Key.Min > 0)
                    splitPoints.Add((char)map.Key.Min);
                if (map.Key.Max != char.MaxValue)
                    splitPoints.Add((char)(map.Key.Max + 1));
            }
            var groupedMap = new SortedMultiMap<RegExInputRange, NfaState>();
            foreach (var map in NfaMap)
            {
                if (map.Key == RegExInputRange.EPS) continue;
                foreach (var split in SplitRange(map.Key, splitPoints))
                    foreach(var state in map.Value.EpsilonStates)
                        groupedMap.Add(split, state);
            }
            return groupedMap.GroupedPairs;
        }
    }
}
