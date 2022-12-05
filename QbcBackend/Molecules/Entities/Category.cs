using System;
using System.Collections.Generic;

namespace QbcBackend.Molecules.Entities
{
    public partial class Category
    {
        public Category()
        {
            InverseCategoryNavigation = new HashSet<Category>();
            MoleculeModel = new HashSet<MoleculeModel>();
        }

        public int Id { get; set; }
        public int Type { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public int? CategoryId { get; set; }
        public int AppId { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
        public byte[] Timestamp { get; set; }

        public virtual Category CategoryNavigation { get; set; }
        public virtual ICollection<Category> InverseCategoryNavigation { get; set; }
        public virtual ICollection<MoleculeModel> MoleculeModel { get; set; }
    }
}
