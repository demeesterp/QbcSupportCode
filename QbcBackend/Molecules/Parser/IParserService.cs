using QbcBackend.Molecules.Model.Molecule;
using QbcBackend.Molecules.Model.Parser;
using System.Threading.Tasks;

namespace QbcBackend.Molecules.Parser
{
    public interface IParserService
    {
        Task<ParseResult> ParseChargeAsync(string content, MoleculeInfo existingMolecule);
        Task<ParseResult> ParseOptimizationAsync(string content);
        ParseResult ParseGmsInput(string content);
        ParseResult ParseGmsInputForFukui(string content);
        Task<ParseResult> ParseFukuiAsync(string neutralcontent, string basecontent, string acidcontent, MoleculeInfo existingMolecule);
    }
}
