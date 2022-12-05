using QbcBackend.Tools.Base.Model;

namespace QbcBackend.Molecules.Model.Molecule
{
    public class MoleculeBond : QbcBase
    {

        public int Atom1Position
        {
            get;
            set;
        }

        public int Atom2Position
        {
            get;
            set;
        }

        public decimal Distance
        {
            get;
            set;
        }

        public decimal BondOrder
        {
            get;
            set;
        }

        public decimal BondOrderAcid
        {
            get;
            set;
        }

        public decimal BondOrderBase
        {
            get;
            set;
        }

        public decimal OverlapPopulation
        {
            get;
            set;
        }

        public decimal OverlapPopulationAcid
        {
            get;
            set;
        }

        public decimal OverlapPopulationBase
        {
            get;
            set;
        }

    }
}
