using System;
using System.Collections.Generic;

namespace QbcBackend.Molecules.Entities
{
    public partial class Calculation
    {
        public Calculation()
        {
            CalculationGroup = new HashSet<CalculationGroup>();
            Molecule = new HashSet<Molecule>();
        }

        public int Id { get; set; }
        public int ModelId { get; set; }
        public int AppId { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public int Type { get; set; }
        public int? BasisSetId { get; set; }
        public string GmsInput { get; set; }
        public int ExecutionStatus { get; set; }
        public bool HasParent { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
        public byte[] Timestamp { get; set; }

        public virtual BasisSet BasisSet { get; set; }
        public virtual MoleculeModel Model { get; set; }
        public virtual ICollection<CalculationGroup> CalculationGroup { get; set; }
        public virtual ICollection<Molecule> Molecule { get; set; }
    }
}
