using System;
using System.Collections.Generic;

namespace QbcBackend.Molecules.Entities
{
    public partial class BotanicalNameType
    {
        public BotanicalNameType()
        {
            BotanicalName = new HashSet<BotanicalName>();
            InverseBotanicalNameTypeNavigation = new HashSet<BotanicalNameType>();
        }

        public int Id { get; set; }
        public int? BotanicalNameTypeId { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }

        public virtual BotanicalNameType BotanicalNameTypeNavigation { get; set; }
        public virtual ICollection<BotanicalName> BotanicalName { get; set; }
        public virtual ICollection<BotanicalNameType> InverseBotanicalNameTypeNavigation { get; set; }
    }
}
