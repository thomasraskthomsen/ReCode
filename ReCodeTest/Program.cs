using ReCode.RegularExpressions.Parsing;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Principal;
using System.Text;
using ReCode.Parsers.Generation;
using ReCode.Parsers.Grammatics;
using ReCode.Parsers.RunTime;
using ReCode.RegularExpressions.Evaluation;
using ReCode.RegularExpressions.NfaEvaluation;
using ReCode.RegularExpressions.Parsing.Nodes;
using ReCode.RegularExpressions.Transform;

namespace ReCodeTest
{
    class Program
    {
        static unsafe void TestRe()
        {
            while (true)
            {
                Console.Write("> ");
                var line = Console.ReadLine();
                if (string.IsNullOrEmpty(line))
                    break;
                fixed (char* start = line)
                {
                    var res = TestParser2.Parse(start, line.Length);
                    if (res.HasValue)
                    {
                        var tok = line.Substring(0, res.Value.Value);
                        Console.WriteLine("Accept({0}):{1}", res.Value.Key, tok);
                    }
                }
            }
        }


        static void TestLr()
        {

            var parser = new TestLr1();
            var sw = Stopwatch.StartNew();
            const int iterations = 1000000;
            Node root = null;
            for (var i = 0; i < iterations; i++)
            {
                root = parser.Parse();
                parser.Reset();
            }
            var seconds = sw.Elapsed.TotalSeconds;
            var rate = (long)(TestLr1.Sequence.Length * (long)iterations / seconds);
            Console.WriteLine("Parser rate: {0} tokens/sec", rate);
            Console.WriteLine("Parser result: {0}", root);
        }

        static void TestGenerationSample()
        {
            var g = Grammar.FromTokens("S'", "$", "S", "C", "C", "c", "d");
            g.Matches("S'", "S");
            g.Matches("S", "C", "C");
            g.Matches("C", "c", "C");
            g.Matches("C", "d");
            var gen = new ParserGenerator(g);
            var tab = gen.GenerateParserTable();
        }

        static void TestGeneration()
        {
            var g = Grammar.FromTokens("E'", "$", "E", "T", "F", "P", "w", "(", ")", "+", "*", "?", "|");
            g.Matches("E'", "E", "$");
            g.Matches("E", "T");
            g.Matches("E", "E", "|", "T");
            g.Matches("T", "F");
            g.Matches("T", "T", "F");
            g.Matches("F", "P");
            g.Matches("F", "P", "*");
            g.Matches("F", "P", "+");
            g.Matches("F", "P", "?");
            g.Matches("P", "w");
            g.Matches("P", "(", "E", ")");
            var gen = new ParserGenerator(g);
            var tab = gen.GenerateParserTable();
        }

        private static IEnumerable<KeyValuePair<int, double>> Tokenize(string str)
        {
            foreach (var c in str)
            {
                if (c >= '0' && c <= '9')
                {
                    var i = c - '0';
                    yield return new KeyValuePair<int, double>(10, i);
                }
                else if (c == '+')
                {
                    yield return new KeyValuePair<int, double>(3, double.NaN);
                }
                else if (c == '-')
                {
                    yield return new KeyValuePair<int, double>(4, double.NaN);
                }
                else if (c == '*')
                {
                    yield return new KeyValuePair<int, double>(5, double.NaN);
                }
                else if (c == '/')
                {
                    yield return new KeyValuePair<int, double>(6, double.NaN);
                }
                else if (c == '(')
                {
                    yield return new KeyValuePair<int, double>(8, double.NaN);
                }
                else if (c == ')')
                {
                    yield return new KeyValuePair<int, double>(9, double.NaN);
                }
            }
            yield return new KeyValuePair<int, double>(1, double.NaN);
        }

        static void TestGenerationAmbiguous()
        {
            var g = Grammar.FromTokens("E", "+", "-", "*", "/", "N", "(", ")", "x");
            g.Matches("E", "N");
            g.MatchesRight("E", "E", "+", "E");
            g.MatchesRight("E", "E", "-", "E");
            g.MatchesLeft("E", "E", "*", "E");
            g.MatchesLeft("E", "E", "/", "E");
            g.MatchesLeft("E", "-", "E");
            g.Matches("E", "(", "E", ")");
            g.Matches("N", "x");
            g.Matches("N", "N", "x");
            var gen = new ParserGenerator(g);
            var tab = gen.GenerateParserTable();
            Console.WriteLine("Constructed table with {0} states.", tab.Length);
            var parser = new RunTimeParser<double>(tab, new RunTimeParser<double>.RuntimeParserAction[]
            {
                seg => seg.Array[seg.Offset],
                seg => seg.Array[seg.Offset],
                seg => seg.Array[seg.Offset] + seg.Array[seg.Offset+2],
                seg => seg.Array[seg.Offset] - seg.Array[seg.Offset+2],
                seg => seg.Array[seg.Offset] * seg.Array[seg.Offset+2],
                seg => seg.Array[seg.Offset] / seg.Array[seg.Offset+2],
                seg => - seg.Array[seg.Offset + 1],
                seg => seg.Array[seg.Offset + 1],
                seg => seg.Array[seg.Offset],
                seg => 10 * seg.Array[seg.Offset] + seg.Array[seg.Offset + 1]
            });
            while (true)
            {
                Console.Write(">");
                var line = Console.ReadLine();
                if (string.IsNullOrEmpty(line)) break;
                parser.SetInput(Tokenize(line));
                try
                {
                    var res = parser.Parse();
                    Console.WriteLine("Res={0}", res);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("ERROR: {0}", ex);
                }
            }
        }

