using System.Collections.Generic;
using System.Text;
using ReCode.Parsers.Generation;

namespace ReCode.Parsers.Grammatics
{
    /// <summary>
    /// A grammatical production
    /// </summary>
    public class Production
    {
        public readonly Grammar Grammar;
        public readonly int Id;
        public readonly int Left;
        public readonly IReadOnlyList<int> Right;
        public readonly ushort? Precedence;
        public readonly LrDerivation? Derivation;

        internal Production(Grammar grammar, int id, ushort? precedence, LrDerivation? derivation, int left, params int[] right)
        {
            Grammar = grammar;
            Id = id;
            Precedence = precedence;
            Derivation = derivation;
            Left = left;
            Right = right;
        }

        public override string ToString()
        {
            var sb= new StringBuilder();
            sb.Append(Grammar.Tokens[Left]);
            sb.Append(" =>");
            foreach (var r in Right)
            {
                sb.Append(" ");
                sb.Append(Grammar.Tokens[r]);
            }
            return sb.ToString();
        }
    }
}