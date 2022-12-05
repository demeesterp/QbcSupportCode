using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QbcBlazorWeb.InputModel
{
    public class MoleculeInputModel
    {
        public MoleculeInputModel()
        {
            Atoms = new List<MoleculeAtomInputModel>();
            Bonds = new List<MoleculeBondInputModel>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Comment { get; set; }

        public string Geometry { get; set; }

        public int ModelId { get; set; }

        public string ModelName { get; set; }


        #region molecule properties

        public int? Charge
        {
            get;
            set;
        }

        public int? Multiplicity
        {
            get;
            set;
        }

        public decimal? DftEnergy
        {
            get;
            set;
        }

        public decimal? HfEnergy
        {
            get;
            set;
        }

        public int? NAlphaOrbitals
        {
            get;
            set;
        }

        public int? NBetaOrbitals
        {
            get;
            set;
        }

        public decimal? ElectronAffinity
        {
            get;
            set;
        }

        public decimal? Hardness
        {
            get;
            set;
        }

        #endregion


        #region atoms

        public List<MoleculeAtomInputModel> Atoms
        {
            get;
            set;
        }

        #endregion


        #region bonds

        public List<MoleculeBondInputModel> Bonds
        {
            get;
            set;
        }

        #endregion
    }
}
