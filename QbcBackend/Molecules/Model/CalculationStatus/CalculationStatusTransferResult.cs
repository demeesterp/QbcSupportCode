using QbcBackend.Molecules.Model.Molecule;
using QbcBackend.Molecules.Model.MoleculeCalculation;
using QbcBackend.Tools.Base.Model;
using System.Collections.Generic;

namespace QbcBackend.Molecules.Model.CalculationStatus
{
    public class CalculationStatusTransferResult : QbcBase
    {


        public CalculationStatusTransferResult()
        {
            this.Molecules = new List<MoleculeInfo>();
        }


        public ExecutionStatus StatusAfterTransfer
        {
            get;
            set;
        }


        public string ErrorMessage
        {
            get;
            set;
        }

        public List<MoleculeInfo> Molecules
        {
            get; set;
        }


    }
}
