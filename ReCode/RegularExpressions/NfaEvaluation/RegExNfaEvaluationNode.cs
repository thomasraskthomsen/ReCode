using System.Collections.Generic;
using System.Diagnostics.Contracts;
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
        private ushort? _bestAcceptState;
         
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
            _bestAcceptState = null;
        }

        private bool Add(NfaState state)
        {
            var id = state.NfaId;
            if (_memberFlags[id]) return false;
            _members[_numMembers++] = id;
            _memberFlags[id] = true;
            if (state.AcceptState.HasValue)
            {
                var proposal = state.AcceptState.Value;
                if (!_bestAcceptState.HasValue || (proposal < _bestAcceptState.Value))
                    _bestAcceptState = proposal;
            }
            return true;
        }

        public void Apply(NfaState state)
        {
            if (!Add(state)) return;
            var epsilonStates = state.EpsilonStates;
            var cnt = epsilonStates.Length;
            for(var i=0;i<cnt;i++)
                Add(epsilonStates[i]);
        }

        public void MapTo(char c, RegExNfaEvaluationNode target)
        {
            for (var i = 0; i < _numMembers; i++)
            {
                var nfa = _states[_members[i]];
                var nfaMap = nfa.Map;
                var cnt = nfaMap.Count;
                for (var j = 0; j < cnt; j++)
                {
                    var range = nfaMap[j];
                    if (range.Key.Min <= c && c <= range.Key.Max)
                        target.Apply(range.Value); // c is contained in  [Key.Min;Key.Max]
                }
            }
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

        public ushort? BestAcceptState => _bestAcceptState;

    }
}
