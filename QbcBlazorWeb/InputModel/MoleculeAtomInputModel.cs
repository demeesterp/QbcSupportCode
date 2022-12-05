using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QbcBlazorWeb.InputModel
{
    public class MoleculeAtomInputModel
    {
        public MoleculeAtomInputModel()
        {
            this.AtomOrbitals = new List<MoleculeAtomOrbitalInputModel>();
        }

        public int Position
        {
            get;
            set;
        }

        public int Number
        {
            get;
            set;
        }

        public string Symbol
        {
            get;
            set;
        }

        public int AtomicWeight
        {
            get;
            set;
        }

        public decimal MullikenCharge
        {
            get;
            set;
        }

        public decimal MullikenPopulationAcid
        {
            get;
            set;
        }

        public decimal MullikenPopulationBase
        {
            get;
            set;
        }

        public decimal LowdinCharge
        {
            get;
            set;
        }

        public decimal LowdinPopulationAcid
        {
            get;
            set;
        }

        public decimal LowdinPopulationBase
        {
            get;
            set;
        }

        public decimal CHelpGCharge
        {
            get;
            set;
        }

        public decimal GeoDiscCharge
        {
            get;
            set;
        }


        #region atomorbitals

        public List<MoleculeAtomOrbitalInputModel> AtomOrbitals
        {
            get;
            set;
        }

        #endregion
    }
}
