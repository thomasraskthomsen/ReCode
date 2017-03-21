using System.Text;

namespace ReCode.RegularExpressions.Parsing.Nodes
{
    public class RegExNodeBlank : RegExNode
    {
        public RegExNodeBlank() : base(RegExType.Blank)
        {
        }

        public override void Print(StringBuilder sb, string indent1, string indent2)
        {
            sb.AppendLine($"{indent1}Blank");
        }
    }
}
