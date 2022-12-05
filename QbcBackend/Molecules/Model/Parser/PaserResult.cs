using QbcBackend.Molecules.Model.Molecule;
using QbcBackend.Tools.Base.Model;

namespace QbcBackend.Molecules.Model.Parser
{
    public class ParseResult : QbcBase
    {
        public ParseResult()
        {
            this.Molecule = new MoleculeInfo();
        }

        public MoleculeInfo Molecule
        {
            get; set;
        }

        public bool Error
        {
            get;
            set;
        }


    }
}
