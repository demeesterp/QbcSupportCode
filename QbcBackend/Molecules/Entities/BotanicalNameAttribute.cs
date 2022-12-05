using System;
using System.Collections.Generic;

namespace QbcBackend.Molecules.Entities
{
    public partial class BotanicalNameAttribute
    {
        public int Id { get; set; }
        public int? BotanicalNameId { get; set; }
        public int? MoleculeModelId { get; set; }
        public string Code { get; set; }

        public virtual BotanicalName BotanicalName { get; set; }
        public virtual MoleculeModel MoleculeModel { get; set; }
    }
}
