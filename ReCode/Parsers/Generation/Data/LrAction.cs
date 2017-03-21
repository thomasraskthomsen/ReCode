using System;
using System.Text;
using ReCode.Parsers.Grammatics;

namespace ReCode.Parsers.Generation.Data
{
    /// <summary>
    /// Representation of an action associated with an <see cref="LrState"/> and a symbol.
    /// </summary>
    public class LrAction : IEquatable<LrAction>
    {
        public readonly LrActionType Action;
        public readonly LrState Target;
        public readonly Production Production;
        public readonly ushort? Precedence;

        private LrAction(LrActionType action, ushort? precedence, LrState target, Production production)
        {
            Action = action;
            Precedence = precedence;
            Target = target;
            Production = production;
        }

        /// <summary>
        /// Creates an accept action.
        /// </summary>
        /// <returns></returns>
        public static LrAction Accept()
        {
            return new LrAction(LrActionType.Accept, null, null, null);
        }

        /// <summary>
        /// Creates a shift to the specified state.
        /// </summary>
        public static LrAction Shift(LrState state, ushort? precedence)
        {
            return new LrAction(LrActionType.Shift, precedence, state, null);
        }

        /// <summary>
        /// Creates a goto to the specified state.
        /// </summary>
        public static LrAction Goto(LrState state, ushort? precedence)
        {
            return new LrAction(LrActionType.Goto, precedence, state, null);
        }

        /// <summary>
        /// Creates a reduce using the specified production.
        /// </summary>
        public static LrAction Reduce(Production production, ushort? precedence)
        {
            return new LrAction(LrActionType.Reduce, precedence, null, production);
        }


        public bool Equals(LrAction other)
        {
            return
                (Action == other.Action) &&
                (Target == other.Target) &&
                (Production == other.Production);
        }

        /// <summary>
        /// A humanly readable repesentation of the item.
        /// </summary>
        public override string ToString()
        {
            var sb  =new StringBuilder();
            sb.Append(Action.ToString().Substring(0, 1));
            if (Target != null)
                sb.Append(Target.Index);
            if (Production != null)
                sb.Append($"({Production})");
            if (Precedence.HasValue)
                sb.Append($"@{Precedence.Value}");
            return sb.ToString();
        }
    }
}
