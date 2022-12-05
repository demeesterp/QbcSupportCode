using FluentValidation;
using QbcBackend.Molecules.Entities;
using QbcBackend.Molecules.Model.Molecule;
using QbcBackend.Molecules.Model.MoleculeCalculation;
using QbcBackend.Molecules.Model.MoleculeModel;
using QbcBackend.Molecules.Repo;
using QbcBackend.Tools.QbcException;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QbcBackend.Molecules.Services
{
    public class ChemicalModelService : IChemicalModelService
    {

        #region private properties

        private IModelRepository Repo { get; }

        private IMoleculeService MoleculeService { get; }

        private ICategoryRepository CategoryRepo {  get;}

        private IValidator<MoleculeModel> Validator { get; }

        #endregion



        public ChemicalModelService( IModelRepository repo 
                                            , IMoleculeService moleculeService
                                            , IValidator<MoleculeModel> validator
                                            , ICategoryRepository categoryRepo) 
        {
            this.Repo = repo;
            this.CategoryRepo = categoryRepo;
            this.Validator = validator;
            this.MoleculeService = moleculeService;
        }



        public async Task<ModelInfo> CreateAsync(ModelInfo model)
        {
            if (model == null) throw new ArgumentNullException("model", "Null input values are not accepted !");
            var project = await CategoryRepo.GetByIdAsync(model.ProjectID);
            if (project == null)
            {
                throw new NotExistsException($"The project {model.ProjectID} does not exist the model cannot be added to the project !");
            }

            MoleculeModel toCreate = new MoleculeModel()
            {
                Code = model.Name,
                Description = model.Description,
                CategoryId = model.ProjectID
            };

            this.Validator.Validate(toCreate);

            toCreate = this.Repo.Add(toCreate);

            await Repo.SaveChangesAsync();

            model.Id = toCreate.Id;

            return model;
        }

        public async Task DeleteAsync(int id)
        {
            this.Repo.Remove(id);

            await Repo.SaveChangesAsync();
        }

        public async Task AddBotanicalNameAsync(int modelId, string botanicalName)
        {
            await Task.CompletedTask;
        }

        private bool CompareMoleculeAtomPos(MoleculeAtom lhs, MoleculeAtom rhs)
        {

            var result = (lhs.PosX - rhs.PosX)*(lhs.PosX - rhs.PosX) +
                         (lhs.PosY - rhs.PosY)*(lhs.PosY - rhs.PosY) +
                         (lhs.PosZ - rhs.PosZ)*(lhs.PosZ - rhs.PosZ);

            return result < 0.01M;

        }

        public async Task<List<MoleculeInfo>> MergeMolecules(int modelid)
        {
            List<MoleculeInfo> retval = new List<MoleculeInfo>();

            ChemicalModel model = await this.Get(modelid);

            foreach (ChemicalCalculation optcalc in model.Calculations.FindAll(i => i.Type == CalculationType.Optimization))
            {
                MoleculeInfo current = new MoleculeInfo()
                {
                    NameInfo = "Merged for " + model.Name,
                    Charge = model.Charge,
                    Multiplicity = 1
                };

                var mol = (await this.MoleculeService.GetByCalculationID(optcalc.Id)).FirstOrDefault(i => !i.NameInfo.Contains("Merged for"));
                if (mol != null)
                {
                    foreach (var atom in mol.Atoms)
                    {
                        current.Atoms.Add(new MoleculeAtom()
                        {
                            Position = atom.Position,
                            Symbol = atom.Symbol,
                            AtomicWeight = atom.AtomicWeight,
                            Number = atom.Number,
                            PosX = atom.PosX,
                            PosY = atom.PosY,
                            PosZ = atom.PosZ
                        });
                    }
                }

                current.DftEnergy = mol.DftEnergy;

                var chargecalc = model.Calculations.FindAll(i => i.Type == CalculationType.CHelpGCharges || i.Type == CalculationType.GeoDiskCharges
                                                                && 
                                                              i.BasisSet.Id == optcalc.BasisSet.Id);               

                if (chargecalc != null)
                {
                    foreach(var c in chargecalc)
                    {
                        mol = (await this.MoleculeService.GetByCalculationID(c.Id)).FirstOrDefault();
                        if (mol != null)
                        {
                            foreach (var atom in mol.Atoms)
                            {
                                var currentAtom = current.Atoms.Find(i => i.Symbol == atom.Symbol && CompareMoleculeAtomPos(i, atom));
                                if (currentAtom != null)
                                {
                                    if ( c.Type == CalculationType.CHelpGCharges)
                                    {
                                        currentAtom.CHelpGCharge = atom.CHelpGCharge;
                                    }

                                    if ( c.Type == CalculationType.GeoDiskCharges)
                                    {
                                        currentAtom.GeoDiscCharge = atom.GeoDiscCharge;
                                    }
                                    
                                    currentAtom.ConnollyCharge = atom.ConnollyCharge;

                                }
                                else
                                {
                                    var mtoAdd = new MoleculeAtom()
                                    {
                                        Position = atom.Position,
                                        AtomicWeight = atom.AtomicWeight,
                                        Symbol = atom.Symbol,
                                        Number = atom.Number,
                                        PosX = atom.PosX,
                                        PosY = atom.PosY,
                                        PosZ = atom.PosZ
                                    };

                                    if (c.Type == CalculationType.CHelpGCharges)
                                    {
                                        mtoAdd.CHelpGCharge = atom.CHelpGCharge;
                                    }

                                    if (c.Type == CalculationType.GeoDiskCharges)
                                    {
                                        mtoAdd.GeoDiscCharge = atom.GeoDiscCharge;
                                    }

                                    mtoAdd.ConnollyCharge = atom.ConnollyCharge;

                                    current.Atoms.Add(mtoAdd);
                                }
                            }

                            foreach(var elPot in mol.ElPot)
                            {
                                current.ElPot.Add(new ElPot()
                                {
                                    PosX = elPot.PosX,
                                    PosY = elPot.PosY,
                                    PosZ = elPot.PosZ,
                                    Electronic = elPot.Electronic,
                                    Nuclear = elPot.Nuclear,
                                    Total = elPot.Total,
                                    Type = elPot.Type
                                });
                            }
                        }
                    }
                }

                var fukuicalc = model.Calculations.Find(i => i.Type == CalculationType.Fukui && i.BasisSet.Id == optcalc.BasisSet.Id);

                if (fukuicalc != null)
                {
                    mol = (await this.MoleculeService.GetByCalculationID(fukuicalc.Id)).FirstOrDefault();
                    if (mol != null)
                    {
                        foreach (var atom in mol.Atoms)
                        {
                            var currentAtom = current.Atoms.Find(i => i.Symbol == atom.Symbol && CompareMoleculeAtomPos(i, atom));
                            if (currentAtom == null)
                            {
                                currentAtom = new MoleculeAtom();
                                currentAtom.Position = atom.Position;
                                currentAtom.AtomicWeight = atom.AtomicWeight;
                                currentAtom.Symbol = atom.Symbol;
                                currentAtom.Number = atom.Number;
                                currentAtom.PosX = atom.PosX;
                                currentAtom.PosY = atom.PosY;
                                currentAtom.PosZ = atom.PosZ;
                                currentAtom.GeoDiscCharge = atom.GeoDiscCharge;
                                currentAtom.ConnollyCharge = atom.ConnollyCharge;
                                currentAtom.CHelpGCharge = atom.CHelpGCharge;
                                current.Atoms.Add(currentAtom);
                            }
                            if (currentAtom != null)
                            {
                                currentAtom.LowdinPopulation = atom.LowdinPopulation;
                                currentAtom.LowdinPopulationAcid = atom.LowdinPopulationAcid;
                                currentAtom.LowdinPopulationBase = atom.LowdinPopulationBase;
                                currentAtom.MullikenPopulation = atom.MullikenPopulation;
                                currentAtom.MullikenPopulationAcid = atom.MullikenPopulationAcid;
                                currentAtom.MullikenPopulationBase = atom.MullikenPopulationBase;

                                // Copy Atom Orbitals
                                foreach (var orbital in atom.Orbitals)
                                {
                                    currentAtom.Orbitals.Add(new MoleculeAtomOrbital()
                                    {
                                        LowdinPopulation = orbital.LowdinPopulation,
                                        LowdinPopulationAcid = orbital.LowdinPopulationAcid,
                                        LowdinPopulationBase = orbital.LowdinPopulationBase,
                                        MullikenPopulation = orbital.MullikenPopulation,
                                        MullikenPopulationAcid = orbital.MullikenPopulationAcid,
                                        MullikenPopulationBase = orbital.MullikenPopulationBase,
                                        Position = orbital.Position,
                                        Symbol = orbital.Symbol
                                    });
                                }

                                // Copy Atom Bonds
                                foreach (var atombond in atom.Bonds)
                                {
                                    currentAtom.Bonds.Add(new MoleculeBond()
                                    {
                                        Atom1Position = atombond.Atom1Position,
                                        Atom2Position = atombond.Atom2Position,
                                        BondOrder = atombond.BondOrder,
                                        BondOrderAcid = atombond.BondOrderAcid,
                                        BondOrderBase = atombond.BondOrderBase,
                                        Distance = atombond.Distance,
                                        OverlapPopulation = atombond.OverlapPopulation,
                                        OverlapPopulationAcid = atombond.OverlapPopulationAcid,
                                        OverlapPopulationBase = atombond.OverlapPopulationBase
                                    });
                                }
                            }
                        }


                        foreach (var bond in mol.Bonds)
                        {
                            current.Bonds.Add(new MoleculeBond()
                            {
                                Atom1Position = bond.Atom1Position,
                                Atom2Position = bond.Atom2Position,
                                BondOrder = bond.BondOrder,
                                BondOrderAcid = bond.BondOrderAcid,
                                BondOrderBase = bond.BondOrderBase,
                                Distance = bond.Distance,
                                OverlapPopulation = bond.OverlapPopulation,
                                OverlapPopulationAcid = bond.OverlapPopulationAcid,
                                OverlapPopulationBase = bond.OverlapPopulationBase
                            });
                        }
                        current.ElectronAffinity = mol.ElectronAffinity;
                        current.Hardness = mol.Hardness;
                        current.HFEnergy = mol.HFEnergy;
                    }
                }
                current.ModelId = model.Id;
                current.ParentCalculationId = optcalc.Id;
                retval.Add(await this.MoleculeService.CreateAsync(current));
            }

            return retval;
        }

        public async Task UpdateAsync(int id, ModelInfo pa_modelInfo)
        {
            var model = await Repo.GetByIdAsync(id);
            if (model == null)
            {
                throw new NotExistsException($"The model {id} does not exist the model cannot be updated !");
            }

            model.Code = pa_modelInfo.Name;
            model.Description = pa_modelInfo.Description;
            model.Charge = pa_modelInfo.Charge;
            model.InitialGeometry = pa_modelInfo.InitialGeometry;
            model.CurrentGeometry = pa_modelInfo.CurrentGeometry;
            await Repo.SaveChangesAsync();
        }

        public async Task<ChemicalModel> Get(int id)
        {
            ChemicalModel retval = null;
            MoleculeModel result = await this.Repo.GetByIdAsync(id);
            if ( result != null)
            {
                retval = new ChemicalModel();
                retval.Id = result.Id;
                retval.Description = result.Description;
                retval.Name = result.Code;
                retval.Charge = result.Charge;
                retval.InitialGeometry = result.InitialGeometry;
                retval.CurrentGeometry = result.CurrentGeometry;
                retval.ProjectID = result.CategoryId;
                retval.Calculations = (from c in result.Calculation select ChemicalCalculationService.Convert(c)).ToList();
            }
            return retval;
        }

        public async Task<List<ChemicalModel>> GetModelForProject(int projectId)
        {
            return (from i in await this.Repo.GetByCategoryAsync(projectId)
                                          select new ChemicalModel()
                                          {
                                              Description = i.Description,
                                              Name = i.Code,
                                              Charge = i.Charge,
                                              ProjectID = i.CategoryId,
                                              InitialGeometry = i.InitialGeometry,
                                              CurrentGeometry = i.CurrentGeometry,
                                              Id = i.Id,
                                              Calculations = (from c in i.Calculation select ChemicalCalculationService.Convert(c) ).ToList()
                                          }).ToList();
        }


    }
}
