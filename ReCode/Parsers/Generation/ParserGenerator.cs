//#define DIAGNOSTICS
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using ReCode.Parsers.Generation.Data;
using ReCode.Parsers.Grammatics;

namespace ReCode.Parsers.Generation
{
    /// <summary>
    /// Class for generating a parser.
    /// </summary>
    public class ParserGenerator
    {
        internal readonly Grammar Grammar;
        internal readonly HashSet<int> Terminals = new HashSet<int>();
        internal readonly HashSet<int> NonTerminals = new HashSet<int>();
        internal readonly LrItemCollection LrItems = new LrItemCollection();
        internal readonly LrStateCollection LrStates = new LrStateCollection();
        internal HashSet<int>[] FirstSets;

        private void PopulateTerminalsAndNonTerminals()
        {
            foreach (var p in Grammar.Productions)
                NonTerminals.Add(p.Left);
            foreach (var p in Grammar.Productions)
                foreach (var s in p.Right)
                    if (!NonTerminals.Contains(s))
                        Terminals.Add(s);
            Terminals.Add(1);
        }

        /// <summary>
        /// Given a collection of items, expand it with the collection of items resulting from replacing the next symbol (if any) 
        /// with a production whose left symbol is said symbol.
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        private SortedSet<LrItem> ComputeClosure(SortedSet<LrItem> items)
        {
#if DIAGNOSTICS
            Console.WriteLine("## Calculating closure for");
            foreach(var item in items)
                Console.WriteLine($"  {item}");
#endif
            var closed = new SortedSet<LrItem>();
            var open = new Queue<LrItem>();
            foreach(var item in items)
                open.Enqueue(item);

            while (open.Count > 0)
            {
                var item = open.Dequeue();
                closed.Add(item);
                if (!item.CanIncrement) continue;
                var nextToken = item.NextToken;
                foreach (var p in Grammar.Productions)
                {
                    if (p.Left != nextToken) continue;
                    if (!item.CanIncrement) continue;
                    var firstSet = new HashSet<int>();
                    foreach (var sym in item.NextNextTokensAndThen(item.LookAheadSymbol))
                    {
                        var bLookAhead = false;
                        foreach (var firstSym in FirstSets[sym])
                        {
                            if (firstSym == -1)
                                bLookAhead = true;
                            firstSet.Add(firstSym);
                        }
                        if (!bLookAhead)
                            break;
                    }

                    foreach (var sym in firstSet)
                    {
                        var newItem = LrItems.GetOrAdd(p, 0, sym);
                        if (!open.Contains(newItem) && !closed.Contains(newItem))
                            open.Enqueue(newItem);
                    }

                }
            }
#if DIAGNOSTICS
            Console.WriteLine("#  Resulting items:");
            foreach (var item in closed)
                Console.WriteLine($"  {item}");
#endif
            return closed;
        }

        /// <summary>
        /// Given the set of items in state, calculate for each token the closed set of items that can result from processing said token.
        /// </summary>
        /// <param name="state">The staring state.</param>
        /// <param name="token">The token to process.</param>
        /// <param name="bAdded">Return true if new state was added.</param>
        /// <param name="precedence">Return the precedence of the goto.</param>
        /// <returns>Returns the resulting LrState or null of unable to process the specified token on any of the state items.</returns>
        private LrState GotoLr(LrState state, int token, out bool bAdded, out ushort? precedence)
        {
            ushort? prec = null;
            var targetItems = new SortedSet<LrItem>();
            foreach (var item in state.Items)
            {
                if(item.CanIncrement && (item.NextToken == token))
                {
                    var newItem = LrItems.GetOrAdd(item.Production, item.ProductionProgress + 1, item.LookAheadSymbol);
                    targetItems.Add(newItem);
                    if (!prec.HasValue)
                        prec = item.Production.Precedence;
                    else if (item.Production.Precedence > prec)
                        prec = item.Production.Precedence;
                }
            }
            if (targetItems.Count == 0)
            {
                bAdded = false;
                precedence = null;
                return null;
            }
            var targetItemsWithClosure = ComputeClosure(targetItems);
            var targetState = LrStates.GetOrAdd(targetItemsWithClosure, out bAdded);
            precedence = prec;
            return targetState;
        }

