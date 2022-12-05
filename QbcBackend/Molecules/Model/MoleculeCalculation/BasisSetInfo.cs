using QbcBackend.Tools.Base.Model;

namespace QbcBackend.Molecules.Model.MoleculeCalculation
{
    public class BasisSetInfo : QbcBase
    {
        public string Code
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public string GmsInput
        {
            get;
            set;
        }

    }
}
