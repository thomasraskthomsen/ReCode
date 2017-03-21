using System.Text;

namespace ReCode.RegularExpressions.Parsing.Nodes
{
    public class RegExNodeSequence : RegExNode
    {
        public readonly string Sequence;
        public readonly RegExCasing Casing;

        public RegExNodeSequence(string sequence, RegExCasing casing = RegExCasing.Sensitive) : base(RegExType.Sequence)
        {
            Sequence = sequence;
            Casing = casing;
        }

        public override void Print(StringBuilder sb, string indent1, string indent2)
        {
            sb.AppendLine($"{indent1}Sequence(Case{Casing},'{Sequence}')");
        }
    }
}
