using System;
using System.Collections.Generic;

namespace QbcBackend.Molecules.Entities
{
    public partial class AtomOrbital
    {
        public int Id { get; set; }
        public int AppId { get; set; }
        public int AtomId { get; set; }
        public int Position { get; set; }
        public string Symbol { get; set; }
        public decimal? MullikenPopulation { get; set; }
        public decimal? MullikenPopulationAcid { get; set; }
        public decimal? MullikenPopulationBase { get; set; }
        public decimal? LowdinPopulation { get; set; }
        public decimal? LowdinPopulationAcid { get; set; }
        public decimal? LowdinPopulationBase { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
        public byte[] Timestamp { get; set; }

        public virtual Atom Atom { get; set; }
    }
}
