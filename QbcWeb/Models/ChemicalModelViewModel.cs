using QbcBackend.Molecules.Model.Molecule;
using QbcBackend.Molecules.Model.MoleculeCalculation;
using System.Collections.Generic;

namespace QbcWeb.Models
{
    public class ChemicalModelViewModel
    {


        public ChemicalModelViewModel()
        {
            this.Molecules = new List<MoleculeInfo>();
            this.Calculations = new List<ChemicalCalculation>();
        }


        /// <summary>
        /// The name of the project
        /// </summary>
        public string ProjectName
        {
            get;
            set;
        }

        /// <summary>
        /// Id of the model
        /// </summary>
        public int Id
        {
            get;
            set;
        }

        /// <summary>
        /// The name of the model
        /// </summary>
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// The description of the model
        /// </summary>
        public string Description
        {
            get;
            set;
        }

        /// <summary>
        /// the charge of the model
        /// </summary>
        public int Charge
        {
            get;
            set;
        }


        /// <summary>
        /// The initial geometry of the model.
        /// </summary>
        public string InitialGeometry
        {
            get;
            set;
        }

        /// <summary>
        /// The final geometry
        /// </summary>
        public string CurrentGeometry
        {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string CanDelete
        {
            get
            {
                return this.Calculations.Count == 0 && this.Molecules.Count == 0 ? "Visible" : "Invisible";
            }
        }

        /// <summary>
        /// List of associated calculation
        /// </summary>
        public List<ChemicalCalculation> Calculations
        {
            get;
            set;
        }

        /// <summary>
        /// List of assiciated molecules
        /// </summary>
        public List<MoleculeInfo> Molecules
        {
            get;
            set;
        }

    }
}
