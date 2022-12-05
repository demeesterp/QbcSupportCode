using QbcWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QbcWeb.Models
{
    public class MoleculeViewModel
    {

        public MoleculeViewModel()
        {
            Atoms = new List<MoleculeAtomViewModel>();
            Bonds = new List<MoleculeBondViewModel>();
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

        public List<MoleculeAtomViewModel> Atoms
        {
            get;
            set;
        }

        #endregion


        #region bonds

        public List<MoleculeBondViewModel> Bonds
        {
            get;
            set;
        }

        #endregion


    }
}
