using System.Text;

namespace ReCode.RegularExpressions.Parsing.Nodes
{
    public class RegExNodeName: RegExNode
    {
        public readonly string Name;

        public RegExNodeName(string name) : base(RegExType.Blank)
        {
            Name = name;
        }

        public override void Print(StringBuilder sb, string indent1, string indent2)
        {
            sb.AppendLine($"{indent1}Name({Name})");
        }
    }
}
