using QbcBackend.Molecules.Entities;
using QbcBackend.Molecules.Model.Molecule;
using QbcBackend.Tools.StringConversion;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QbcBackend.Molecules.Conversion
{
    public static class QbcMoleculesConverter
    {
        public static ElPot Convert(MoleculeElPot input)
        {
            return new ElPot()
            {
                Electronic = input.Electronic,
                Id = input.Id,
                MoleculeID = input.MoleculeId,
                Nuclear = input.Nuclear,
                PosX = input.PosX,
                PosY = input.PosY,
                PosZ = input.PosZ,
                Total = input.Total,
                Type = input.Type,
                Timestamp = input.Timestamp
            };
        }

        public static MoleculeElPot Convert(ElPot input)
        {
            return new MoleculeElPot()
            {
                Electronic = input.Electronic,
                Id = input.Id,
                MoleculeId = input.MoleculeID,
                Nuclear = input.Nuclear,
                PosX = input.PosX,
                PosY = input.PosY,
                PosZ = input.PosZ,
                Total = input.Total,
                Type = input.Type,
                Timestamp = input.Timestamp
            };
        }

        public static Bond Convert(MoleculeBond input)
        {
            return new Bond()
            {
                AppId = 0,
                Atom1Position = input.Atom1Position,
                Atom2Position = input.Atom2Position,
                BondOrder = input.BondOrder,
                BondOrderAcid = input.BondOrderAcid,
                BondOrderBase = input.BondOrderBase,
                Distance = input.Distance,
                OverlapPopulation = input.OverlapPopulation,
                OverlapPopulationAcid = input.OverlapPopulationAcid,
                OverlapPopulationBase = input.OverlapPopulationBase,
            };
        }

        public static MoleculeBond Convert(Bond input)
        {
            return new MoleculeBond()
            {
                Atom1Position = input.Atom1Position,
                Atom2Position = input.Atom2Position,
                BondOrder = input.BondOrder.GetValueOrDefault(),
                BondOrderAcid = input.BondOrderAcid.GetValueOrDefault(),
                BondOrderBase = input.BondOrderBase.GetValueOrDefault(),
                Distance = input.Distance.GetValueOrDefault(),
                OverlapPopulation = input.OverlapPopulation.GetValueOrDefault(),
                OverlapPopulationAcid = input.OverlapPopulationAcid.GetValueOrDefault(),
                OverlapPopulationBase = input.OverlapPopulationBase.GetValueOrDefault()
            };
        }

        public static Atom Convert(MoleculeAtom input)
        {
            return new Atom()
            {
                AppId = 0,
                AtomicWeight = input.AtomicWeight,
                AtomOrbital = input.Orbitals?.ConvertAll(i => Convert(i)),
                ChelpGcharge = input.CHelpGCharge,
                ConnollyCharge = input.ConnollyCharge,
                GeoDiscCharge = input.GeoDiscCharge,
                Id = input.Id,
                LowdinPopulation = input.LowdinPopulation,
                LowdinPopulationAcid = input.LowdinPopulationAcid,
                LowdinPopulationBase = input.LowdinPopulationBase,
                MullikenPopulation = input.MullikenPopulation,
                MullikenPopulationAcid = input.MullikenPopulationAcid,
                MullikenPopulationBase = input.MullikenPopulationBase,
                Number = input.Number,
                Position = input.Position,
                PosX = input.PosX,
                PosY = input.PosY,
                PosZ = input.PosZ,
                Symbol = input.Symbol,
                Radius = input.Radius
            };
        }

        public static MoleculeAtom Convert(Atom input)
        {
            return new MoleculeAtom()
            {
                AtomicWeight = input.AtomicWeight.GetValueOrDefault(),
                Orbitals = input.AtomOrbital?.ToList().ConvertAll(i => Convert(i)),
                CHelpGCharge = input.ChelpGcharge.GetValueOrDefault(),
                ConnollyCharge = input.ConnollyCharge.GetValueOrDefault(),
                GeoDiscCharge = input.GeoDiscCharge.GetValueOrDefault(),
                Id = input.Id,
                LowdinPopulation = input.LowdinPopulation.GetValueOrDefault(),
                LowdinPopulationAcid = input.LowdinPopulationAcid.GetValueOrDefault(),
                LowdinPopulationBase = input.LowdinPopulationBase.GetValueOrDefault(),
                MullikenPopulation = input.MullikenPopulation.GetValueOrDefault(),
                MullikenPopulationAcid = input.MullikenPopulationAcid.GetValueOrDefault(),
                MullikenPopulationBase = input.MullikenPopulationBase.GetValueOrDefault(),
                Number = input.Number,
                Position = input.Position,
                PosX = input.PosX,
                PosY = input.PosY,
                PosZ = input.PosZ,
                Symbol = input.Symbol,
                Radius = input.Radius.GetValueOrDefault()
            };
        }

        public static AtomOrbital Convert(MoleculeAtomOrbital input)
        {
            return new AtomOrbital() 
            {
                AppId = 0,
                Id = input.Id,
                LowdinPopulation = input.LowdinPopulation,
                LowdinPopulationAcid = input.LowdinPopulationAcid,
                LowdinPopulationBase = input.LowdinPopulationBase,
                MullikenPopulation = input.MullikenPopulation,
                MullikenPopulationAcid = input.MullikenPopulationAcid,
                MullikenPopulationBase = input.MullikenPopulationBase,
                Position = input.Position,
                Symbol = input.Symbol
            };
        }

        public static MoleculeAtomOrbital Convert(AtomOrbital input)
        {
            return new MoleculeAtomOrbital()
            {
                Id = input.Id,
                LowdinPopulation = input.LowdinPopulation,
                LowdinPopulationAcid = input.LowdinPopulationAcid,
                LowdinPopulationBase = input.LowdinPopulationBase,
                MullikenPopulation = input.MullikenPopulation,
                MullikenPopulationAcid = input.MullikenPopulationAcid,
                MullikenPopulationBase = input.MullikenPopulationBase,
                Position = input.Position,
                Symbol = input.Symbol
            };
        }

        public static Molecule Convert(MoleculeInfo input)
        {
            Molecule retval = new Molecule()
            {
                Comment = input.Comment,
                Description = input.Description,
                ErrorStatus = (int)input.Status,
                Id = input.Id,
                ModelId = input.ModelId,
                Name = input.NameInfo,
                ParentCalculationId = input.ParentCalculationId
            };

            // Molecule Properties
            retval.MoleculeProperty = new List<MoleculeProperty>();
            if (input.Charge.HasValue)
            {
                retval.MoleculeProperty.Add(new MoleculeProperty()
                {
                    Code = nameof(input.Charge),
                    Data = input.Charge.ToString(),
                });
            }

            if (input.Multiplicity.HasValue)
            {
                retval.MoleculeProperty.Add(new MoleculeProperty()
                {
                    Code = nameof(input.Multiplicity),
                    Data = input.Multiplicity.ToString(),
                });
            }

            if (input.DftEnergy.HasValue)
            {
                retval.MoleculeProperty.Add(new MoleculeProperty()
                {
                    Code = nameof(input.DftEnergy),
                    Data = input.DftEnergy.ToString(),
                });
            }


            if (input.HFEnergy.HasValue)
            {
                retval.MoleculeProperty.Add(new MoleculeProperty()
                {
                    Code = nameof(input.HFEnergy),
                    Data = input.HFEnergy.ToString(),
                });
            }

            if (input.NAlphaOrbitals.HasValue)
            {
                retval.MoleculeProperty.Add(new MoleculeProperty()
                {
                    Code = nameof(input.NAlphaOrbitals),
                    Data = input.NAlphaOrbitals.ToString(),
                });
            }

            if (input.NBetaOrbitals.HasValue)
            {
                retval.MoleculeProperty.Add(new MoleculeProperty()
                {
                    Code = nameof(input.NBetaOrbitals),
                    Data = input.NBetaOrbitals.ToString(),
                });
            }

            if (input.ElectronAffinity.HasValue)
            {
                retval.MoleculeProperty.Add(new MoleculeProperty()
                {
                    Code = nameof(input.ElectronAffinity),
                    Data = input.ElectronAffinity.ToString(),
                });
            }

            if (input.Hardness.HasValue)
            {
                retval.MoleculeProperty.Add(new MoleculeProperty()
                {
                    Code = nameof(input.Hardness),
                    Data = input.Hardness.ToString(),
                });
            }

            // Bonds
            retval.Bond = new List<Bond>();
            foreach (MoleculeBond b in input.Bonds)
            {
                retval.Bond.Add(Convert(b));
            }

            // Atoms
            retval.Atom = new List<Atom>();
            foreach (MoleculeAtom a in input.Atoms)
            {
                var current = Convert(a);
                // AtomOrbital
                current.AtomOrbital = new List<AtomOrbital>();
                foreach (MoleculeAtomOrbital ao in a.Orbitals)
                {
                    current.AtomOrbital.Add(Convert(ao));
                }

                retval.Atom.Add(current);
            }

            // Elpot
            retval.MoleculeElPot = new List<MoleculeElPot>();
            foreach (ElPot e in input.ElPot)
            {
                retval.MoleculeElPot.Add(Convert(e));
            }

            return retval;
        }

        public static MoleculeInfo Convert(Molecule input)
        {
            MoleculeInfo retval = new MoleculeInfo()
            {
                NameInfo = input.Name,
                Comment = input.Comment,
                Description = input.Description,
                Status = (MoleculeErrorStatus)input.ErrorStatus,
                Id = input.Id,
                ModelId = input.ModelId,
                ParentCalculationId = input.ParentCalculationId

            };
            if (input.MoleculeProperty != null)
            {
                // Charge
                var result = input.MoleculeProperty.FirstOrDefault((i) => i.Code == nameof(retval.Charge));
                if (result != null && !String.IsNullOrWhiteSpace(result.Data))
                {
                    retval.Charge = int.Parse(result.Data);
                }

                // Multiplicity
                result = input.MoleculeProperty.FirstOrDefault((i) => i.Code == nameof(retval.Multiplicity));
                if (result != null && !String.IsNullOrWhiteSpace(result.Data))
                {
                    retval.Multiplicity = int.Parse(result.Data);
                }

                // DftEnergy
                result = input.MoleculeProperty.FirstOrDefault((i) => i.Code == nameof(retval.DftEnergy));
                if (result != null && !String.IsNullOrWhiteSpace(result.Data))
                {
                    retval.DftEnergy = QbcStringConvert.ToDecimal(result.Data);
                }

                // HFEnergy
                result = input.MoleculeProperty.FirstOrDefault((i) => i.Code == nameof(retval.HFEnergy));
                if (result != null && !String.IsNullOrWhiteSpace(result.Data))
                {
                    retval.HFEnergy = QbcStringConvert.ToDecimal(result.Data);
                }

                // NAlphaOrbitals
                result = input.MoleculeProperty.FirstOrDefault((i) => i.Code == nameof(retval.NAlphaOrbitals));
                if (result != null && !String.IsNullOrWhiteSpace(result.Data))
                {
                    retval.NAlphaOrbitals = int.Parse(result.Data);
                }

                // NBetaOrbitals
                result = input.MoleculeProperty.FirstOrDefault((i) => i.Code == nameof(retval.NBetaOrbitals));
                if (result != null && !String.IsNullOrWhiteSpace(result.Data))
                {
                    retval.NBetaOrbitals = int.Parse(result.Data);
                }

                // ElectronAffinity
                result = input.MoleculeProperty.FirstOrDefault((i) => i.Code == nameof(retval.ElectronAffinity));
                if (result != null && !String.IsNullOrWhiteSpace(result.Data))
                {
                    retval.ElectronAffinity = QbcStringConvert.ToDecimal(result.Data);
                }

                // Hardness
                result = input.MoleculeProperty.FirstOrDefault((i) => i.Code == nameof(retval.Hardness));
                if (result != null && !String.IsNullOrWhiteSpace(result.Data))
                {
                    retval.Hardness = QbcStringConvert.ToDecimal(result.Data);
                }
            }

            // Bonds
            retval.Bonds = new List<MoleculeBond>();
            foreach (Bond b in input.Bond)
            {
                retval.Bonds.Add(Convert(b));
            }

            // Elpot
            retval.ElPot = new List<ElPot>();
            foreach (MoleculeElPot e in input.MoleculeElPot)
            {
                retval.ElPot.Add(Convert(e));
            }

            // Atoms
            retval.Atoms = new List<MoleculeAtom>();
            MoleculeAtom current = null;
            foreach (Atom a in input.Atom)
            {
                current = Convert(a);

                // Add the atomOrbitals
                current.Orbitals = new List<MoleculeAtomOrbital>();
                foreach (var o in a.AtomOrbital)
                {
                    current.Orbitals.Add(Convert(o));
                }

                // Add the atombonds
                current.Bonds = new List<MoleculeBond>();
                Bond currentb = null;
                foreach (var b in a.BondAtom)
                {
                    currentb = input.Bond.FirstOrDefault((i) => i.Id == b.BondId);
                    if (currentb != null)
                    {
                        current.Bonds.Add(Convert(currentb));
                    }
                }

                retval.Atoms.Add(current);
            }
            return retval;
        }

    }
}
