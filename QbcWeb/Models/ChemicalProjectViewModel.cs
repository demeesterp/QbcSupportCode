using QbcBackend.Molecules.Model.MoleculeProject;
using System.Collections.Generic;

namespace QbcWeb.Models
{
    public class ChemicalProjectViewModel
    {

        public ChemicalProjectViewModel()
        {
            this.Models = new List<ChemicalModelViewModel>();
        }

        public ChemicalProject Project
        {
            get;
            set;
        }

        public List<ChemicalModelViewModel> Models
        {
            get;
            set;
        }


        public string CanDelete
        {
            get
            {
                return this.Models.Count == 0 ? "Visible" : "Invisible";
            }
        }
    }
}
