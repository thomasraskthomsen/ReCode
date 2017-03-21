using System.Collections.Generic;
using ReCode.RegularExpressions.Parsing.Nodes;

namespace ReCode.RegularExpressions.Parsing
{
    public partial class RegExParser
    {
        private readonly string _exp;
        private int _position;
        private readonly IReadOnlyDictionary<string, RegExNode> _namedExpressions;

        public RegExParser(string exp, IReadOnlyDictionary<string, RegExNode> namedExpressions = null)
        {
            _exp = exp;
            _namedExpressions = namedExpressions ?? new Dictionary<string, RegExNode>();
        }

    }
}
