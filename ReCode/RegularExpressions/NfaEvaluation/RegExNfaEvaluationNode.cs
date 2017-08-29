using System.Collections.Generic;
using ReCode.RegularExpressions.Transform;

namespace ReCode.RegularExpressions.NfaEvaluation
{
    public class RegExNfaEvaluationNode
    {
        public readonly HashSet<NfaState> NfaStates = new HashSet<NfaState>();

        public void Reset()
        {
            NfaStates.Clear();
        }

        public void Apply(NfaState state)
        {
            if (NfaStates.Add(state))
                NfaStates.UnionWith(state.EpsilonStates);
        }

        public bool IsEmpty => NfaStates.Count == 0;

        public ushort? GetBestAcceptState() {
            ushort? best = null;
            foreach (var state in NfaStates)
            {
                if (state.AcceptState.HasValue)
                {
                    if (best < state.AcceptState.Value)
                        continue;
                    best = state.AcceptState;
                }
            }
            return best;
        }
    }
}
