using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReCode.RegularExpressions.Parsing.Nodes
{
    public class RegExNodeUnion : RegExNode
    {
        public readonly List<RegExNode> Expressions = new List<RegExNode>();

        private void Add(RegExNode node)
        {
            if (node.RegExType == RegExType.Union)
                Expressions.AddRange(((RegExNodeUnion)node).Expressions);
            else
                Expressions.Add(node);
        }

        private RegExNodeUnion(RegExNode first, RegExNode second) : base(RegExType.Union)
        {
            Add(first);
            Add(second);
        }

        public override void Print(StringBuilder sb, string indent1, string indent2)
        {
            sb.AppendLine($"{indent1}Union");
            for (var i = 0; i < Expressions.Count; i++)
            {
                var exp = Expressions[i];
                if(i+1 < Expressions.Count)
                    exp.Print(sb, indent2 + SubDash, indent2 + SubBar);
                else
                    exp.Print(sb, indent2 + SubDash, indent2 + SubSpaces);
            }
        }

        public static RegExNode Of(RegExNode first, RegExNode second)
        {
            if (first.RegExType == RegExType.Ranges && second.RegExType == RegExType.Ranges)
            {
                var r1 = ((RegExNodeRanges) first).Ranges;
                var r2 = ((RegExNodeRanges) second).Ranges;
                return new RegExNodeRanges(new[] { r1, r2 }.SelectMany(r => r).ToArray());
            }
            return new RegExNodeUnion(first, second);
        }
    }
}
