using System;
using System.Collections.Generic;

namespace QbcBackend.Molecules.Entities
{
    public partial class ConfigurationData
    {
        public int Id { get; set; }
        public int AppId { get; set; }
        public string Name { get; set; }
        public string GmsInputDir { get; set; }
        public string GmsOutputDir { get; set; }
        public string GmsArchiveDir { get; set; }
        public string Host { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsDefault { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Updated { get; set; }
        public byte[] Timestamp { get; set; }
    }
}
