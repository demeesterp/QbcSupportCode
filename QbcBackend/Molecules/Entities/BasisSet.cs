using System;
using System.Collections.Generic;

namespace QbcBackend.Molecules.Entities
{
    public partial class BasisSet
    {
        public BasisSet()
        {
            Calculation = new HashSet<Calculation>();
        }

        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string GmsInput { get; set; }
        public int? AppId { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
        public byte[] Timestamp { get; set; }

        public virtual ICollection<Calculation> Calculation { get; set; }
    }
}
