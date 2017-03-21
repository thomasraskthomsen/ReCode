using ReCode.Parsers.Generation.Data;

namespace ReCode.Parsers.Generation
{
    /// <summary>
    /// An enum the represents action types associated with a parser table.
    /// </summary>
    public enum LrActionType
    {
        /// <summary>
        /// Shift action (<see cref="LrAction"/>)
        /// </summary>
        Shift,
        /// <summary>
        /// Reduce action (<see cref="LrAction"/>)
        /// </summary>
        Reduce,
        /// <summary>
        /// Goto action (<see cref="LrAction"/>)
        /// </summary>
        Goto,
        /// <summary>
        /// Accept action (<see cref="LrAction"/>)
        /// </summary>
        Accept
    }
}