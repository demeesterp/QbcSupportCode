using Microsoft.AspNetCore.Mvc;
using QbcBackend.Molecules.Model.MoleculeCalculation;
using QbcBackend.Molecules.Services;
using QbcWeb.Models;
using System.Threading.Tasks;

namespace QbcWeb.Controllers
{
    public class MoleculesCalculationController : Controller
    {
        #region private helpers

        private IChemicalModelService ModelService
        {
            get;
            set;
        }

        private IChemicalCalculationService CalculationService
        {
            get;
            set;
        }


        private IChemicalBasissetService BasisSetService
        {
            get;
            set;
        }

        #endregion


        public MoleculesCalculationController(IChemicalModelService modelservice,
                                                IChemicalCalculationService calculationservice,
                                                IChemicalBasissetService basisSetService)
        {

            this.CalculationService = calculationservice;
            this.ModelService = modelservice;
            this.BasisSetService = basisSetService;
        }


        public async Task<IActionResult> CreateCalculationScreen(int Id)
        {
            ChemicalCalculationViewModel model = new ChemicalCalculationViewModel();
            var basisSets = await this.BasisSetService.GetAllAsync();
            foreach (var basisSet in basisSets)
            {
                model.BasisSets.Add(new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem()
                {
                    Value = basisSet.Id.ToString(),
                    Text = basisSet.Name
                });
            }

            model.ModelId = Id;
            var cmodel = await this.ModelService.Get(Id);
            model.ChemicalModelName = cmodel.Name;


            return View("MoleculesCalculation", model);
        }

        public async Task<IActionResult> UpdateCalculationScreen(int Id)
        {
            ChemicalCalculationViewModel model = new ChemicalCalculationViewModel();
            var basisSets = await this.BasisSetService.GetAllAsync();
            foreach (var basisSet in basisSets)
            {
                model.BasisSets.Add(new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem()
                {
                    Value = basisSet.Id.ToString(),
                    Text = basisSet.Name
                });
            }

            var calc = await this.CalculationService.Get(Id);
            model.Name = calc.Name;
            model.Description = calc.Description;
            model.CalculationType = calc.Type;
            model.ModelId = calc.ModelID;
            model.SelectedBasisSet = calc.BasisSet.Id;
            model.GamessInput = calc.GmsInput;
            model.ExecutionStatus = calc.Status;
            model.Id = calc.Id;
            var cmodel = await this.ModelService.Get(calc.ModelID);
            model.ChemicalModelName = cmodel.Name;

            return View("MoleculesCalculation", model);
        }

        public async Task<IActionResult> SaveCalculation(ChemicalCalculationViewModel input)
        {
            ChemicalCalculation calc = new ChemicalCalculation();
            calc.Code = input.Name;
            calc.ModelID = input.ModelId;
            calc.Description = input.Description;
            calc.GmsInput = input.GamessInput;
            calc.Type = input.CalculationType;
            calc.Status = input.ExecutionStatus;
            var basisSets = await this.BasisSetService.GetAllAsync();
            calc.BasisSet = basisSets.Find((i) => i.Id == input.SelectedBasisSet);
            if (input.Id == 0)
            {
                var result = await this.CalculationService.CreateAsync(calc);
            }
            else
            {
                calc.Id = input.Id;
                await this.CalculationService.UpdateAsync(input.Id, calc);
            }
            return RedirectToAction("Index", "MoleculesModel", new { Id = input.ModelId });
        }


        public async Task<IActionResult> DeleteCalculation(int Id)
        {
            var currentCalc = await this.CalculationService.Get(Id);
            await this.CalculationService.DeleteAsync(Id);
            return RedirectToAction("Index", "MoleculesModel", new { Id = currentCalc.ModelID });
        }
    }
}