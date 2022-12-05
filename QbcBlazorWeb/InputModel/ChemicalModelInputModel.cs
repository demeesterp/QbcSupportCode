using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QbcBlazorWeb.InputModel
{
    public class ChemicalModelInputModel
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Charge { get; set; }
        public string InitialGeometry { get; set; }
        public string CurrentGeometry { get; set; }
    }
}
