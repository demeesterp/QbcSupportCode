using System;
using System.Collections.Generic;

namespace QbcBackend.Molecules.Entities
{
    public partial class BondAtom
    {
        public int Id { get; set; }
        public int AppId { get; set; }
        public int BondId { get; set; }
        public int AtomId { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
        public byte[] Timestamp { get; set; }

        public virtual Atom Atom { get; set; }
        public virtual Bond Bond { get; set; }
    }
}
