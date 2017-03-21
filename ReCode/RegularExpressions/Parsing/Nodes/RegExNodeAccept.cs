using System.Text;

namespace ReCode.RegularExpressions.Parsing.Nodes
{
    public class RegExNodeAccept : RegExNode
    {
        public readonly ushort AcceptState;
        public readonly RegExNode Expression;

        public RegExNodeAccept(RegExNode expression, ushort acceptState) : base(RegExType.Accept)
        {
            Expression = expression;
            AcceptState = acceptState;
        }

        public override void Print(StringBuilder sb, string indent1, string indent2)
        {
            sb.AppendLine($"{indent1}Accept({AcceptState})");
            Expression?.Print(sb, indent2 + SubDash, indent2 + SubSpaces);
        }
    }
}
