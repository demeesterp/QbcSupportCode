using QbcBackend.Molecules.Model.Molecule;
using QbcBackend.Molecules.Model.MoleculeCalculation;
using System.Collections.Generic;

namespace QbcBackend.Molecules.Model.MoleculeModel
{
    /// <summary>
    /// A conceptual model for a molecule
    /// </summary>
    public class ChemicalModel : ModelInfo
    {

        /// <summary>
        /// The calculation that have been done for this model
        /// </summary>
        public List<ChemicalCalculation> Calculations
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public List<MoleculeInfo> Molecules
        {
            get;
            set;
        }

    }
}
