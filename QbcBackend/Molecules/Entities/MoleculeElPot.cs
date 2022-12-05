using System;
using System.Collections.Generic;

namespace QbcBackend.Molecules.Entities
{
    public partial class MoleculeElPot
    {
        public int Id { get; set; }
        public int MoleculeId { get; set; }
        public int Type { get; set; }
        public decimal PosX { get; set; }
        public decimal PosY { get; set; }
        public decimal PosZ { get; set; }
        public decimal Nuclear { get; set; }
        public decimal Electronic { get; set; }
        public decimal Total { get; set; }
        public byte[] Timestamp { get; set; }

        public virtual Molecule Molecule { get; set; }
    }
}
