using System;
using System.Collections.Generic;

namespace QbcBackend.Molecules.Entities
{
    public partial class Bond
    {
        public Bond()
        {
            BondAtom = new HashSet<BondAtom>();
        }

        public int Id { get; set; }
        public int AppId { get; set; }
        public int MoleculeId { get; set; }
        public int Atom1Position { get; set; }
        public int Atom2Position { get; set; }
        public decimal? Distance { get; set; }
        public decimal? BondOrder { get; set; }
        public decimal? BondOrderAcid { get; set; }
        public decimal? BondOrderBase { get; set; }
        public decimal? OverlapPopulation { get; set; }
        public decimal? OverlapPopulationAcid { get; set; }
        public decimal? OverlapPopulationBase { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
        public byte[] Timestamp { get; set; }

        public virtual Molecule Molecule { get; set; }
        public virtual ICollection<BondAtom> BondAtom { get; set; }
    }
}
