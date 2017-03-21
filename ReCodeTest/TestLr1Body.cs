using System.Collections.Generic;

namespace ReCodeTest
{
    using static TestLr1.Token;

    public partial class TestLr1
    {
        public static readonly KeyValuePair<Token, Node>[] Sequence = new[]
        {
            new KeyValuePair<Token, Node>(BrBegin, null),

            new KeyValuePair<Token, Node>(Letters, new NodeString {Value = "A"}),
            new KeyValuePair<Token, Node>(Comma, null),
            new KeyValuePair<Token, Node>(Letters, new NodeString {Value = "B"}),
            new KeyValuePair<Token, Node>(Comma, null),
            new KeyValuePair<Token, Node>(Letters, new NodeString {Value = "C"}),
            new KeyValuePair<Token, Node>(Comma, null),

            new KeyValuePair<Token, Node>(CbBegin, null),
            new KeyValuePair<Token, Node>(CbEnd, null),

            new KeyValuePair<Token, Node>(Comma, null),

            new KeyValuePair<Token, Node>(CbBegin, null),

            new KeyValuePair<Token, Node>(Letters, new NodeString {Value = "Info"}),
            new KeyValuePair<Token, Node>(Equal, null),

            new KeyValuePair<Token, Node>(CbBegin, null),
            new KeyValuePair<Token, Node>(Letters, new NodeString {Value = "Id"}),
            new KeyValuePair<Token, Node>(Equal, null),
            new KeyValuePair<Token, Node>(Letters, new NodeString {Value = "42"}),
            new KeyValuePair<Token, Node>(CbEnd, null),

            new KeyValuePair<Token, Node>(CbEnd, null),

            new KeyValuePair<Token, Node>(BrEnd, null),
            new KeyValuePair<Token, Node>(END, null)
        };

        private int _next;

        public void Reset()
        {
            _next = 0;
        }

        public KeyValuePair<Token, Node> NextToken()
        {
            return Sequence[_next++];
        }
    }
}
