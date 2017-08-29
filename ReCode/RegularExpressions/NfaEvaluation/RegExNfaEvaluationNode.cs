using System.Collections.Generic;
using System.Linq;
using ReCode.RegularExpressions.Transform;

namespace ReCode.RegularExpressions.NfaEvaluation
{
    public class RegExNfaEvaluationNode
    {
        private readonly NfaState[] _states;
        private readonly bool[] _memberFlags;
        private readonly List<int> _members;
         
        public RegExNfaEvaluationNode(NfaState[] states)
        {
            _states = states;
            _memberFlags = new bool[_states.Length];
            _members = new List<int>(_states.Length);
        }

        public void Reset()
        {
            foreach (var idx in _members)
                _memberFlags[idx] = false;
            _members.Clear();
        }

        private bool Add(int id)
        {
            if (_memberFlags[id]) return false;
            _members.Add(id);
            _memberFlags[id] = true;
            return true;
        }

        public void Apply(NfaState state)
        {
            var id = state.NfaId;
            if (!Add(id)) return;
            foreach (var eps in state.EpsilonStates)
                Add(eps.NfaId);
        }

        public IEnumerable<NfaState> NfaStates
        {
            get {
                return _members.Select(idx => _states[idx]);
            }
        }

        public bool IsEmpty => _members.Count == 0;

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
