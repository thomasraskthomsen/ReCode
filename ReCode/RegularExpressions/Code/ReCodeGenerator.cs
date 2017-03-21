using System;
using System.Collections.Generic;
using System.Text;
using ReCode.RegularExpressions.Parsing;
using ReCode.RegularExpressions.Parsing.Nodes;
using ReCode.RegularExpressions.Transform;

namespace ReCode.RegularExpressions.Code
{
    public interface IReCodeGenerator
    {
        void Define(string name, string expression);
        bool Matches(string expression);
        bool Fails { get; }
    }

    public class ReCodeGenerator : IReCodeGenerator
    {
        public StringBuilder StringBuilder = new StringBuilder();
        public int LastLabelId;
        public ushort NumAcceptCases;
        public ushort NumProcessedCases;
        public RegExNode Expression { get; private set; }
        public bool DoCodeGeneration { get; private set; }
        public string EndTag { get; private set; } = string.Empty;
        public readonly int InstanceId = _nextInstanceId++;
        public Dictionary<string, RegExNode> NamedExpressions = new Dictionary<string, RegExNode>();

        private static int _nextInstanceId;

        protected Action<string> PrintLine;

        public GeneratorScope Scope { get; private set; }

        public IEnumerable<ReCodeGenerator> Generate(Action<string> printLine)
        {
            PrintLine = printLine;

            // collect expressions
            yield return this;

            // write actions
            var builder = new Builder();
            builder.Build(Expression);
            PrintLine("{");
            PrintLine(builder.Generate(string.Empty, this));
            DoCodeGeneration = true;
            yield return this;
            PrintLine("}");
        }

        public void Define(string name, string expression)
        {
            if (DoCodeGeneration) return;
            var parser = new RegExParser(expression, NamedExpressions);
            var root = parser.Parse();
            NamedExpressions[name] = root;
        }

        public bool Matches(string expression)
        {
            if (DoCodeGeneration)
            {
                PrintLine("");
                PrintLine($"accept{InstanceId}_{NumProcessedCases}:");
                PrintLine(" pNext = pEnd;");
                NumProcessedCases++;
                return true;
            }
            var parser = new RegExParser(expression, NamedExpressions);
            var root = parser.Parse();
            var accept = new RegExNodeAccept(root, NumAcceptCases);
            if (Expression == null)
                Expression = accept;
            else
                Expression = RegExNodeUnion.Of(Expression, accept);
            ++NumAcceptCases;
            return false;
        }

        public bool Fails
        {
            get
            {
                if (DoCodeGeneration)
                {
                    PrintLine("");
                    PrintLine($"nonaccept{InstanceId}:");
                    return true;
                }
                return false;
            }
        }
    }

    public class GeneratorScope
    {
        private readonly ReCodeGenerator _reCodeGenerator;
        public readonly string Indentation;

        public GeneratorScope(ReCodeGenerator generator, string indentation)
        {
            _reCodeGenerator = generator;
            Indentation = indentation;
        }

        public GeneratorScope Indent()
        {
            return new GeneratorScope(_reCodeGenerator, Indentation + "    ");
        }

        public void WriteLine(string format, params object[] args)
        {
            var line = string.Format(format, args);
            _reCodeGenerator.StringBuilder.Append(Indentation);
            _reCodeGenerator.StringBuilder.AppendLine(line);
        }

        public void WriteLine(string line)
        {
            _reCodeGenerator.StringBuilder.Append(Indentation);
            _reCodeGenerator.StringBuilder.AppendLine(line);
        }

        public void SetLabel(string label)
        {
            WriteLine("{0}:", label);
        }


        public void Goto(string label)
        {
            WriteLine("goto {0};", label);
        }

        public string StateLabel(int stateId)
        {
            return $"state{_reCodeGenerator.InstanceId}_{stateId}";
        }

        public string AcceptLabel(ushort? acceptId)
        {
            if (!acceptId.HasValue)
                return $"nonaccept{_reCodeGenerator.InstanceId}";
            return $"accept{_reCodeGenerator.InstanceId}_{acceptId}";
        }

        public void IncrementPos(string failLabel, bool first)
        {
            WriteLine($"if({Cursor} >= {Limit}) goto {failLabel};");
            var declare = first ? "var " : string.Empty;
            WriteLine($"{declare}{Current} = *{Cursor}++;");
        }

        public void MarkPos()
        {
            WriteLine($"{End} = {Cursor};");
        }

        public void IfBegin(uint limit, int cnt)
        {

            var label = (limit >= 33 && limit <= 126) ? $" /* ('{(char)(limit - 1)}') '{(char)limit}' */ " : string.Empty;
            var scope = cnt > 0 ? @" {" : string.Empty;
            WriteLine($"if({Current} < 0x{limit:X}){label}{scope}");
        }

        public void IfEnd(int cnt)
        {
            if (cnt > 0)
                WriteLine(@"}");
        }

        public void NewLine()
        {
            _reCodeGenerator.StringBuilder.AppendLine();
        }

        public readonly string Current = "current";
        public readonly string Cursor = "pNext";
        public readonly string Limit = "pLimit";
        public readonly string End = "pEnd";
    }
}
