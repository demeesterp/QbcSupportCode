using System;
using System.Collections.Generic;

namespace QbcBackend.Molecules.Entities
{
    public partial class BotanicalName
    {
        public BotanicalName()
        {
            BotanicalNameAttribute = new HashSet<BotanicalNameAttribute>();
            InverseBotanicalNameNavigation = new HashSet<BotanicalName>();
        }

        public int Id { get; set; }
        public int? BotanicalNameId { get; set; }
        public int BotanicalNameTypeId { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public virtual BotanicalName BotanicalNameNavigation { get; set; }
        public virtual BotanicalNameType BotanicalNameType { get; set; }
        public virtual ICollection<BotanicalNameAttribute> BotanicalNameAttribute { get; set; }
        public virtual ICollection<BotanicalName> InverseBotanicalNameNavigation { get; set; }
    }
}
