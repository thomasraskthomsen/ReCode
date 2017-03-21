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
        /// <summary>
        /// Calculates the id string for a state represented by the supplied underlying NDA noces.
        /// </summary>
        /// <param name="states">The collection of underlying NDA states.</param>
        /// <param name="acceptState">The largest processed accept state so far.</param>
        /// <returns>The id</returns>
        public static string GetDfaName(ushort? acceptState, ICollection<NfaState> states)
        {
            // just append the numbers into a string.
            var sb = new StringBuilder();
            var isFirst = true;
            foreach (var s in states)
            {
                if (isFirst)
                    isFirst = false;
                else
                    sb.Append(',');
                sb.Append(s.NfaId);
            }
            sb.Append(';');
            sb.Append(acceptState?.ToString() ?? "X");
            return sb.ToString();
        }

        /// <summary>
        /// The name of this state (created using <see cref="GetDfaName"/>). 
        /// </summary>
        public readonly string Name;
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
        public DfaState(ushort? smallestAcceptState, ICollection<NfaState> nfaStates, string name, int dfaId)
        {
            Name = name;
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
