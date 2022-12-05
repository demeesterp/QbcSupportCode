using QbcBackend.Molecules.Model.MoleculeCalculation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QbcBlazorWeb.InputModel
{
    public class ChemicalCalculationInputModel
    {
        public int Id { get; set; }

        public string ChemicalModelName { get; set; }

        public int ModelId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public CalculationType CalculationType { get; set; }

        public ExecutionStatus ExecutionStatus { get; set; }

        public int SelectedBasisSet { get; set; }

        //public List<SelectListItem> BasisSets { get; set; }

        public string GamessInput { get; set; }
    }
}
