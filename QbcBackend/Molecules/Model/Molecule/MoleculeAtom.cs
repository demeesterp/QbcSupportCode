﻿using QbcBackend.Tools.Base.Model;
using System.Collections.Generic;

namespace QbcBackend.Molecules.Model.Molecule
{
    public class MoleculeAtom : QbcBase
    {

        public MoleculeAtom()
        {
            Orbitals = new List<MoleculeAtomOrbital>();
            Bonds = new List<MoleculeBond>();
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

        public MoleculeAtomInfo Info
        {
            get;
            set;
        }

        public decimal PosX
        {
            get;
            set;
        }

        public decimal PosY
        {
            get;
            set;
        }

        public decimal PosZ
        {
            get;
            set;
        }

        public decimal Radius
        {
            get;
            set;
        }

        public decimal MullikenPopulation
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

        public decimal LowdinPopulation
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

        public decimal ConnollyCharge
        {
            get;
            set;
        }

        public decimal GeoDiscCharge
        {
            get;
            set;
        }


        public List<MoleculeAtomOrbital> Orbitals
        {
            get;
            set;
        }

        public List<MoleculeBond> Bonds
        {
            get;
            set;
        }




    }
}
