using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ReCode.Parsers.Generation;
using ReCode.Parsers.Grammatics;

namespace ReCode.Parsers.CodeGeneration
{
    public class CodeGenerator : IGrammar
    {
        private Grammar _grammar;
        private int _actionIndex = 1;
        private Production _currentProduction;
        private Production[] _productions;

        protected readonly Action<string> WriteLine;
        private readonly string _valueType;

        public CodeGenerator(Action<string> lineWriter, string valueType)
        {
            WriteLine = lineWriter;
            _valueType = valueType;
        }

        public IEnumerable<CodeGenerator> DoGeneration()
        {
            // generation requires two runs - first we collect the grammar
            yield return this;

            // process the grammar
            _productions = _grammar.Productions.ToArray();
            var generator = new ParserGenerator(_grammar);
            var table = generator.GenerateParserTable();

            // on second run we render the code and insert the actions.
            WriteLine(PreParser.Replace("{VALUETYPE}", _valueType));
            // write the token enum
            WriteTokenEnum();
            // write the parser table code
            WriteParserTableCode(table);
            // put in the actions at the end using a second run
            yield return this;
            EndAction(); // (manually close the last action)
            // write the end of the parser function
            WriteLine(PostParser);
        }

        public IGrammar GrammarFromTokens(params string[] tokens)
        {
            if (_grammar != null) // second run for code generation
                return this;
            _grammar = Grammar.FromTokens(tokens);
            return _grammar;
        }

        private void WriteTokenEnum()
        {
            WriteLine("public enum Token {");
            for(var i=1;i<_grammar.Tokens.Length;i++)
            {
                if(_grammar.Productions.Any(p => p.Left == i))
                    continue;
                WriteLine($"  {_grammar.Tokens[i]} = {i},");
            }
            WriteLine("}");
        }

        private string Concatenate<T>(string prefix, IEnumerable<T> values, Func<T, string> mapping)
        {
            var sb = new StringBuilder();
            sb.Append(prefix);
            foreach (var v in values)
                sb.Append(mapping(v));
            return sb.ToString();
        } 

        const string ReadToken = "currentToken = NextToken();";

        private void WriteParserTableCode(ParserTableEntry[][] parserTable)
        {
            var numStates = parserTable.GetLength(0);
            WriteLine($"public {_valueType} Parse() {{");
            WriteLine($"  {_valueType} res = default({_valueType});");
            WriteLine($"  var targetTokenType = 0;");
            WriteLine("  _stateStack.Reset();");
            WriteLine("  _valueStack.Reset();");
            WriteLine($"  KeyValuePair<Token,{_valueType}> " + ReadToken);
            WriteLine("  _stateStack.Push(0); goto __state_0;");
            WriteLine("");

            WriteLine("__postReduce:");
            WriteLine("  /* find on the stack the state where we need to handle post-reduce goto. */");
            WriteLine("  var gotoState = _stateStack.Elements[_stateStack.Top];");
            WriteLine("  /* put information on the stack regarding the reduced type. */");
            WriteLine("  _stateStack.Push(targetTokenType);");
            WriteLine("  /* jump to the resulting state. */");
            WriteLine("  switch(gotoState){");

            for (var state = 0; state < numStates; state++)
            {
                var actions = parserTable[state];
                if (actions.Any(a => a?.Action == LrActionType.Goto))
                {
                    WriteLine($"  case {state}:");
                    WriteLine("    switch(targetTokenType){");
                    foreach (var actionType in Group(actions))
                    {
                        var action = actionType.First().Value;
                        if (action.Action != LrActionType.Goto) continue;
                        var validActions = actionType.ToArray();
                        foreach (var act in validActions)
                            WriteLine($"    case {act.Key}: _stateStack.Push({act.Value.Target}); goto __state_{act.Value.Target};");
                    }
                    WriteLine("    default: goto __syntaxError;");
                    WriteLine("  }");
                }
            }
            WriteLine("  default:");
            WriteLine("    goto __syntaxError;");
            WriteLine("  }");
            WriteLine("");


            for (var state = 0; state < numStates; state++)
            {
                WriteLine($"__state_{state}:");
                WriteLine("  switch((int)currentToken.Key){");
                var actions = parserTable[state];
                foreach (var actionType in Group(actions))
                {
                    var action = actionType.First().Value;
                    switch (action.Action)
                    {
                        case LrActionType.Accept:
                            WriteLine(Concatenate(" ", actionType, pair => $" case {pair.Key}:"));
                            WriteLine("  /* accept value from stack */");
                            WriteLine("  return _valueStack.Elements[_valueStack.Top];");
                            break;

                        case LrActionType.Shift:
                            WriteLine(Concatenate(" ", actionType, pair => $" case {pair.Key}:"));
                            WriteLine("  /* perform shift to state {action.Target} */");
                            WriteLine("  _valueStack.Push(currentToken.Value);");
                            WriteLine("  _stateStack.Push((int)currentToken.Key);");
                            WriteLine($"  {ReadToken}");
                            WriteLine($"  _stateStack.Push({action.Target}); goto __state_{action.Target};");
                            break;
                        case LrActionType.Reduce:
                            if (action.Target < 0) continue;
                            WriteLine(Concatenate(" ", actionType, pair => $" case {pair.Key}:"));
                            WriteLine($"  /* do reduce using production {action.Target} */");
                            WriteLine($"  goto __action_{action.Target};");
                            break;
                    }
                }
                WriteLine("  default:");
                WriteLine("    goto __syntaxError;");
                WriteLine("  }");
                WriteLine("");
            }
        }

