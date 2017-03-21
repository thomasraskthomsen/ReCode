using System.Collections.Generic;
using ReCode.RegularExpressions.Parsing.Nodes;

namespace ReCode.RegularExpressions.Evaluation
{
    public class RegExEvaluationNode : SortedList<RegExInputRange, RegExEvaluationNode>
    {
        public int Id;
        public ushort? AcceptAction;
    }
}
