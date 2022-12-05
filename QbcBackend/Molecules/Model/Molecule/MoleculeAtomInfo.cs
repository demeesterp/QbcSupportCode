using QbcBackend.Tools.Base.Model;

namespace QbcBackend.Molecules.Model.Molecule
{
    public class MoleculeAtomInfo : QbcBase
    {
        public string Symbol { get; set; }
        public string Name { get; set; }
        public int AtomNumber { get; set; }
        public decimal? AtomMass { get; set; }
        public decimal? AtomRadius { get; set; }
        public decimal? ElectroNegativity { get; set; }
        public decimal? ElectronAffinity { get; set; }
    }
}
