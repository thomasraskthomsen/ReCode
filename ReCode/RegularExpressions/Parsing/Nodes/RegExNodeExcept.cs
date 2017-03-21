using System.Text;

namespace ReCode.RegularExpressions.Parsing.Nodes
{
    public class RegExNodeExcept : RegExNode
    {
        public readonly RegExNode Expression1;
        public readonly RegExNode Expression2;

        public RegExNodeExcept(RegExNode e1, RegExNode e2) : base(RegExType.Except)
        {
            Expression1 = e1;
            Expression2 = e2;
        }

        public override void Print(StringBuilder sb, string indent1, string indent2)
        {
            sb.AppendLine($"{indent1}Except");
            Expression1.Print(sb, indent2 + SubDash, indent2 + SubSpaces);
            Expression2.Print(sb, indent2 + SubDash, indent2 + SubSpaces);
        }
    }
}
