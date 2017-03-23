//#define DIAGNOSTICS
using System;
using System.Collections.Generic;
using System.Linq;
using ReCode.RegularExpressions.Code;
using ReCode.RegularExpressions.Evaluation;
using ReCode.RegularExpressions.Parsing.Nodes;

namespace ReCode.RegularExpressions.Transform
{
    /// <summary>
    /// Class for building deterministic regular expressions.
    /// </summary>
    public class Builder
    {
        private int _nextNfaStateNumber;
        private int _nextDfaStateNumber;
        private RegExNode _root;
        private readonly List<NfaState> _nfaStates = new List<NfaState>();
        private readonly List<DfaState> _dfaStates = new List<DfaState>();
        private readonly Dictionary<string, DfaState> _knownDfaStates = new Dictionary<string, DfaState>();
        private readonly Queue<DfaState> _pendingStates = new Queue<DfaState>();

        private NfaState NewNfaState(ushort? acceptState = null)
        {
            var res = new NfaState(_nextNfaStateNumber, acceptState);
            _nfaStates.Add(res);
            ++_nextNfaStateNumber;
            return res;
        }

        private DfaState NewDfaState(ushort? smallestAcceptState, ICollection<NfaState> nfaStates, string id)
        {
            var res = new DfaState(smallestAcceptState, nfaStates, id, _nextDfaStateNumber);
            ++_nextDfaStateNumber;
            _dfaStates.Add(res);
            return res;
        }

        /// <summary>
        /// Builds a DFA from the specified expression tree.
        /// </summary>
        /// <param name="root">The expresion tree to transform into a DFA.</param>
        /// <param name="defaultAcceptState">A default accept state can be specified. For instance in case the pression does not have one.</param>
        /// <returns></returns>
        public RegExEvaluationNode Build(RegExNode root, ushort? defaultAcceptState = null)
        {
            _root = root;
            // first build NFA
            var start = NewNfaState();
            var end = NewNfaState(defaultAcceptState);
            Apply(root, start, end);
#if DIAGNOSTICS
            Console.WriteLine("Created {0} new states.", _nextNfaStateNumber);
            for (var i = 0; i < _nfaStates.Count; i++)
            {
                var state = _nfaStates[i];
                Console.Write("{0,3}:\t", i);
                foreach (var pair in state.Map)
                {
                    var str = $"{pair.Key}->N{pair.Value.NfaId}";
                    Console.Write("{0,-15}", str);
                }
                Console.WriteLine();
            }
#endif

            // add to each NFA state the closure for epsilon moves
            CalculateEpsilonState();
            CalcuateBestReachableAcceptStates();

            // transform NFA to DFA, starting 'start' and what can be reached from there via epsilon steps.
            var initial = new HashSet<NfaState> { start };
            initial.UnionWith(start.EpsilonStates);
            var acceptState = GetSmallestAcceptState(null, initial);
            var stateId = DfaState.GetDfaName(acceptState, initial);
            var dfaBegin = NewDfaState(acceptState, initial, stateId);
            _knownDfaStates.Add(stateId, dfaBegin);
            _pendingStates.Enqueue(dfaBegin);
            while (_pendingStates.Count > 0)
            {
                var state = _pendingStates.Dequeue();
                ProcessDfaState(state);
            }
            return _dfaStates[0].EvaluationNode;
        }

        private bool IsAcceptStateImprovement(ushort? from, ushort? to)
        {
            if (!from.HasValue)
                return to.HasValue;
            if (!to.HasValue)
                return false;
            return from.Value >= to.Value;
        }

        private ushort? GetSmallestAcceptState(ushort? baseState, ICollection<NfaState> set)
        {
            return new[] { set.SelectMany(s => s.EpsilonStates).Min(s => s.AcceptState), baseState }.Min();
        }

        private void CalcuateBestReachableAcceptStates()
        {
            foreach (var set in _nfaStates)
            {
                var knownStates = new HashSet<NfaState>();
                var pending = new Queue<NfaState>();
                knownStates.Add(set);
                pending.Enqueue(set);

                ushort? res = null;
                while (pending.Count > 0)
                {
                    var state = pending.Dequeue();
                    if (IsAcceptStateImprovement(res, state.AcceptState))
                        res = state.AcceptState;
                    foreach (var nextState in state.Map)
                    {
                        //if (nextState.Value.BestReachableState.HasValue)
                        //{
                        //    if (IsAcceptStateImprovement(res, state.AcceptState))
                        //        res = state.AcceptState;
                        //}
                        //else
                        {
                            if (!knownStates.Contains(nextState.Value))
                            {
                                knownStates.Add(nextState.Value);
                                pending.Enqueue(nextState.Value);
                            }
                        }
                    }
                }
                set.BestReachableState = res;
            }
        }