        /// <summary>
        /// Compute the collection of Lr states by recursively applying productions from root state until no new states are added.
        /// </summary>
        private void CreateLrItemsAndStates()
        {
            var rootProduction = Grammar.Productions[0];
            var rootItem = LrItems.GetOrAdd(rootProduction, 0, 1);
            var rootState = LrStates.GetOrAdd(ComputeClosure(new SortedSet<LrItem> {rootItem}));   
                     
            var open = new Queue<LrState>();
            open.Enqueue(rootState);

            while (open.Count > 0)
            {
                var state = open.Dequeue();
                var gotos = new LrState[Grammar.Tokens.Length];
                var gotoPrecedences = new ushort?[Grammar.Tokens.Length];
                foreach (var token in Enumerable.Range(0, Grammar.Tokens.Length))
                {
                    bool bAdded;
                    ushort? precedence;
                    var gotoState = GotoLr(state, token, out bAdded, out precedence);
                    gotos[token] = gotoState;
                    gotoPrecedences[token] = precedence;
                    if (bAdded)
                        open.Enqueue(gotoState);
                }
                state.Gotos = gotos;
                state.GotoPrecedences = gotoPrecedences;
            }
        }

        /// <summary>
        /// Tries to set the specified action in the action array. Throws an exception if an action was already set for the specified index.
        /// </summary>
        /// <param name="actions">The array in which to set the action.</param>
        /// <param name="index">The index to change.</param>
        /// <param name="indexActions">The possible actions to take set at the specified index.</param>
        private static void SetAction(LrAction[] actions, int index, IReadOnlyList<LrAction> indexActions)
        {
            if (indexActions.Count == 0) return;
            if (indexActions.Count == 1)
            {
                actions[index] = indexActions[0];
                return;
            }
#if DIAGNOSTICS
            Console.WriteLine("Trying to resolve conflict for the following actions:");
            foreach(var act in indexActions)
                Console.WriteLine($"  {act}");
#endif
            var shiftActions = indexActions.Where(a => a.Action == LrActionType.Shift).ToList();
            var reduceActions = indexActions.Where(a => a.Action == LrActionType.Reduce).ToList();
            if (shiftActions.Count == 1 && reduceActions.Count == 1)
            {
                var reduceAction = reduceActions[0];
                var shiftAction = shiftActions[0];
                switch (reduceAction.Production.Derivation)
                {
                    case LrDerivation.Left:
                        actions[index] = reduceAction;
                        return;
                    case LrDerivation.Right:
                        actions[index] = shiftAction;
                        return;
                }
            }

            throw new Exception("Conflicts on " + string.Join(" ", indexActions.Select(a => a.ToString()).ToArray()));
        }

        /// <summary>
        /// Populates the actions for each state and symbol combination.
        /// </summary>
        private void FillActions()
        {
            var acceptProduction = Grammar.Productions[0];
            foreach (var state in LrStates)
            {
                try
                {
                    var actions = new LrAction[state.Gotos.Count];

                    for(var sym =0;sym<Grammar.Tokens.Length; sym++)
                    {
                        var possibleActions = new ActionSet();
                         
                        var target = state.Gotos[sym];
                        if (target != null)
                        {
                            if (Terminals.Contains(sym))
                                possibleActions.Add(LrAction.Shift(target, state.GotoPrecedences[sym]));
                            if (NonTerminals.Contains(sym))
                                possibleActions.Add(LrAction.Goto(target, state.GotoPrecedences[sym]));
                        }
                        foreach (var item in state.Items)
                        {
                            if (item.LookAheadSymbol != sym) continue;
                            if (item.CanIncrement == false)
                            {
                                if (item.Production == acceptProduction && item.ProductionProgress == 1)
                                    possibleActions.Add(LrAction.Accept());
                                else
                                    possibleActions.Add(LrAction.Reduce(item.Production, item.Production.Precedence));

                            }
                        }
                        SetAction(actions, sym, possibleActions);
                    }
                    state.Actions = actions;
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error processing state: {state}", ex);
                }
            }
        }

        public void WriteStateInfo(int stateIndex, Action<string> writeLine)
        {
            var state = LrStates.First(s => s.Index == stateIndex);
                writeLine(state.ToString());
            for (var i = 0; i < Grammar.Tokens.Length; i++)
            {
                var action = state.Actions[i];
                if (action != null)
                {
                    writeLine($"  {Grammar.Tokens[i]}: {action}");
                }
            }
        }

