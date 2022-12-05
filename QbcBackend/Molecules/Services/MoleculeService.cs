using FluentValidation;
using QbcBackend.Molecules.Conversion;
using QbcBackend.Molecules.Entities;
using QbcBackend.Molecules.Model.Molecule;
using QbcBackend.Molecules.Repo;
using QbcBackend.Tools.QbcException;
using QbcBackend.Tools.StringConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QbcBackend.Molecules.Services
{
    public class MoleculeService : IMoleculeService
    {

        #region private members

        private IMoleculeRepository Repo
        {
            get;
        }

        private IAtomRepository AtomRepo
        {
            get;
        }

        private IBondRepository BondRepo
        {
            get;
        }

        private IBondAtomRepository BondAtomRepo
        {
            get;
        }

        private IAtomOrbitalRepository AtomOrbitalRepo
        {
            get;
        }

        private IMoleculePropertyRepository MoleculePropertyRepo
        {
            get;
        }

        private IMoleculeElpotRepository MoleculeElPotRepo
        {
            get;
        }

        private IModelRepository ModelRepo
        {
            get;
        }

        private IValidator<Bond> BondValidator
        {
            get;
        }

        private IValidator<BondAtom> BondAtomValidator
        {
            get;
        }

        private IValidator<Atom> AtomValidator
        {
            get;
        }

        private IValidator<AtomOrbital> AtomOrbitalValidator
        {
            get;
        }

        private IValidator<Molecule> Validator { get; }

        #endregion


        public MoleculeService(IMoleculeRepository repo,
                                  IAtomRepository atomRepo,
                                  IBondRepository bondRepo,
                                  IBondAtomRepository bondAtomRepo,
                                  IAtomOrbitalRepository atomOrbitalRepo,
                                  IMoleculePropertyRepository moleculePropertyRepo,
                                  IMoleculeElpotRepository moleculeElPotRepo,
                                  IModelRepository modelRepo,
                                  IValidator<Molecule> validator,
                                  IValidator<Bond> bondValidator,
                                  IValidator<Atom> atomValidator,
                                  IValidator<AtomOrbital> atomOrbitalValidator,
                                  IValidator<BondAtom> bondAtomValidator)
        {
            this.Repo = repo;
            this.ModelRepo = modelRepo;
            this.AtomRepo = atomRepo;
            this.BondRepo = bondRepo;
            this.BondAtomRepo = bondAtomRepo;
            this.AtomOrbitalRepo = atomOrbitalRepo;
            this.MoleculePropertyRepo = moleculePropertyRepo;
            this.MoleculeElPotRepo = moleculeElPotRepo;
            this.BondValidator = bondValidator;
            this.BondAtomValidator = bondAtomValidator;
            this.AtomValidator = atomValidator;
            this.AtomOrbitalValidator = atomOrbitalValidator;
            this.Validator = validator;
        }


        #region public methods


        public async Task<MoleculeInfo> CreateAsync(MoleculeInfo molecule)
        {
            if (molecule == null) throw new ArgumentNullException("molecule", "Null input values are not accepted !");
            var model = await this.ModelRepo.GetByIdAsync(molecule.ModelId);
            if (model == null)
            {
                throw new NotExistsException($"The model {molecule.ModelId} does not exist the molecule cannot be added to the model !");
            }

            Molecule toCreate = QbcMoleculesConverter.Convert(molecule);

            this.Validate(toCreate);

            PrepareForCreate(toCreate);

            toCreate = this.Repo.Add(toCreate);

            await Repo.SaveChangesAsync();

            foreach (var atom in toCreate.Atom)
            {
                atom.BondAtom = new List<BondAtom>();
                foreach (Bond ba in (from i in toCreate.Bond where i.Atom1Position == atom.Position select i))
                {
                    atom.BondAtom.Add(new BondAtom()
                    {
                        AtomId = atom.Id,
                        BondId = ba.Id
                    });
                }
            }

            await Repo.SaveChangesAsync();
            return molecule;
        }

        public async Task UpdateAsync(int id, MoleculeInfo molecule)
        {
            var toUpdate = await this.Repo.GetByIdAsync(id);
            if (toUpdate == null)
            {
                throw new NotExistsException($"The molecule {id} does not exist and cannot be updated.");
            }

            toUpdate.Comment = molecule.Comment;
            toUpdate.Description = molecule.Description;
            toUpdate.Updated = DateTime.Now;

            toUpdate.Name = molecule.NameInfo;
            toUpdate.ErrorStatus = (int)molecule.Status;

            // Update the properties
            if (toUpdate.MoleculeProperty != null)
            {
                // Charge
                var propresult = toUpdate.MoleculeProperty.FirstOrDefault((i) => i.Code == nameof(molecule.Charge));
                if (propresult != null)
                {
                    propresult.Data = molecule.Charge.ToString();
                }
                else
                {
                    toUpdate.MoleculeProperty.Add(new MoleculeProperty()
                    {
                        Code = nameof(molecule.Charge),
                        Data = molecule.Charge.ToString(),
                    });
                }

                // Multiplicity
                propresult = toUpdate.MoleculeProperty.FirstOrDefault((i) => i.Code == nameof(molecule.Multiplicity));
                if (propresult != null)
                {
                    propresult.Data = molecule.Multiplicity.ToString();
                }
                else
                {
                    toUpdate.MoleculeProperty.Add(new MoleculeProperty()
                    {
                        Code = nameof(molecule.Multiplicity),
                        Data = molecule.Multiplicity.ToString(),
                    });
                }

                // DftEnergy
                propresult = toUpdate.MoleculeProperty.FirstOrDefault((i) => i.Code == nameof(molecule.DftEnergy));
                if (propresult != null)
                {
                    propresult.Data = molecule.DftEnergy.ToString();
                }
                else
                {
                    toUpdate.MoleculeProperty.Add(new MoleculeProperty()
                    {
                        Code = nameof(molecule.DftEnergy),
                        Data = molecule.DftEnergy.ToString(),
                    });
                }

                // NAlphaOrbitals
                propresult = toUpdate.MoleculeProperty.FirstOrDefault((i) => i.Code == nameof(molecule.NAlphaOrbitals));
                if (propresult != null)
                {
                    propresult.Data = molecule.NAlphaOrbitals.ToString();
                }
                else
                {
                    toUpdate.MoleculeProperty.Add(new MoleculeProperty()
                    {
                        Code = nameof(molecule.NAlphaOrbitals),
                        Data = molecule.NAlphaOrbitals.ToString(),
                    });
                }

                // NBetaOrbitals
                propresult = toUpdate.MoleculeProperty.FirstOrDefault((i) => i.Code == nameof(molecule.NBetaOrbitals));
                if (propresult != null)
                {
                    propresult.Data = molecule.NBetaOrbitals.ToString();
                }
                else
                {
                    toUpdate.MoleculeProperty.Add(new MoleculeProperty()
                    {
                        Code = nameof(molecule.NBetaOrbitals),
                        Data = molecule.NBetaOrbitals.ToString(),
                    });
                }

                // ElectronAffinity
                propresult = toUpdate.MoleculeProperty.FirstOrDefault((i) => i.Code == nameof(molecule.ElectronAffinity));
                if (propresult != null)
                {
                    propresult.Data = molecule.ElectronAffinity.ToString();
                }
                else
                {
                    toUpdate.MoleculeProperty.Add(new MoleculeProperty()
                    {
                        Code = nameof(molecule.ElectronAffinity),
                        Data = molecule.ElectronAffinity.ToString(),
                    });
                }

                // Hardness
                propresult = toUpdate.MoleculeProperty.FirstOrDefault((i) => i.Code == nameof(molecule.Hardness));
                if (propresult != null)
                {
                    propresult.Data = molecule.Hardness.ToString();
                }
                else
                {
                    toUpdate.MoleculeProperty.Add(new MoleculeProperty()
                    {
                        Code = nameof(molecule.Hardness),
                        Data = molecule.Hardness.ToString(),
                    });
                }
            }


            await Repo.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var result = await this.Repo.GetByIdAsync(id);

            if (result == null)
            {
                throw new NotExistsException($"The molecule {id} does not exist and cannot be deleted.");
            }

            // Delete all atoms
            foreach (var atom in result.Atom)
            {
                foreach (var atomBond in atom.BondAtom)
                {
                    this.BondAtomRepo.Remove(atomBond.Id);
                }

                foreach (var atomOrbital in atom.AtomOrbital)
                {
                    this.AtomOrbitalRepo.Remove(atomOrbital.Id);
                }
                this.AtomRepo.Remove(atom.Id);
            }

            // Delete all bonds
            foreach (var bond in result.Bond)
            {
                this.BondRepo.Remove(bond.Id);
            }

            // Delete all properties
            foreach (var property in result.MoleculeProperty)
            {
                this.MoleculePropertyRepo.Remove(property.Id);
            }

            // Delete all elpot
            foreach(var elpot in result.MoleculeElPot)
            {
                this.MoleculeElPotRepo.Remove(elpot.Id);
            }


            this.Repo.Remove(result.Id);
            await Repo.SaveChangesAsync();
        }

        public async Task<ICollection<MoleculeInfo>> GetByModelID(int modelid)
        {
            List<MoleculeInfo> retval = new List<MoleculeInfo>();
            foreach (var m in await this.Repo.GetByModelAsync(modelid))
            {
                retval.Add(QbcMoleculesConverter.Convert(m));
            }
            return retval;
        }

        public async Task<MoleculeInfo> GetById(int id)
        {
            Molecule result = await this.Repo.GetByIdAsync(id);
            result.Atom = await this.AtomRepo.GetAtomByMolecule(id);
            result.Bond = await this.BondRepo.GetBondForMolecule(id);
            result.MoleculeElPot = await this.MoleculeElPotRepo.GetByMoleculeAsync(id);
            return QbcMoleculesConverter.Convert(result);
        }

        public async Task<ICollection<MoleculeInfo>> GetByCalculationID(int calculationid)
        {
            List<MoleculeInfo> retval = new List<MoleculeInfo>();
            foreach (var m in await this.Repo.GetByParentCalculation(calculationid))
            {
                m.Atom = await this.AtomRepo.GetAtomByMolecule(m.Id);
                m.Bond = await this.BondRepo.GetBondForMolecule(m.Id);
                m.MoleculeElPot = await this.MoleculeElPotRepo.GetByMoleculeAsync(m.Id);
                retval.Add(QbcMoleculesConverter.Convert(m));
            }
            return retval;
        }

        #endregion


        #region static helpers

        public static void PrepareForCreate(Molecule molecule)
        {
            DateTime createDate = DateTime.Now;
            molecule.Id = 0;
            molecule.Created = createDate;

            foreach(var atom in molecule.Atom)
            {

                foreach(var orb in atom.AtomOrbital)
                {
                    orb.Id = 0;
                    orb.Created = createDate;
                }

                atom.BondAtom.Clear();

                atom.Id = 0;
                atom.Created = createDate;
            }

            foreach(var bond in molecule.Bond)
            {
                bond.Id = 0;
                bond.Created = createDate;
            }

            foreach(var prop in molecule.MoleculeProperty)
            {
                prop.Id = 0;
                prop.Created = createDate;
            }

        }


        #endregion

        protected void Validate(object input)
        {
            Molecule toValidate = (Molecule)input;
            Validator.Validate(toValidate);
            foreach( Bond b in toValidate.Bond)
            {
                this.BondValidator.Validate(b);
            }

            foreach(Atom a in toValidate.Atom)
            {
                this.AtomValidator.Validate(a);

                foreach(BondAtom ba in a.BondAtom)
                {
                    this.BondAtomValidator.Validate(ba);
                }

                foreach(AtomOrbital ao in a.AtomOrbital)
                {
                    this.AtomOrbitalValidator.Validate(ao);
                }
            }
        }
    }
}
