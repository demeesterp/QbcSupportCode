using System;
using System.Collections.Generic;

namespace QbcBackend.Molecules.Entities
{
    public partial class Molecule
    {
        public Molecule()
        {
            Atom = new HashSet<Atom>();
            Bond = new HashSet<Bond>();
            MoleculeElPot = new HashSet<MoleculeElPot>();
            MoleculeProperty = new HashSet<MoleculeProperty>();
        }

        public int Id { get; set; }
        public int AppId { get; set; }
        public int ModelId { get; set; }
        public int ParentCalculationId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Comment { get; set; }
        public int ErrorStatus { get; set; }
        public bool IsModel { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
        public byte[] Timestamp { get; set; }

        public virtual MoleculeModel Model { get; set; }
        public virtual Calculation ParentCalculation { get; set; }
        public virtual ICollection<Atom> Atom { get; set; }
        public virtual ICollection<Bond> Bond { get; set; }
        public virtual ICollection<MoleculeElPot> MoleculeElPot { get; set; }
        public virtual ICollection<MoleculeProperty> MoleculeProperty { get; set; }
    }
}
