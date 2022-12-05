using QbcBackend.Tools.Base.Model;
using System.Collections.Generic;

namespace QbcBackend.Molecules.Model.Molecule
{

    public enum MoleculeErrorStatus
    {
        NoInfo = 0,
        Ok = 1,
        Error = 2
    }


    public class MoleculeInfo : QbcBase
    {

        public MoleculeInfo()
        {
            Bonds = new List<MoleculeBond>();
            Atoms = new List<MoleculeAtom>();
            ElPot = new List<ElPot>();
        }

        public string NameInfo
        {
            get;
            set;
        }

        public string Description
        {
            get;
            set;
        }

        public string Comment
        {
            get;
            set;
        }

        public int ModelId
        {
            get;
            set;
        }

        public MoleculeErrorStatus Status
        {
            get;
            set;
        }

        public int ParentCalculationId
        {
            get;
            set;
        }


        public List<MoleculeBond> Bonds
        {
            get;
            set;
        }

        public List<MoleculeAtom> Atoms
        {
            get;
            set;
        }

        public List<ElPot> ElPot
        {
            get;
            set;
        }

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

        public decimal? HFEnergy
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

    }
}
