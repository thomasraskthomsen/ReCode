using System;
using System.Collections.Generic;
using System.Linq;
using ReCode.Parsers.Generation;

namespace ReCode.Parsers.RunTime
{
    /// <summary>
    /// A class for runnning a parser table generated a run time.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class RunTimeParser<T> : IDisposable
    {
        /// <summary>
        /// A delegate for communicating a runtime parser action.
        /// </summary>
        public delegate T RuntimeParserAction(ArraySegment<T> arguments);

        private class ArrayStack<TElm>
        {
            public readonly TElm[] Elements;
            public int Top = -1;

            public ArrayStack(int capacity) { Elements = new TElm[capacity]; }

            public void Push(TElm elm) { Elements[++Top] = elm; }

            public ArraySegment<TElm> Peek(int size) { return new ArraySegment<TElm>(Elements, Top + 1 - size, size); }

            public void Pop(int count = 1)
            {
                var newTop = Top - count;
                if (newTop < -1)
                    throw new Exception();
                Top = newTop;
            }

            public void Reset()
            {
                Top = -1;
            }
        }

        const int DefaultStackSize = 50;

        private readonly ParserTableEntry[][] _parserTableEntries;
        private readonly RuntimeParserAction[] _runtimeParserActions;
        private readonly ArrayStack<int> _stateStack = new ArrayStack<int>(2 * DefaultStackSize);
        private readonly ArrayStack<T> _valueStack = new ArrayStack<T>(DefaultStackSize);
        private IEnumerator<KeyValuePair<int, T>> _inputEnumerator;

        /// <summary>
        /// Creates a runtime parser based on a parser table generated a runtime.
        /// </summary>
        /// <param name="parserTableEntries">The parser table.</param>
        /// <param name="actions">The actions associated with productions.</param>
        public RunTimeParser(ParserTableEntry[][] parserTableEntries, RuntimeParserAction[] actions)
        {
            var targets = new SortedSet<int>();
            foreach (var act in parserTableEntries.SelectMany(row => row.Where(r => r != null && r.Action == LrActionType.Reduce)))
                targets.Add(act.Target);
            if(targets.Max()+1 != actions.Length)
                throw new Exception("Incorrect number of actions");
            _parserTableEntries = parserTableEntries;
            _runtimeParserActions = actions;
        }

        public void SetInput(IEnumerable<KeyValuePair<int, T>> input)
        {
            _inputEnumerator = input.GetEnumerator();
        }

        void IDisposable.Dispose()
        {
            _inputEnumerator.Dispose();
        }

        /// <summary>
        /// Parses the input sequence.
        /// </summary>
        public T Parse()
        {
            _stateStack.Reset();
            _valueStack.Reset();
            _inputEnumerator.MoveNext();
            _stateStack.Push(0);
            var currentValue = _inputEnumerator.Current;
            while (true)
            {
                // fetch the current action
                var stateIndex = _stateStack.Elements[_stateStack.Top];
                var actionInfo = _parserTableEntries[stateIndex][currentValue.Key];
                if (actionInfo == null)
                    throw new Exception($"Parser error near token: {currentValue.Key};{currentValue.Value}");
                switch (actionInfo.Action)
                {
                    case LrActionType.Shift:
                        //move the next token to stack
                        _valueStack.Push(currentValue.Value);
                        _stateStack.Push(currentValue.Key);
                        _stateStack.Push(actionInfo.Target);
                        _inputEnumerator.MoveNext();
                        currentValue = _inputEnumerator.Current;
                        break;
                    case LrActionType.Reduce:
                        // fetch arguments for the production
                        var argCount = actionInfo.ArgCount;
                        var args = _valueStack.Peek(argCount);
                        // call production
                        var res = _runtimeParserActions[actionInfo.Target](args);
                        // alter the value stack
                        _valueStack.Pop(argCount);
                        _valueStack.Push(res);
                        // find goto state and adjust state stack
                        _stateStack.Pop(2 * argCount);
                        stateIndex = _stateStack.Elements[_stateStack.Top];
                        var gotoAction = _parserTableEntries[stateIndex][actionInfo.TargetTokenType];
                        if ((gotoAction == null) || (gotoAction.Action != LrActionType.Goto))
                            throw new Exception("Parser error");
                        _stateStack.Push(actionInfo.TargetTokenType);
                        _stateStack.Push(gotoAction.Target);
                        break;
                    case LrActionType.Accept:
                        return _valueStack.Elements[_valueStack.Top];
                    default:
                        throw new Exception("Parser error");
                }
            }
        }

    }
}
