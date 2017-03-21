using System;
using System.Collections.Generic;
using System.Text;
using ReCode.Parsers.Grammatics;

namespace ReCode.Parsers.Generation.Data
{
    /// <summary>
    /// A parser LR item.
    /// </summary>
    public class LrItem : IComparable<LrItem>
    {
        /// <summary>
        /// A string that uniquely identifices the item (generated using the <see cref="MakeId"/> functions).
        /// </summary>
        public readonly string Id;
        /// <summary>
        /// The underlying production.
        /// </summary>
        public readonly Production Production;
        /// <summary>
        /// The progress in the production.
        /// </summary>
        public readonly int ProductionProgress;
        /// <summary>
        /// The progress in the production.
        /// </summary>
        public readonly int LookAheadSymbol;


        /// <summary>
        /// Creates a new item from the specified parameters.
        /// </summary>
        /// <param name="production">The production</param>
        /// <param name="productionProgress">The production Right pattern progress.</param>
        /// <param name="lookAheadSymbol">The look-ahead symbol</param>
        public LrItem(Production production, int productionProgress, int lookAheadSymbol)
        {
            Production = production;
            ProductionProgress = productionProgress;
            LookAheadSymbol = lookAheadSymbol;
            Id = MakeId(production, productionProgress, lookAheadSymbol);
        }

        /// <summary>
        /// Specifies if the progress can be incremented further.
        /// </summary>
        public bool CanIncrement => ProductionProgress < Production.Right.Count;

        /// <summary>
        /// Specifies the next token in the progress.
        /// </summary>
        public int NextToken => Production.Right[ProductionProgress];

        /// <summary>
        /// Gets the sequence for remaining tokens from the current progress followed by tok.
        /// </summary>
        public IEnumerable<int> NextNextTokensAndThen(int tok)
        {
            for (var i = ProductionProgress + 1; i < Production.Right.Count; i++)
                yield return Production.Right[i];
            yield return tok;
        } 

        /// <summary>
        /// Creates an item id from the specified parameters.
        /// </summary>
        /// <param name="production">The production</param>
        /// <param name="productionProgress">The production progress</param>
        /// <param name="lookAheadSymbol">The look-ahead symbol</param>
        /// <returns></returns>
        public static string MakeId(Production production, int productionProgress, int lookAheadSymbol)
        {
            return $"{production.Id}[{productionProgress}],{lookAheadSymbol}";
        }

        /// <summary>
        /// Used to establish a unique sorting order of items.
        /// </summary>
        public int CompareTo(LrItem other)
        {
            return string.Compare(Id, other.Id, StringComparison.Ordinal);
        }

        /// <summary>
        /// A humanly readable repesentation of the item.
        /// </summary>
        public override string ToString()
        {
            const bool addSpaces = false;
            var tokens = Production.Grammar.Tokens;
            var sb = new StringBuilder();
            sb.Append(tokens[Production.Left]);
            sb.Append("=>");
            if (0 == ProductionProgress)
                sb.Append(".");
            else if(addSpaces)
                sb.Append(' ');
            for (var i = 0; i < Production.Right.Count; i++)
            {
                sb.Append(tokens[Production.Right[i]]);
                if (i+1 == ProductionProgress)
                    sb.Append(".");
                else if(addSpaces)
                    sb.Append(' ');
            }
            sb.Append(",");
            sb.Append(Production.Grammar.Tokens[LookAheadSymbol]);
            return sb.ToString();
        }
    }
}
