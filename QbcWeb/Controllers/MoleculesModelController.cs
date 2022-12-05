using Microsoft.AspNetCore.Mvc;
using QbcBackend.Molecules.Model.Molecule;
using QbcBackend.Molecules.Model.MoleculeModel;
using QbcBackend.Molecules.Services;
using QbcWeb.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QbcWeb.Controllers
{
    public class MoleculesModelController : Controller
    {
        #region private members

        public IChemicalModelService ModelService
        {
            get;
            set;
        }

        public IChemicalProjectService ProjectService
        {
            get;
            set;
        }


        public IMoleculeService MoleculeService
        {
            get;
            set;
        }

        #endregion

        public MoleculesModelController(IChemicalModelService modelService, IChemicalProjectService projectService, IMoleculeService moleculeService)
        {
            this.ModelService = modelService;
            this.ProjectService = projectService;
            this.MoleculeService = moleculeService;
        }


        public async Task<IActionResult> Index(int id)
        {
            ChemicalModelViewModel retval = new ChemicalModelViewModel();
            var result = await this.ModelService.Get(id);

            var relatedProject = await this.ProjectService.GetChemicalProjectAsync(result.ProjectID);
            retval.ProjectName = relatedProject.Name;
            retval.Charge = result.Charge;
            retval.Id = result.Id;
            retval.Description = result.Description;
            retval.InitialGeometry = result.InitialGeometry;
            retval.CurrentGeometry = result.CurrentGeometry;

            retval.Name = result.Name;
            retval.Calculations = (from i in result.Calculations where i.HasParent == false select i).ToList();
            retval.Molecules = new List<MoleculeInfo>();
            retval.Molecules.AddRange(await this.MoleculeService.GetByModelID(id));
            return View("MoleculesModel", retval);
        }

        [HttpPost]
        public async Task<IActionResult> NewModel(ChemicalModel model)
        {
            var result = await this.ModelService.CreateAsync(model);

            return RedirectToAction("Index", new { Id = result.Id });
        }

        [HttpPost]
        public async Task<IActionResult> UpdateModel(ChemicalModel model)
        {
            await this.ModelService.UpdateAsync(model.Id, model);

            return RedirectToAction("Index", new { Id = model.Id });
        }

        public async Task<IActionResult> MergeMolecules(int id)
        {
            await this.ModelService.MergeMolecules(id);
            return RedirectToAction("Index", new { Id = id });
        }
    }
}