using QbcBackend.Tools.Base.Model;
using System.Collections.Generic;

namespace QbcBackend.Molecules.Model.MoleculeCalculation
{

    public enum CalculationType
    {
        Optimization = 0,
        Fukui = 2,
        CHelpGCharges = 3,
        GeoDiskCharges = 4
    }

    public enum ExecutionStatus
    {
        Created = 0,
        Ready = 1,
        Executed = 3,
        Error = 4
    }

    public class ChemicalCalculation : QbcBase
    {

        public int ModelID
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public CalculationType Type
        {
            get;
            set;
        }

        public ExecutionStatus Status
        {
            get;
            set;
        }

        public string Code
        {
            get;
            set;
        }

        public string Description
        {
            get;
            set;
        }

        public string GmsInput
        {
            get;
            set;
        }

        public bool HasParent
        {
            get;
            set;
        }

        public BasisSetInfo BasisSet
        {
            get;
            set;
        }

        public List<ChemicalCalculation> RelatedCalculations
        {
            get;
            set;
        }


    }
}