        private void ProcessDfaState(DfaState state)
        {
#if DIAGNOSTICS
            Console.WriteLine("NODE D{0} (name={1}):", state.DfaId, state.Name);
#endif
            state.EvaluationNode.AcceptAction = GetSmallestAcceptState(null, state.NfaStates);      
            foreach (var nfa in state.NfaStates)
            {
                foreach (var pair in nfa.Map)
                {
                    foreach (var epsState in pair.Value.EpsilonStates)
                    {
                        if(IsAcceptStateImprovement(state.SmallestAcceptState, epsState.BestReachableState))
                            state.NfaMap.Add(new KeyValuePair<RegExInputRange, NfaState>(pair.Key, epsState));
                    }
                }
            }


            foreach (var pair in state.GetGroupedTransition())
            {
                var nextAcceptState = GetSmallestAcceptState(state.SmallestAcceptState, pair.Value);
                var name = DfaState.GetDfaName(nextAcceptState, pair.Value);
                DfaState dfa;
                if (!_knownDfaStates.TryGetValue(name, out dfa))
                {
                    dfa = NewDfaState(nextAcceptState, pair.Value, name);
                    _pendingStates.Enqueue(dfa);
                    _knownDfaStates.Add(name, dfa);
                }
                state.EvaluationNode.Add(pair.Key, dfa.EvaluationNode);
#if DIAGNOSTICS
                Console.WriteLine("\t{0}->D{1} ({2})", pair.Key, dfa.DfaId, dfa.Name);
#endif
            }
        }


        private static void Transition(NfaState from, NfaState to, RegExInputRange via)
        {
            from.Map.Add(new KeyValuePair<RegExInputRange, NfaState>(via, to));
        }

        private void Apply(RegExNode regExNode, NfaState begin, NfaState end)
        {
            switch (regExNode.RegExType)
            {
                case RegExType.Blank:
                    Transition(begin, end, RegExInputRange.EPS);
                    break;
                case RegExType.Union:
                    var union = (RegExNodeUnion)regExNode;
                    foreach (var exp in union.Expressions)
                        Apply(exp, begin, end);
                    break;
                case RegExType.Concat:
                    var concat = (RegExNodeConcat)regExNode;
                    var lastConcat = begin;
                    for (var i = 0; i < concat.Expressions.Count; i++)
                    {
                        var tmp = (i + 1) < concat.Expressions.Count ? NewNfaState() : end;
                        Apply(concat.Expressions[i], lastConcat, tmp);
                        lastConcat = tmp;
                    }
                    break;
                case RegExType.Ranges:
                    var prim = (RegExNodeRanges)regExNode;
                    foreach (var range in prim.Ranges)
                        Transition(begin, end, range);
                    break;
                case RegExType.Sequence:
                    var seq = (RegExNodeSequence)regExNode;
                    var lastSeq = begin;
                    switch (seq.Casing)
                    {
                        case RegExCasing.Insensitive:
                            for (var i = 0; i < seq.Sequence.Length; i++)
                            {
                                var tmp = (i + 1 < seq.Sequence.Length) ? NewNfaState() : end;
                                var c1 = char.ToLowerInvariant(seq.Sequence[i]);
                                var c2 = char.ToUpperInvariant(c1);
                                Transition(lastSeq, tmp, new RegExInputRange(c1));
                                if(c2!=c1) Transition(lastSeq, tmp, new RegExInputRange(c2));
                                lastSeq = tmp;
                            }
                            break;
                        case RegExCasing.Sensitive:
                            for (var i = 0; i < seq.Sequence.Length; i++)
                            {
                                var tmp = (i + 1 < seq.Sequence.Length) ? NewNfaState() : end;
                                Transition(lastSeq, tmp, new RegExInputRange(seq.Sequence[i]));
                                lastSeq = tmp;
                            }
                            break;
                    }
                    break;
                case RegExType.Repeat:
                    var rep = (RegExNodeRepeat)regExNode;
                    switch (rep.RegExRepeatType)
                    {
                        case RegExRepeatType.ZeroOrOne:
                            Apply(rep.Expression, begin, end);
                            Transition(begin, end, RegExInputRange.EPS);
                            break;
                        case RegExRepeatType.ZeroOrMore:
                            {
                                var s1 = NewNfaState();
                                var s2 = NewNfaState();
                                Transition(begin, end, RegExInputRange.EPS);
                                Transition(begin, s1, RegExInputRange.EPS);
                                Apply(rep.Expression, s1, s2);
                                Transition(s2, s1, RegExInputRange.EPS);
                                Transition(s2, end, RegExInputRange.EPS);
                            }
                            break;
                        case RegExRepeatType.OneOrMore:
                            {
                                var s1 = NewNfaState();
                                var s2 = NewNfaState();
                                Transition(begin, s1, RegExInputRange.EPS);
                                Apply(rep.Expression, s1, s2);
                                Transition(s2, s1, RegExInputRange.EPS);
                                Transition(s2, end, RegExInputRange.EPS);
                            }
                            break;
                    }
                    break;
                case RegExType.Except:
                    var except = (RegExNodeExcept) regExNode;
                    break;

                case RegExType.Accept:
                    // Add an accept state.
                    var accept = (RegExNodeAccept)regExNode;
                    var term = NewNfaState(accept.AcceptState);
                    Apply(accept.Expression, begin, term);
                    Transition(term, end, RegExInputRange.EPS);
                    break;
            }
        }

