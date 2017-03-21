using System;
using System.Collections.Generic;
using ReCode.Parsers.CodeGeneration;
using ReCode.RegularExpressions.Code;

namespace ReCode
{
    public static class ReCode
    {
        /// <summary>
        /// Used for generating code for regular expressions.
        /// </summary>
        public static IEnumerable<IReCodeGenerator> RegularExpressionTokens(Action<string> writeLine)
        {
            var gen = new ReCodeGenerator();
            return gen.Generate(writeLine);
        }

        /// <summary>
        /// Used for generating code for an SLR(0) parser.
        /// </summary>
        public static IEnumerable<CodeGenerator> ParserTokens(Action<string> writeLine, string valueType) => new CodeGenerator(writeLine, valueType).DoGeneration();

    }
}
