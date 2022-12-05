using QbcBackend.Tools.Base.Model;

namespace QbcBackend.Molecules.Model.Molecule
{
    public class MoleculeAtomOrbital : QbcBase
    {
        public int Position { get; set; }
        public string Symbol { get; set; }
        public decimal? MullikenPopulation { get; set; }
        public decimal? MullikenPopulationAcid { get; set; }
        public decimal? MullikenPopulationBase { get; set; }
        public decimal? LowdinPopulation { get; set; }
        public decimal? LowdinPopulationAcid { get; set; }
        public decimal? LowdinPopulationBase { get; set; }

    }
}
