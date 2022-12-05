using QbcBackend.Molecules.Model.Molecule;
using System.Collections.Generic;

namespace QbcBackend.Molecules.Parser
{
    public interface IParserCommand
    {

        bool Parse(List<string> input, MoleculeInfo molecule);

    }
}
