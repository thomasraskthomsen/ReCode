using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReCode.RegularExpressions.Parsing.Nodes
{
    public class RegExNodeRanges : RegExNode
    {
        public readonly IReadOnlyList<RegExInputRange> Ranges;

        public RegExNodeRanges(params RegExInputRange[] ranges) : base(RegExType.Ranges)
        {
            Ranges = new List<RegExInputRange>(ranges);
        }

        public RegExNodeRanges(IReadOnlyList<RegExInputRange> ranges) : base(RegExType.Ranges)
        {
            Ranges = ranges;
        }

        public override void Print(StringBuilder sb, string indent1, string indent2)
        {
            var tok = string.Join(",", Ranges.Select(r => r.ToString()).ToArray());
            sb.AppendLine($"{indent1}Ranges({tok})");
        }

        private static IEnumerable<RegExInputRange> FindExcepts(RegExInputRange pos, IReadOnlyList<RegExInputRange> negatives)
        {
            foreach (var neg in negatives)
            {
                if ((neg.Max < pos.Min) || (pos.Max < neg.Min))
                    continue;

                if (pos.Min < neg.Min)
                {
                    var sub = new RegExInputRange(pos.Min, neg.Min-1);
                    foreach (var r in FindExcepts(sub, negatives))
                        yield return r;
                }

                if (neg.Max < pos.Max)
                {
                    var sub = new RegExInputRange(neg.Max+1, pos.Max);
                    foreach (var r in FindExcepts(sub, negatives))
                        yield return r;
                }
                yield break;
            }
            yield return pos;
        } 

        public static RegExNodeRanges Except(RegExNode n1, RegExNode n2)
        {
            var r1 = n1 as RegExNodeRanges;
            var r2 = n2 as RegExNodeRanges;
            if((r1==null) || (r2==null)) throw new Exception("Invalid expression used in 'except' construct.");
            var positives = r1.Ranges.SelectMany(range => FindExcepts(range, r2.Ranges)).ToList();
            return new RegExNodeRanges(positives);

        }
    }
}
