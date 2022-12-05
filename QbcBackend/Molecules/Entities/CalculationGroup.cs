using System;
using System.Collections.Generic;

namespace QbcBackend.Molecules.Entities
{
    public partial class CalculationGroup
    {
        public int Id { get; set; }
        public int AppId { get; set; }
        public string Name { get; set; }
        public int? ParentCalcId { get; set; }
        public int CalcId { get; set; }
        public int Type { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
        public byte[] Timestamp { get; set; }

        public virtual Calculation Calc { get; set; }
    }
}
