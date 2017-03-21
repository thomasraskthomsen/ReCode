using System.Text;

namespace ReCode.RegularExpressions.Parsing.Nodes
{
    public abstract class RegExNode
    {
        public readonly RegExType RegExType;

        protected RegExNode(RegExType regExType)
        {
            RegExType = regExType;
        }

        public abstract void Print(StringBuilder sb, string indent1, string indent2);

        public override string ToString()
        {
            var sb = new StringBuilder();
            Print(sb, string.Empty, string.Empty);
            return sb.ToString();
        }

        protected const string SubDash   = " +-";
        protected const string SubBar    = " | ";
        protected const string SubSpaces = "   ";
    }
}
