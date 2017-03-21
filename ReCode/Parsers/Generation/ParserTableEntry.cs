namespace ReCode.Parsers.Generation
{
    /// <summary>
    /// The building block of the parser table. We have a row of these for each state. Each item in the row corresponds to a symbol. 
    /// It is nullable in case we have no action defined for a specific symbol in a specific state (which constitutes a syntax error).
    /// </summary>
    public class ParserTableEntry 
    {
        /// <summary>
        /// The action to perform.
        /// </summary>
        public LrActionType Action;
        /// <summary>
        /// The action target (production or state depending on the action type).
        /// </summary>
        public int Target;
        /// <summary>
        /// The number of arguments of the production (when reduce action).
        /// </summary>
        public int ArgCount;
        /// <summary>
        /// The resulting reduction token type (when reduce action).
        /// </summary>
        public int TargetTokenType;

        /// <summary>
        /// A humanly readable repesentation of the item.
        /// </summary>
        public override string ToString()
        {
            return $"{Action}-{Target}-{ArgCount}-{TargetTokenType}";
        }
    }
}