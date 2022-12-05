using System;
using System.Collections.Generic;

namespace QbcBackend.Molecules.Entities
{
    public partial class Atom
    {
        public Atom()
        {
            AtomOrbital = new HashSet<AtomOrbital>();
            BondAtom = new HashSet<BondAtom>();
        }

        public int Id { get; set; }
        public int AppId { get; set; }
        public int MoleculeId { get; set; }
        public int Position { get; set; }
        public int Number { get; set; }
        public string Symbol { get; set; }
        public int? AtomicWeight { get; set; }
        public decimal PosX { get; set; }
        public decimal PosY { get; set; }
        public decimal PosZ { get; set; }
        public decimal? Radius { get; set; }
        public decimal? MullikenPopulation { get; set; }
        public decimal? MullikenPopulationAcid { get; set; }
        public decimal? MullikenPopulationBase { get; set; }
        public decimal? LowdinPopulation { get; set; }
        public decimal? LowdinPopulationAcid { get; set; }
        public decimal? LowdinPopulationBase { get; set; }
        public decimal? ChelpGcharge { get; set; }
        public decimal? ConnollyCharge { get; set; }
        public decimal? GeoDiscCharge { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
        public byte[] Timestamp { get; set; }

        public virtual Molecule Molecule { get; set; }
        public virtual ICollection<AtomOrbital> AtomOrbital { get; set; }
        public virtual ICollection<BondAtom> BondAtom { get; set; }
    }
}