        static void TestRegExParser2Scanner()
        {
            var parser = new RegExParser(@" name42 'ab'+'.'?[\[\]0-9]+  ~1| 'testing\'s'""more \""testing\"""" [0-5] ([a-f]~2|[0-34-9a-da-f]~3)  ");
            while (true)
            {
                var token = parser.NextToken();
                Console.WriteLine($"{token.Key},{token.Value}");
                if (token.Key == RegExParser.Token.END) break;
            }
        }

        static void TestRegExParser2()
        {
            var dict = new Dictionary<string, RegExNode> { { "name42", new RegExNodeName("+name42+") } };
            var parser = new RegExParser(@" name42 'ab'+'.'?[\[\]0-9]+  ~1| 'testing\'s'""more \""testing\"""" [0-5] ([a-f]~2|[0-34-9a-da-f]~3)  ", dict);
            var tree = parser.Parse();
            Console.Write(tree);
        }

        static void BugTest()
        {
            foreach (var token in ReCode.ReCode.RegularExpressionTokens(Console.WriteLine))
            {
                token.Matches(@"[']");
                token.Matches(@"[""]");
                token.Matches(@"[\|]");
                token.Matches(@"[\(]");
                token.Matches(@"[\)]");
                token.Matches(@"[\+]");
                token.Matches(@"[\?]");
                token.Matches(@"[\*]");
                token.Matches(@"[\[]");
                token.Matches(@"[ ]+");
                token.Matches(@"[\~][0-9]+");
                token.Matches(@"([a-zA-Z])([a-zA-Z0-9])*");
            }

            foreach (var token in ReCode.ReCode.RegularExpressionTokens(Console.WriteLine))
            {
                token.Matches(@"[']");
                token.Matches(@"[\\][.]");
                token.Matches(@"[.]");
            }
            foreach (var token in ReCode.ReCode.RegularExpressionTokens(Console.WriteLine))
            {
                token.Matches(@"[""]");
                token.Matches(@"[\\][.]");
                token.Matches(@"[.]");
            }
            foreach (var token in ReCode.ReCode.RegularExpressionTokens(Console.WriteLine))
            {
                token.Matches(@"[\]]");
                token.Matches(@"[0-9][\-][0-9]");
                token.Matches(@"[A-Z][\-][A-Z]");
                token.Matches(@"[a-z][\-][a-z]");
                token.Matches(@"[\\][.]");
                token.Matches(@"[\.]");
                token.Matches(@"[.]");
            }
        }

        static unsafe void TestRe3()
        {
            Console.WriteLine("Accepts html");
            while (true)
            {
                Console.Write("> ");
                var line = Console.ReadLine();
                if (string.IsNullOrEmpty(line)) break;
                int res;
                fixed (char* ptr = line)
                    res = TestParser3.Parse(ptr, line.Length);
                Console.WriteLine(res);
            }
        }

        static void TestReGenerator()
        {
            foreach (var token in ReCode.ReCode.RegularExpressionTokens(Console.WriteLine))
            {
                token.Define("letters", "[a-z]");
                token.Define("validLetters", "[i]");
                token.Define("invalidLetters", "[f-p] \\ validLetters");
                token.Define("notInvalidLetters", "letters \\ invalidLetters");
                token.Matches(@"notInvalidLetters+");
                token.Matches(@"invalidLetters+");
                token.Matches(@"letters+");
            }

        }

        static void TestParser()
        {
            while (true)
            {
                Console.Write('>');
                var line = Console.ReadLine();
                if (string.IsNullOrEmpty(line)) break;
                try
                {
                    var parser = new RegExParser(line);
                    var tree = parser.Parse();
                    var sb = new StringBuilder();
                    tree.Print(sb, string.Empty, string.Empty);
                    Console.WriteLine(sb);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }

        static void TestPrecedence()
        {
            var g = Grammar.FromTokens("EXP", "SET", "Set", "Except");
            var g1 = g.WithPrecedenceGroup();
            g1.Matches("EXP", "Set");
            var g2 = g.WithPrecedenceGroup();
            g2.MatchesLeft("EXP", "EXP", "EXP");
            var g3 = g.WithPrecedenceGroup();
            g3.MatchesRight("EXP", "EXP", "Except", "EXP");

            var gen = new ParserGenerator(g);
            var tab = gen.GenerateParserTable();
            Console.Write("\t");
            foreach (var s in g.Tokens)
            {
                var tok = s.Length > 7 ? s.Substring(0, 7) : s;
                Console.Write($"{tok}\t");
            }
            Console.WriteLine();

            for(var i=0;i<tab.Length;i++)
            {
                var row = tab[i];
                Console.Write($"#{i}:\t");
                for(var j=0;j<row.Length;j++)
                {
                    var col = row[j];
                    if (col == null)
                    {
                        Console.Write("\t");
                        continue;
                    }
                    var action = col.Action.ToString().Substring(0, 2);
                    Console.Write($"{action}-{col.Target}\t");
                }
                Console.WriteLine();
            }

            var parser = new RunTimeParser<string>(tab, new RunTimeParser<string>.RuntimeParserAction[]
            {
                arguments => arguments.Array[arguments.Offset],
                arguments => arguments.Array[arguments.Offset],
                arguments => $"({arguments.Array[arguments.Offset]} + {arguments.Array[arguments.Offset+1]})",
                arguments => $"({arguments.Array[arguments.Offset]} \\ {arguments.Array[arguments.Offset+2]})",
            });
            parser.SetInput(new []
            {
                new KeyValuePair<int, string>(4, "A"),
                new KeyValuePair<int, string>(4, "B"),
                new KeyValuePair<int, string>(5, "\\"),
                new KeyValuePair<int, string>(4, "C"),
                new KeyValuePair<int, string>(5, "\\"),
                new KeyValuePair<int, string>(4, "D"),
                new KeyValuePair<int, string>(1, "END"),
            });
            var res = parser.Parse();
            Console.WriteLine(res);
        }

        private static void TestRuntimeNfa()
        {
            const int iNum = 2;
            const int jNum = 100;

            RegExNode rootNode = null;
            var lastRuleNumber = ushort.MaxValue;
            var wildcard = new RegExNodeRepeat(new RegExNodeRanges(new RegExInputRange()), RegExRepeatType.ZeroOrMore);
            for (var i = 0; i < iNum; i++)
            {
                var si = new RegExNodeSequence($"{i:X}/");
                for (var j = 0; j < jNum; j++)
                {
                    var sj = new RegExNodeSequence($"{3*j:X}¤");
                    var node = new RegExNodeAccept(new RegExNodeConcat(si, new RegExNodeConcat(wildcard, sj)), --lastRuleNumber);
                    if (rootNode == null)
                        rootNode = node;
                    else
                        rootNode = RegExNodeUnion.Of(rootNode, node);
                }
            }
            {
                var sw = Stopwatch.StartNew();
                var builder = new Builder();
                var nfaRoot = builder.BuildNfa(rootNode);
                var eval = new RegExNfaEvaluator(nfaRoot);
                var nodes = eval.GetNodes();
                Console.WriteLine("NFA Processor prepared in {0} ms", sw.Elapsed.TotalMilliseconds);

                sw.Restart();
                var lastMatchedRuleNumber = ushort.MaxValue;
                for (var i = 0; i < iNum; i++)
                {
                    for (var j = 0; j < jNum; j++)
                    {
                        var pattern = $"{i:X}/0000/testing{3*j:X}¤";
                        var match = eval.Match(pattern, nodes);
                        var expected = --lastMatchedRuleNumber;
                        if (match.Value.Key != expected)
                            throw new Exception("Incorrectly matched sequence");
                    }
                }

                const int numSteps = iNum*jNum;
                var iterationMs = sw.Elapsed.TotalMilliseconds/numSteps;
                Console.WriteLine("NFA Processing time {0} ms/item", iterationMs);
            }
            {
                var sw = Stopwatch.StartNew();
                var builder = new Builder();
                var dfaRoot = builder.Build(rootNode);
                var eval = new RegExEvaluator(dfaRoot);
                Console.WriteLine("DFA Processor prepared in {0} ms", sw.Elapsed.TotalMilliseconds);

                sw.Restart();
                var lastMatchedRuleNumber = ushort.MaxValue;
                for (var i = 0; i < iNum; i++)
                {
                    for (var j = 0; j < jNum; j++)
                    {
                        var pattern = $"{i:X}/0000/testing{3*j:X}¤";
                        var match = eval.Match(pattern);
                        var expected = --lastMatchedRuleNumber;
                        if (match.Value.Key != expected)
                            throw new Exception("Incorrectly matched sequence");
                    }
                }

                const int numSteps = iNum * jNum;
                var iterationMs = sw.Elapsed.TotalMilliseconds / numSteps;
                Console.WriteLine("DFA Processing time {0} ms/item", iterationMs);
            }
        }

        static void Main(string[] args)
        {
            //TestGenerationSample();
            //TestGeneration();
            //TestGenerationAmbiguous();
            //TestReGenerator();
            //TestRe();
            //TestLr();
            //TestRegExParser2Scanner();
            //TestRegExParser2();
            //BugTest();
            //TestRe3();
            //TestParser();
            //TestPrecedence();
            TestRuntimeNfa();
        }
    }
}