        private void GetEpsilonMoves(SortedSet<NfaState> stateCollection, NfaState currentState)
        {
            foreach (var pair in currentState.Map)
            {
                if (pair.Key != RegExInputRange.EPS)
                    continue;
                var state = pair.Value;
                if (stateCollection.Contains(state))
                    continue;
                stateCollection.Add(state);
                GetEpsilonMoves(stateCollection, state);
            }
        }

        private void CalculateEpsilonState()
        {
            foreach (var state in _nfaStates)
            {
                state.EpsilonStates.Add(state);
                GetEpsilonMoves(state.EpsilonStates, state);
            }
        }

        /// <summary>
        /// Generate C# code for matching the DFA.
        /// </summary>
        /// <param name="indent"></param>
        /// <returns></returns>
        public string Generate(string indent, ReCodeGenerator generator)
        {
            var scope = new GeneratorScope(generator, indent);
            scope.WriteLine("/*");
            _root.Print(generator.StringBuilder, scope.Indentation + " * ", scope.Indentation + " * ");
            scope.WriteLine(" */");
            foreach (var state in _dfaStates)
                GenerateCode(scope, state.EvaluationNode);
            return generator.StringBuilder.ToString();
        }

        private void SubGenerate(GeneratorScope scope, IReadOnlyList<KeyValuePair<uint, RegExEvaluationNode>> nodes, int a, int b, RegExEvaluationNode target, ushort? currentState)
        {
            if (a > b)
            {
                scope.Goto(target == null ? scope.AcceptLabel(currentState) : scope.StateLabel(target.Id));
            }
            else
            {
                var m = (a + b) / 2;
                var pair = nodes[m];
                scope.IfBegin(pair.Key, m - a);
                SubGenerate(scope.Indent(), nodes, a, m-1, pair.Value, currentState);
                scope.IfEnd(m - a);
                SubGenerate(scope, nodes, m+1, b, target, currentState);
            }
        }

        private bool NodeIsReferenced(RegExEvaluationNode node)
        {
            return _dfaStates.Exists(s => s.EvaluationNode.Values.Contains(node));
        }

        private void GenerateCode(GeneratorScope scope, RegExEvaluationNode node)
        {
            var dfa = _dfaStates[node.Id];
            var currentState = dfa.SmallestAcceptState;
#if DIAGNOSTICS
            Console.WriteLine($"Code generation for of state {dfa.DfaId}:");
            foreach (var range in node)
                Console.WriteLine($" range {range.Key.Min}-{range.Key.Max} {range.Value?.Id} {range.Value?.AcceptAction}");
#endif
            var points = new SortedDictionary<uint, RegExEvaluationNode>();

            RegExEvaluationNode lastValue = null;
            uint lastSuccessor = 0; 
            foreach (var pair in node)
            {
                if ((pair.Value == lastValue) && (lastSuccessor == pair.Key.Min))
                    points.Remove(lastSuccessor);
                else if (lastSuccessor < pair.Key.Min)
                    points.Add(pair.Key.Min, null);
                lastSuccessor = pair.Key.Max + 1; 
                points.Add(lastSuccessor, pair.Value);
                lastValue = pair.Value;
            }
            if (lastSuccessor == char.MaxValue + 1)
                points.Remove(lastSuccessor); 
            else
                lastValue = null;

#if DIAGNOSTICS
            Console.WriteLine("Points:");
            foreach(var pair in points)
                Console.WriteLine(" point {0} -> {1}", pair.Key, pair.Value != null ? $"{pair.Value.Id} ({pair.Value.AcceptAction})" : string.Empty);
#endif

            var accept = node.AcceptAction.HasValue ? $" (accepts to {node.AcceptAction.Value})" : string.Empty;
            scope.WriteLine("/*");
            scope.WriteLine(" * DFA STATE {0}{1}", node.Id, accept);
            foreach (var map in node)
                scope.WriteLine(" * {0} -> {1}", map.Key, map.Value.Id);
            scope.WriteLine(" */");
            if ((node.Id > 0) || NodeIsReferenced(node))
                scope.SetLabel(scope.StateLabel(node.Id));
            if (node.AcceptAction.HasValue && !(node.AcceptAction > currentState))
            {
                scope.MarkPos();
                if(node.Count>0)
                    scope.IncrementPos(scope.AcceptLabel(node.AcceptAction), node.Id == 0);
            }
            else
            {
                if (node.Count > 0)
                    scope.IncrementPos(scope.AcceptLabel(currentState), node.Id == 0);
            }
            var pointList = points.ToList();
            if((pointList.Count == 0) && (node.Count == 0))
                scope.Goto(scope.AcceptLabel(currentState));
            else
                SubGenerate(scope, pointList, 0, points.Count-1, lastValue, currentState);
        }

    }
}
