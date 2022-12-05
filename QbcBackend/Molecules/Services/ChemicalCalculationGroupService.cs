using FluentValidation;
using QbcBackend.Molecules.Entities;
using QbcBackend.Molecules.Model.MoleculeCalculation;
using QbcBackend.Molecules.Repo;
using QbcBackend.Tools.QbcException;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QbcBackend.Molecules.Services
{
    public class ChemicalCalculationGroupService :IChemicalCalculationGroupService
    {

        #region private members

        private IChemicalCalculationService CalcSvc { get; }

        private ICalculationGroupRepository Repo { get; }

        private IValidator<Calculation> Validator { get; }

        #endregion

        public ChemicalCalculationGroupService(IValidator<Calculation> validator, 
                                                IChemicalCalculationService calculationSvc,
                                                    ICalculationGroupRepository repo)
        {
            this.CalcSvc = calculationSvc;
            this.Repo = repo;
            this.Validator = validator;
        }

        public async Task<ChemicalCalculationGroup> CreateAsync(ChemicalCalculationGroup calculationGroup)
        {
            if ( calculationGroup == null) throw new ArgumentNullException("calculationgroup", "Null input values are not accepted !");
            if (calculationGroup.ParentCalculationID.HasValue)
            {
                var result = await CalcSvc.Get(calculationGroup.ParentCalculationID.Value);
                if (result == null)
                {
                    throw new NotExistsException($"The parent calculation {calculationGroup.ParentCalculationID.Value} does not exist the calculationgroup cannot be created!");
                }
            }

            if (calculationGroup.Calculation == null)
            {
                throw new ArgumentNullException("calculationgroup", "Null input values are not accepted for a calculation in a calculationgroup!");
            }

            var createdCalculation = await CalcSvc.CreateAsync(calculationGroup.Calculation);

            CalculationGroup toCreate = new CalculationGroup();
            toCreate.CalcId = createdCalculation.Id;
            toCreate.Name = calculationGroup.Name;
            toCreate.ParentCalcId = calculationGroup.ParentCalculationID;


            this.Validator.Validate(toCreate);


            this.Repo.Add(toCreate);

            await Repo.SaveChangesAsync();

            calculationGroup.Id = toCreate.Id;

            return calculationGroup;          
        }

        public async Task DeleteAsync(int id)
        {
            Repo.Remove(id);
            await Repo.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, ChemicalCalculationGroup calculationGroup)
        {
            var result = await this.Repo.GetByIdAsync(id);
            if (result == null)
            {
                throw new NotExistsException($"The calculationgroup with {id} does not exist the calculationgroup cannot be updated!");
            }

            if (calculationGroup.Calculation == null || calculationGroup.Calculation.Id == 0)
            {
                throw new ArgumentNullException("calculationgroup", "Null input values are not accepted for a calculation in a calculationgroup!");
            }

            result.Name = calculationGroup.Name;
            result.ParentCalcId = calculationGroup.ParentCalculationID;
            result.CalcId = calculationGroup.Calculation.Id;
            await Repo.SaveChangesAsync();
        }

        public async Task<List<ChemicalCalculationGroup>> GetByParentAsync(int parentID)
        {
            List<ChemicalCalculationGroup> retval = new List<ChemicalCalculationGroup>();
            foreach ( var item in  await this.Repo.GetByParentID(parentID))
            {
                retval.Add(new ChemicalCalculationGroup()
                {
                    Id = item.Id,
                    Name = item.Name,
                    Calculation = ChemicalCalculationService.Convert(item.Calc),
                    ParentCalculationID = item.ParentCalcId
                });
            }
            return retval;
        }


    }
}
