using System.Collections.Generic;
using System.Text;

namespace ReCode.RegularExpressions.Parsing.Nodes
{
    public class RegExNodeConcat : RegExNode
    {
        public readonly List<RegExNode> Expressions = new List<RegExNode>();

        private void Add(RegExNode node)
        {
            if (node.RegExType == RegExType.Concat)
                Expressions.AddRange(((RegExNodeConcat)node).Expressions);
            else
                Expressions.Add(node);
        }

        public RegExNodeConcat(RegExNode left, RegExNode right) : base(RegExType.Concat)
        {
            Add(left);
            Add(right);
        }

        public override void Print(StringBuilder sb, string indent1, string indent2)
        {
            sb.AppendLine($"{indent1}Concat");
            for (var i = 0; i < Expressions.Count; i++)
            {
                var exp = Expressions[i];
                if (i + 1 < Expressions.Count)
                    exp.Print(sb, indent2 + SubDash, indent2 + SubBar);
                else
                    exp.Print(sb, indent2 + SubDash, indent2 + SubSpaces);
            }
        }
    }
}