        /// <summary>
        /// Computes the set of terminals that can appear as the first terminal when expanding a symbol.
        /// </summary>
        private void ComputeFirstSets()
        {
            /*
             * If X is a terminal then First(X) is just X!
             * If there is a Production X → ε then add ε to first(X)
             * If there is a Production X → Y1Y2..Yk then add first(Y1Y2..Yk) to first(X)
             * First(Y1Y2..Yk) is either
             *      First(Y1) (if First(Y1) doesn't contain ε)
             *      OR (if First(Y1) does contain ε) then First (Y1Y2..Yk) is everything in First(Y1) <except for ε > as well as everything in First(Y2..Yk)
             *      If First(Y1) First(Y2)..First(Yk) all contain ε then add ε to First(Y1Y2..Yk) as well.
             */
            var numSymbols = Grammar.Tokens.Length;
            FirstSets = new HashSet<int>[numSymbols];

            for (var sym = 0; sym < numSymbols; sym++)
            {
                FirstSets[sym] = new HashSet<int>();
                if (Terminals.Contains(sym))
                    FirstSets[sym].Add(sym);
            }

            foreach (var p in Grammar.Productions)
                if (p.Right.Count == 0)
                    FirstSets[p.Left].Add(-1);

            bool changed;
            do
            {
                changed = false;
                foreach (var production in Grammar.Productions)
                {
                    foreach (var sym in production.Right)
                    {
                        var bLookAhead = false;
                        foreach (var firstSym in FirstSets[sym])
                        {
                            if (firstSym == -1)
                                bLookAhead = true;
                            else if (FirstSets[production.Left].Add(firstSym))
                                changed = true;
                        }
                        if (!bLookAhead)
                            break;
                    }
                }
            }
            while (changed);

#if DIAGNOSTICS
            Console.WriteLine();
            Console.WriteLine("## First Sets");
            for (var sym = 0; sym < FirstSets.Length; sym++)
            {
                Console.Write($"  First Sets {Grammar.Tokens[sym]}:");
                foreach (var firstSym in FirstSets[sym])
                    Console.Write($" {Grammar.Tokens[firstSym]}");
                Console.WriteLine();
            }
#endif
        }

        public ParserGenerator(Grammar grammar)
        {
            Grammar = grammar;
        }

        public ParserTableEntry[][] GenerateParserTable()
        {
            PopulateTerminalsAndNonTerminals();
            ComputeFirstSets();
            CreateLrItemsAndStates();
#if DIAGNOSTICS
            Console.WriteLine();
            Console.WriteLine("## Items:");
            foreach (var item in LrItems)
                Console.WriteLine(item);
            Console.WriteLine();
            Console.WriteLine("## States:");
            foreach (var state in LrStates)
            {
                Console.WriteLine(state);
                for (var i = 0; i < Grammar.Tokens.Length; i++)
                {
                    var target = state.Gotos[i];
                    if (target != null)
                        Console.WriteLine($"  {Grammar.Tokens[i]}: {target})");
                }
            }
#endif
            FillActions();

#if DIAGNOSTICS
            Console.WriteLine("Actions:");
            foreach (var state in LrStates)
            {
                Console.WriteLine(state);
                for (var i = 0; i < Grammar.Tokens.Length; i++)
                {
                    var action = state.Actions[i];
                    if (action != null)
                    {
                        Console.WriteLine($"  {Grammar.Tokens[i]}: {action}");
                    }
                }
            }
#endif
            var res = new ParserTableEntry[LrStates.Count][];            
            foreach (var state in LrStates)
            {
                var entries = new ParserTableEntry[Grammar.Tokens.Length];
                res[state.Index] = entries;
                var actions = state.Actions;
                for (var i = 0; i < Grammar.Tokens.Length; i++)
                {
                    var action = actions[i];
                    if (action != null)
                    {
                        switch (action.Action)
                        {
                            case LrActionType.Accept:
                                entries[i] = new ParserTableEntry { Action = LrActionType.Accept};
                                break;
                            case LrActionType.Shift:
                                entries[i] = new ParserTableEntry { Action = LrActionType.Shift, Target = action.Target.Index };
                                break;
                            case LrActionType.Reduce:
                                var production = actions[i].Production;
                                entries[i] = new ParserTableEntry { Action = LrActionType.Reduce, Target = production.Id, ArgCount = production.Right.Count, TargetTokenType = production.Left };
                                break;
                            case LrActionType.Goto:
                                entries[i] = new ParserTableEntry { Action = LrActionType.Goto, Target = action.Target.Index };
                                break;
                        }
                    }
                }
            }
            return res;
        }
    }
}
