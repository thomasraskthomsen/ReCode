using System;
using System.Collections.Generic;
using System.Linq;
using ReCode.Parsers.Generation;

namespace ReCode.Parsers.Grammatics
{
    public class PrecedenceGroup : IPrecedenceGroup
    {
        private Grammar _grammar;
        private readonly ushort? _precedence;

        public PrecedenceGroup(Grammar grammar, ushort? precedence)
        {
            _grammar = grammar;
            _precedence = precedence;
        }

        protected void SetGrammar(Grammar grammar)
        {
            _grammar = grammar;
        }

        public int this[string token]
        {
            get
            {
                var idx = Array.IndexOf(_grammar.Tokens, token);
                if (idx < 0) throw new ArgumentException($"Unknown token: '{token}'");
                return idx;
            }
        }

        public bool Matches(int left, params int[] right)
        {
            var id = _grammar.NewProductionId();
            var production = new Production(_grammar, id, _precedence, null, left, right);
            _grammar._productions.Add(production);
            return false;
        }

        public bool MatchesLeft(int left, params int[] right)
        {
            var id = _grammar.NewProductionId();
            var production = new Production(_grammar, id, null, LrDerivation.Left, left, right);
            _grammar._productions.Add(production);
            return false;
        }

        public bool MatchesRight(int left, params int[] right)
        {
            var id = _grammar.NewProductionId();
            var production = new Production(_grammar, id, null, LrDerivation.Right, left, right);
            _grammar._productions.Add(production);
            return false;
        }

        public bool Matches(string left, params string[] right)
        {
            var leftNum = _grammar[left];
            var rightNum = right.Select(name => _grammar[name]).ToArray();
            return Matches(leftNum, rightNum);
        }

        public bool MatchesLeft(string left, params string[] right)
        {
            var leftNum = _grammar[left];
            var rightNum = right.Select(name => _grammar[name]).ToArray();
            return MatchesLeft(leftNum, rightNum);
        }

        public bool MatchesRight(string left, params string[] right)
        {
            var leftNum = _grammar[left];
            var rightNum = right.Select(name => _grammar[name]).ToArray();
            return MatchesRight(leftNum, rightNum);
        }

        public bool Matches<T>(T left, params T[] right)
        {
            var leftStr = left.ToString();
            var rightStr = right.Select(obj => obj.ToString()).ToArray();
            return Matches(leftStr, rightStr);
        }

    }

    /// <summary>
    /// All of the information required to make a Parser
    /// </summary>
    public sealed class Grammar : PrecedenceGroup, IGrammar
    {
        internal readonly List<Production> _productions = new List<Production>();
        private int _nextProductionId;
        private ushort _nextPrecedence;

        public readonly string[] Tokens;
        public IReadOnlyList<Production> Productions => _productions;

        private Grammar(params string[] tokens) : base(null, null)
        {
            SetGrammar(this);
            var res = new List<string> { "<<ROOT>>", "END" };
            res.AddRange(tokens);
            Tokens = res.ToArray();
            Matches(0, 2); // default rule
        }

        public static Grammar FromTokens(params string[] tokens)
        {
            return new Grammar(tokens);
        }

        public IPrecedenceGroup WithPrecedenceGroup()
        {
            return new PrecedenceGroup(this, _nextPrecedence++);
        }

        internal int NewProductionId() { return _nextProductionId++; }
    }
}
