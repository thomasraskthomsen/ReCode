using System.Collections;
using System.Collections.Generic;

namespace ReCode.Parsers.Generation.Data
{
    public class ActionSet : IReadOnlyList<LrAction>
    {
        private readonly List<LrAction> _actions = new List<LrAction>();
        private ushort? _minPrecedense;

        public void Add(LrAction action)
        {
            if (_minPrecedense.HasValue)
            {
                if (!action.Precedence.HasValue) return;
                if (action.Precedence.Value < _minPrecedense.Value) return;
                if (action.Precedence.Value > _minPrecedense.Value)
                {
                    _actions.Clear();
                    _minPrecedense = action.Precedence;
                }
            }
            else if (action.Precedence.HasValue)
            {
                _actions.Clear();
                _minPrecedense = action.Precedence.Value;
            }
            if(!_actions.Contains(action))
                _actions.Add(action);
        }


        public IEnumerator<LrAction> GetEnumerator()
        {
            return _actions.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _actions.GetEnumerator();
        }

        public int Count => _actions.Count;

        public LrAction this[int index] => _actions[index];
    }
}
