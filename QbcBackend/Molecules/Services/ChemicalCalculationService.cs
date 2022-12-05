using FluentValidation;
using QbcBackend.Molecules.Entities;
using QbcBackend.Molecules.Model.GmsInput;
using QbcBackend.Molecules.Model.MoleculeCalculation;
using QbcBackend.Molecules.Repo;
using QbcBackend.Tools.QbcException;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QbcBackend.Molecules.Services
{
    public class ChemicalCalculationService : IChemicalCalculationService
    {
        #region private datamembers

        private ICalculationRepository Repo { get; }

        private IModelRepository ModelRepo { get; }

        private IBasisSetRepository BasisSetRepo { get; }

        private ICalculationStatusService StatusService { get; }

        private IMoleculeService MoleculeService { get; }

        private IGmsInputService GmsInputService { get; }

        private IValidator<Calculation> Validator { get; }

        #endregion

        public ChemicalCalculationService(IValidator<Calculation> validator, 
                                                ICalculationRepository repo,
                                                    IModelRepository modelRepo,
                                                        IBasisSetRepository basisSetRepo,
                                                            ICalculationStatusService statusService,
                                                                IMoleculeService moleculeService,
                                                                    IGmsInputService gmsInputService)
        {
            this.Repo = repo;
            this.ModelRepo = modelRepo;
            this.BasisSetRepo = basisSetRepo;
            this.StatusService = statusService;
            this.GmsInputService = gmsInputService;
            this.MoleculeService = moleculeService;
            this.Validator = validator;
        }

        public async Task<ChemicalCalculation> CreateAsync(ChemicalCalculation calculation)
        {
            if (calculation == null) throw new ArgumentNullException("calculation", "Null input values are not accepted !");
            var model = await this.ModelRepo.GetByIdAsync(calculation.ModelID);
            if (model == null)
            {
                throw new NotExistsException($"The model {calculation.ModelID} does not exist the calculation cannot be added to the model !");
            }

            string geometryxyz = string.Empty;

            if (!String.IsNullOrWhiteSpace(model.CurrentGeometry))
            {
                geometryxyz = model.CurrentGeometry;
            }
            else
            {
                geometryxyz = model.InitialGeometry;
            }

            Calculation toCreate = null;
            if ( calculation.Type == CalculationType.CHelpGCharges)
            {

                toCreate = new Calculation()
                {
                    BasisSetId = calculation.BasisSet?.Id,
                    Code = calculation.Code,
                    Description = calculation.Description,
                    ExecutionStatus = (int)ExecutionStatus.Created,
                    GmsInput = calculation.GmsInput,
                    HasParent = false,
                    ModelId = calculation.ModelID,
                    Type = (int)calculation.Type
                };

                if (String.IsNullOrWhiteSpace(toCreate.GmsInput))
                {
                    toCreate.GmsInput = await this.GmsInputService.GenCHelpGChargeInput(geometryxyz, model.Charge, toCreate.BasisSetId.GetValueOrDefault());
                }
            }
            else if (calculation.Type == CalculationType.GeoDiskCharges)
            {

                toCreate = new Calculation()
                {
                    BasisSetId = calculation.BasisSet?.Id,
                    Code = calculation.Code,
                    Description = calculation.Description,
                    ExecutionStatus = (int)ExecutionStatus.Created,
                    GmsInput = calculation.GmsInput,
                    HasParent = false,
                    ModelId = calculation.ModelID,
                    Type = (int)calculation.Type
                };

                if (String.IsNullOrWhiteSpace(toCreate.GmsInput))
                {
                    toCreate.GmsInput = await this.GmsInputService.GenGeoDiskChargeInput(geometryxyz, model.Charge, toCreate.BasisSetId.GetValueOrDefault());
                }
            }
            else if ( calculation.Type == CalculationType.Optimization)
            {
                toCreate = new Calculation()
                {
                    BasisSetId = calculation.BasisSet?.Id,
                    Code = calculation.Code,
                    Description = calculation.Description,
                    ExecutionStatus = (int)ExecutionStatus.Created,
                    GmsInput = calculation.GmsInput,
                    HasParent = false,
                    ModelId = calculation.ModelID,
                    Type = (int)calculation.Type
                };

                if (String.IsNullOrWhiteSpace(toCreate.GmsInput))
                {
                    toCreate.GmsInput = await this.GmsInputService.GenOptInput(geometryxyz, model.Charge, toCreate.BasisSetId.GetValueOrDefault());
                }
            }
            else if (calculation.Type == CalculationType.Fukui)
            {
                toCreate = new Calculation()
                {
                    BasisSetId = calculation.BasisSet?.Id,
                    Code = calculation.Code,
                    Description = calculation.Description,
                    ExecutionStatus = (int)ExecutionStatus.Created,
                    GmsInput = calculation.GmsInput,
                    HasParent = false,
                    ModelId = calculation.ModelID,
                    Type = (int)calculation.Type
                };


                List<FukuiInputInfo> fukuiInput = await this.GmsInputService.GenFukuiInput(geometryxyz, model.Charge, toCreate.BasisSetId.GetValueOrDefault());
                StringBuilder buffer = new StringBuilder();
                foreach(var input in fukuiInput)
                {
                    buffer.Append($"({input.Type},{input.GmsInput});");
                }

                if (String.IsNullOrWhiteSpace(toCreate.GmsInput))
                {
                    toCreate.GmsInput = buffer.ToString();
                }
            }
            else
            {
                toCreate = new Calculation()
                {
                    BasisSetId = calculation.BasisSet?.Id,
                    Code = calculation.Code,
                    Description = calculation.Description,
                    ExecutionStatus = (int)ExecutionStatus.Created,
                    GmsInput = calculation.GmsInput,
                    HasParent = false,
                    ModelId = calculation.ModelID,
                    Type = (int)calculation.Type
                };
            }

            this.Validator.Validate(toCreate);

            this.Repo.Add(toCreate);
            await Repo.SaveChangesAsync();
            calculation.Id = toCreate.Id;
            return calculation;
        }

        public async Task UpdateAsync(int id, ChemicalCalculation calculation)
        {
            if (calculation == null) throw new ArgumentNullException("calculation", "Null input values are not accepted !");
            var resultCalcs = await this.Repo.GetByCodeAsync(calculation.Code);
            Calculation existingCalc = null;
            if (!resultCalcs.Any())
            {
                existingCalc = await this.Repo.GetByIdAsync(calculation.Id);
                if (resultCalcs == null)
                {
                    throw new NotExistsException($"The calculation {calculation.Id} does not exist the calculation cannot be updated !");
                }
            }
            else
            {
                existingCalc = resultCalcs.FirstOrDefault();
            }

            var basisSet = this.BasisSetRepo.GetByIdAsync((calculation.BasisSet?.Id).GetValueOrDefault());
            if (basisSet == null)
            {
                throw new NotExistsException($"The basisset {calculation.BasisSet?.Id} does not exist the calculation cannot be updated !");
            }

            var model = await this.ModelRepo.GetByIdAsync(calculation.ModelID);
            if (model == null)
            {
                throw new NotExistsException($"The model {calculation.ModelID} does not exist the calculation cannot be added to the model !");
            }

            var result = await this.StatusService.TransferCalculation((ExecutionStatus)existingCalc.ExecutionStatus, calculation);

            existingCalc.BasisSetId = calculation.BasisSet?.Id;
            existingCalc.Code = calculation.Code;
            existingCalc.Description = calculation.Description;

            existingCalc.ExecutionStatus = (int)result.StatusAfterTransfer;
            existingCalc.GmsInput = calculation.GmsInput;
            existingCalc.ModelId = calculation.ModelID;
            existingCalc.Type = (int)calculation.Type;

            this.Validator.Validate(existingCalc);


            await this.Repo.SaveChangesAsync();


            if (result.StatusAfterTransfer != ExecutionStatus.Error)
            {
                foreach (var molecule in result.Molecules)
                {
                    molecule.NameInfo = model.Code + " " + calculation.Type;
                    await this.MoleculeService.CreateAsync(molecule);                    
                }
            }
        }

        public async Task DeleteAsync(int id)
        {
            this.Repo.Remove(id);
            await Repo.SaveChangesAsync();
        }

        public async Task<ICollection<ChemicalCalculation>> GetByModel(int modelID)
        {
            List<ChemicalCalculation> retval = new List<ChemicalCalculation>();
            foreach (var calc in await this.Repo.GetByModelAsync(modelID))
            {
                retval.Add(Convert(calc));
            }
            return retval;
        }

        public async Task<ChemicalCalculation> Get(int ID)
        {
            ChemicalCalculation retval = null;
                var result = await this.Repo.GetByIdAsync(ID);
                if (result != null)
                {
                    retval = Convert(result);
                }
            return retval;
        }


        #region private helpers

        public  static ChemicalCalculation Convert(Calculation calc)
        {
            return new ChemicalCalculation()
            {
                BasisSet = new BasisSetInfo()
                {
                    Code = calc.BasisSet?.Code,
                    Name = calc.BasisSet?.Name,
                    GmsInput = calc.BasisSet?.GmsInput,
                    Id = (calc.BasisSet?.Id).GetValueOrDefault()
                },
                Code = calc.Code,
                Description = calc.Description,
                Id = calc.Id,
                GmsInput = calc.GmsInput,
                Name = calc.Code,
                ModelID = calc.ModelId,
                HasParent = calc.HasParent,
                Status = (ExecutionStatus)calc.ExecutionStatus,
                Type = (CalculationType)calc.Type
            };
        }

        #endregion


    }
}
