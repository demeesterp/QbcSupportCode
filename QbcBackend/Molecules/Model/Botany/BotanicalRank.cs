using QbcBackend.Tools.Base.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace QbcBackend.Molecules.Model.Botany
{
    public class BotanicalRank : QbcBase
    {
        public string Name
        {
            get;
            set;
        }

        public string Description
        {
            get;
            set;
        }

        public string ParentName
        {
            get;
            set;
        }

    }
}
