using System.Collections.Generic;

namespace ReCode.RegularExpressions.Evaluation
{
    public class RegExEvaluator
    {
        private readonly RegExEvaluationNode _root;

        public RegExEvaluator(RegExEvaluationNode root)
        {
            _root = root;
        }

        private static RegExEvaluationNode Map(char c, RegExEvaluationNode node)
        {
            foreach (var range in node)
            {
                if (range.Key.Min > c)
                    break;
                if (c <= range.Key.Max)
                    return range.Value;
            }
            return null;
        }

        public unsafe KeyValuePair<ushort,int>? Match(string str)
        {
            ushort? bestAccept = null;
            int bestIndex = 0;
            fixed (char* letters = str)
            {
                var len = str.Length;
                var state = _root;
                for (var i = 0; i < len; i++)
                {
                    var c = letters[i];
                    state = Map(c, state);
                    if (state == null) break;
                    if (state.AcceptAction.HasValue)
                    {
                        if(bestAccept < state.AcceptAction.Value)
                            continue;
                        bestAccept = state.AcceptAction;
                        bestIndex = i;
                    }
                }
            }
            if (bestAccept.HasValue)
                return new KeyValuePair<ushort, int>(bestAccept.Value, bestIndex + 1);
            return null;
        }
    }
}
