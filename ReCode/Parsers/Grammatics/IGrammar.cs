using System.Security.Cryptography.X509Certificates;

namespace ReCode.Parsers.Grammatics
{
    public interface IPrecedenceGroup
    {
        int this[string token] { get; }
        bool Matches(int left, params int[] right);
        bool MatchesLeft(int left, params int[] right);
        bool MatchesRight(int left, params int[] right);
        bool Matches(string left, params string[] right);
        bool MatchesLeft(string left, params string[] right);
        bool MatchesRight(string left, params string[] right);
    }

    public interface IGrammar : IPrecedenceGroup
    {
        IPrecedenceGroup WithPrecedenceGroup();
    }
}
