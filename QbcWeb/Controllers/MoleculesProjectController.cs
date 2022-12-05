using Microsoft.AspNetCore.Mvc;
using QbcBackend.Molecules.Services;
using QbcWeb.Models;
using System.Linq;
using System.Threading.Tasks;

namespace QbcWeb.Controllers
{
    public class MoleculesProjectController : Controller
    {
        #region private members

        private IChemicalProjectService ProjectService { get; }

        private IChemicalModelService ModelService { get; }

        #endregion


        public MoleculesProjectController(IChemicalProjectService projectService, IChemicalModelService modelService)
        {
            this.ProjectService = projectService;
            this.ModelService = modelService;
        }

        public async Task<IActionResult> Index(string name)
        {
            var result = await this.ProjectService.SearchChemicalProjectAsync(name);
            ChemicalProjectViewModel viewModel = new ChemicalProjectViewModel();
            if (result.Count > 0)
            {
                viewModel.Project = result.FirstOrDefault();
                ChemicalModelViewModel current = null;
                foreach (var m in await this.ModelService.GetModelForProject(result.FirstOrDefault().Id))
                {
                    current = new ChemicalModelViewModel()
                    {
                        Calculations = m.Calculations,
                        Charge = m.Charge,
                        Description = m.Description,
                        InitialGeometry = m.InitialGeometry,
                        Name = m.Name,
                        ProjectName = viewModel.Project.Name,
                        Id = m.Id
                    };
                    viewModel.Models.Add(current);
                }
            }
            return View("MoleculesProject", viewModel);
        }

        public async Task<IActionResult> DeleteModel(int id, string projectname)
        {
            await this.ModelService.DeleteAsync(id);
            return RedirectToAction("Index", new { Name = projectname });
        }
    }
}