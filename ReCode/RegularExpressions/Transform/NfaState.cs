using System;
using System.Collections.Generic;
using ReCode.RegularExpressions.Parsing.Nodes;

namespace ReCode.RegularExpressions.Transform
{
    /// <summary>
    /// Class that represents a node in the NFA graph.
    /// </summary>
    public class NfaState : IComparable<NfaState>
    {
        /// <summary>
        /// The overlapping list of NFA transitions from this state to another including epsilon transitions.
        /// </summary>
        public readonly List<KeyValuePair<RegExInputRange, NfaState>> Map = new List<KeyValuePair<RegExInputRange, NfaState>>(1);

        /// <summary>
        /// The set of NFA states that can be reached from this state using zero or more epsilon transitions.
        /// </summary>
        public NfaState[] EpsilonStates;
        //public readonly SortedSet<NfaState> EpsilonStates = new SortedSet<NfaState>(); 

        /// <summary>
        /// The NFA node id.
        /// </summary>
        public readonly int NfaId;

        /// <summary>
        /// The accept state. A non-zero value is used to mark a valid accept.
        /// </summary>
        public readonly ushort? AcceptState;

        /// <summary>
        /// Best reachable accept state.
        /// </summary>
        public ushort? BestReachableState;


        /// <summary>
        /// Constructs a new state with the specified id.
        /// </summary>
        /// <param name="nfaId">The id of this state.</param>
        /// <param name="acceptState">Acceptance criteria for this state.</param>
        public NfaState(int nfaId, ushort? acceptState = null)
        {
            NfaId = nfaId;
            AcceptState = acceptState;
        }

        int IComparable<NfaState>.CompareTo(NfaState other)
        {
            return NfaId.CompareTo(other.NfaId);
        }

        public override int GetHashCode()
        {
            return NfaId;
        }
    }
}
