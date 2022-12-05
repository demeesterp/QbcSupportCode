using QbcBackend.Tools.Base.Model;

namespace QbcBackend.Molecules.Model.MoleculeCalculation
{
    public class ChemicalCalculationGroup : QbcBase
    {
        public string Name
        {
            get;
            set;
        }

        public int? ParentCalculationID
        {
            get;
            set;
        }

        public ChemicalCalculation Calculation
        {
            get;
            set;
        }


    }
}
