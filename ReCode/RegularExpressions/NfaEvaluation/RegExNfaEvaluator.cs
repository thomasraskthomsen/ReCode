using System.Collections.Generic;
using System.Linq;
using ReCode.RegularExpressions.Transform;

namespace ReCode.RegularExpressions.NfaEvaluation
{
    public class RegExNfaEvaluator
    {
        private readonly NfaState[] _states;

        public RegExNfaEvaluator(IReadOnlyList<NfaState> states)
        {
            _states = states.ToArray();
        }

        private static void Map(char c, ref RegExNfaEvaluationNode current, ref RegExNfaEvaluationNode spare)
        {
            foreach (var nfa in current.NfaStates)
            {
                foreach (var range in nfa.Map)
                {
                    if (range.Key.Min > c)
                        break; // no more relevant values
                    if (c <= range.Key.Max)
                        spare.Apply(range.Value); // c is contained in  [Key.Min;Key.Max]
                }
            }
            var tmp = current;
            current = spare;
            spare = tmp;
            spare.Reset();
        }

        public unsafe KeyValuePair<ushort, int>? Match(string str)
        {
            ushort? bestAccept = null;
            int bestIndex = 0;

            fixed (char* letters = str)
            {
                var current = new RegExNfaEvaluationNode(_states);
                var spare = new RegExNfaEvaluationNode(_states);

                current.Apply(_states[0]);

                var len = str.Length;
                for (var i = 0; i < len; i++)
                {
                    var c = letters[i];
                    Map(c, ref current, ref spare);
                    if (current.IsEmpty) break;
                    var best = current.GetBestAcceptState();
                    if (best.HasValue)
                    {
                        if (bestAccept < best.Value)
                            continue;
                        bestAccept = best.Value;
                        bestIndex = i;
                    }
                }

            }
            if (bestAccept.HasValue)
                return new KeyValuePair<ushort, int>(bestAccept.Value, bestIndex + 1);
            return null;
        }
    }
}
