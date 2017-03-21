using System.Text;

namespace ReCode.RegularExpressions.Parsing.Nodes
{
    public enum RegExRepeatType
    {
        ZeroOrOne,
        ZeroOrMore,
        OneOrMore
    }

    public class RegExNodeRepeat : RegExNode
    {
        public RegExRepeatType RegExRepeatType;
        public readonly RegExNode Expression;

        public RegExNodeRepeat(RegExNode intern, RegExRepeatType regExRepeatType) : base(RegExType.Repeat)
        {
            Expression = intern;
            RegExRepeatType = regExRepeatType;
        }

        public override void Print(StringBuilder sb, string indent1, string indent2)
        {
            sb.AppendLine($"{indent1}Repeat({RegExRepeatType})");
            Expression.Print(sb, indent2 + SubDash, indent2 + SubSpaces);
        }
    }
}
