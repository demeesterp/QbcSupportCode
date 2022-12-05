using Microsoft.AspNetCore.Mvc.Rendering;
using QbcBackend.Molecules.Model.MoleculeCalculation;
using System.Collections.Generic;

namespace QbcWeb.Models
{
    public class ChemicalCalculationViewModel
    {


        public ChemicalCalculationViewModel()
        {
            this.BasisSets = new List<SelectListItem>();
        }


        public int Id { get; set; }

        public string ChemicalModelName { get; set; }

        public int ModelId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public CalculationType CalculationType { get; set; }

        public ExecutionStatus ExecutionStatus { get; set; }

        public int SelectedBasisSet { get; set; }

        public List<SelectListItem> BasisSets { get; set; }

        public string GamessInput { get; set; }


    }
}
