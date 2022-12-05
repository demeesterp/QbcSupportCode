using System;
using System.Collections.Generic;

namespace QbcBackend.Molecules.Entities
{
    public partial class FtpConfig
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Host { get; set; }
        public string InputFileDir { get; set; }
        public string OutputFileDir { get; set; }
        public string ArchiveFileDir { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
        public byte[] Timestamp { get; set; }
    }
}
