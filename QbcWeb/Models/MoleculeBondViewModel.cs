﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QbcWeb.Models
{
    public class MoleculeBondViewModel
    {
        public string BondLabel { get; set; }

        public decimal Distance { get; set; }

        public decimal BondOrder { get; set; }

        public decimal BondOrderAcid { get; set; }

        public decimal BondOrderBase { get; set; }

        public decimal OverlapPopulation { get; set; }

        public decimal OverlapPopulationAcid { get; set; }

        public decimal OverlapPopulationBase { get; set; }

    }
}
