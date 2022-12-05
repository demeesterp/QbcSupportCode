using System;
using System.Collections.Generic;

namespace QbcBackend.Molecules.Entities
{
    public partial class MoleculeModel
    {
        public MoleculeModel()
        {
            BotanicalNameAttribute = new HashSet<BotanicalNameAttribute>();
            Calculation = new HashSet<Calculation>();
            Molecule = new HashSet<Molecule>();
        }

        public int Id { get; set; }
        public int CategoryId { get; set; }
        public int AppId { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public int Charge { get; set; }
        public int Type { get; set; }
        public string InitialGeometry { get; set; }
        public string CurrentGeometry { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
        public byte[] Timestamp { get; set; }

        public virtual Category Category { get; set; }
        public virtual ICollection<BotanicalNameAttribute> BotanicalNameAttribute { get; set; }
        public virtual ICollection<Calculation> Calculation { get; set; }
        public virtual ICollection<Molecule> Molecule { get; set; }
    }
}