        int IPrecedenceGroup.this[string token] => -1;


        private bool NewAction()
        {
            EndAction();
            _currentProduction = _productions[_actionIndex];
            //if (_actionIndex == 0) return false;
            WriteLine("");
            WriteLine($"__action_{_currentProduction.Id}:");
            WriteLine("  {");
            WriteLine($"    res = default({_valueType});");
            for (var arg = 0; arg < _currentProduction.Right.Count; arg++)
                WriteLine($"    var arg{arg} = _valueStack.Elements[_valueStack.Top-{_currentProduction.Right.Count - (1 + arg)}];");
            return true;

        }

        private void EndAction()
        {
            if (_currentProduction != null)
            {
                //if (_actionIndex != 0)
                {
                    WriteLine("  }");
                    var argCount = _currentProduction.Right.Count;
                    WriteLine($"  _valueStack.Pop({argCount});");
                    WriteLine("  _valueStack.Push(res);");
                    WriteLine("  // find goto state and adjust state stack");
                    WriteLine($"  _stateStack.Pop({2*argCount});");
                    WriteLine($"  targetTokenType = {_currentProduction.Left};");
                    WriteLine("goto __postReduce;");
                }
                _currentProduction = null;
                _actionIndex++;

            }
        }

        bool IPrecedenceGroup.Matches(int left, params int[] right)
        {
            return NewAction();
        }

        bool IPrecedenceGroup.Matches(string left, params string[] right)
        {
            return NewAction();
        }

        bool IPrecedenceGroup.MatchesLeft(int left, params int[] right)
        {
            return NewAction();
        }

        bool IPrecedenceGroup.MatchesLeft(string left, params string[] right)
        {
            return NewAction();
        }

        bool IPrecedenceGroup.MatchesRight(int left, params int[] right)
        {
            return NewAction();
        }

        bool IPrecedenceGroup.MatchesRight(string left, params string[] right)
        {
            return NewAction();
        }

        IPrecedenceGroup IGrammar.WithPrecedenceGroup()
        {
            return this;
        }

        const string PreParser = @"
        private class ArrayStack<TElm>
        {
          public readonly TElm[] Elements;
          public int Top;
          [System.Runtime.CompilerServices.MethodImplAttribute(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
          public void Reset() { Top = -1; }
          public ArrayStack(int capacity) { Elements = new TElm[capacity]; Top=-1; }
          [System.Runtime.CompilerServices.MethodImplAttribute(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
          public void Push(TElm elm) { Elements[++Top] = elm; }
          [System.Runtime.CompilerServices.MethodImplAttribute(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
          public void Pop(int count = 1) { Top -= count; }
        }
        const int DefaultStackSize = 50;
        private readonly ArrayStack<int> _stateStack = new ArrayStack<int>(2 * DefaultStackSize);
        private readonly ArrayStack<{VALUETYPE}> _valueStack = new ArrayStack<{VALUETYPE}>(DefaultStackSize);

        ";

        const string PostParser = @"  __syntaxError:
  throw new Exception(""Syntax error"");
}";

        public static IGrouping<string, KeyValuePair<int, ParserTableEntry>>[] Group(ParserTableEntry[] entries)
        {
            var filteredEntries =
                entries.Select((e, i) => new KeyValuePair<int, ParserTableEntry>(i, e))
                    .Where(pair => pair.Value != null);
            return filteredEntries.GroupBy(pair => pair.Value.ToString()).ToArray();
        }
    }
}
