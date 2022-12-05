using System;
using System.Collections.Generic;

namespace QbcBackend.Molecules.Entities
{
    public partial class MoleculeProperty
    {
        public int Id { get; set; }
        public int AppId { get; set; }
        public int MoleculeId { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string Data { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
        public byte[] Timestamp { get; set; }

        public virtual Molecule Molecule { get; set; }
    }
}
