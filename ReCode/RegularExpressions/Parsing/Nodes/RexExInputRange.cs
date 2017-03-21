using System;

namespace ReCode.RegularExpressions.Parsing.Nodes
{
    public class RegExInputRange : IComparable<RegExInputRange>
    {
        public const uint Epsilon = uint.MaxValue;

        public static readonly RegExInputRange EPS = new RegExInputRange(Epsilon);

        public readonly uint Min;
        public readonly uint Max;

        public RegExInputRange()
        {
            Min = char.MinValue;
            Max = char.MaxValue;
        }

        public RegExInputRange(char c)
        {
            Min = c;
            Max = c;
        }

        private RegExInputRange(uint c)
        {
            Min = c;
            Max = c;
        }


        public RegExInputRange(uint min, uint max)
        {
            if(min>max) throw new ArgumentException("Invalid range.");
            Min = min;
            Max = max;
        }

        public int CompareTo(RegExInputRange other)
        {
            return Min.CompareTo(other.Min);
        }

        private static string AsString(uint c)
        {
            if (c == uint.MaxValue) return ".";
            if ((c < 20) || (c>250))
                return $"0x{c.ToString("X2")}";
            return $"'{(char) c}'";
        }

        public override string ToString()
        {
            var strMin = AsString(Min);
            var strMax = AsString(Max);
            if (Min == Max)
                return strMin;
            return $"[{strMin}-{strMax}]";
        }
    }
}
