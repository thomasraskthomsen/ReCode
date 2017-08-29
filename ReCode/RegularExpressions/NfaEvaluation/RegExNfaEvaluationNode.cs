using System.Collections.Generic;
using System.Linq;
using ReCode.RegularExpressions.Transform;

namespace ReCode.RegularExpressions.NfaEvaluation
{
    public class RegExNfaEvaluationNode
    {
        private readonly NfaState[] _states;
        private int _numMembers;
        private readonly bool[] _memberFlags;
        private readonly int[] _members;
         
        public RegExNfaEvaluationNode(NfaState[] states)
        {
            _states = states;
            _memberFlags = new bool[_states.Length];
            _members = new int[_states.Length];
        }

        public void Reset()
        {
            for (var i = 0; i < _numMembers; i++)
                _memberFlags[_members[i]] = false;
            _numMembers = 0;
        }

        private bool Add(int id)
        {
            if (_memberFlags[id]) return false;
            _members[_numMembers++] = id;
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
            get
            {
                for (var i = 0; i < _numMembers; i++)
                    yield return _states[_members[i]];
            }
        }

        public bool IsEmpty => _numMembers == 0;

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
