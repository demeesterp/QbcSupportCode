using System;
using System.Collections.Generic;

namespace QbcBackend.Molecules.Entities
{
    public partial class AtomInfo
    {
        public int Id { get; set; }
        public int AppId { get; set; }
        public string Symbol { get; set; }
        public string Name { get; set; }
        public int AtomNumber { get; set; }
        public decimal? AtomMass { get; set; }
        public decimal? AtomRadius { get; set; }
        public decimal? ElectroNegativity { get; set; }
        public decimal? ElectronAffinity { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
        public byte[] Timestamp { get; set; }
    }
}
