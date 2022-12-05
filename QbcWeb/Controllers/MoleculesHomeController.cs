using Microsoft.AspNetCore.Mvc;
using QbcBackend.Molecules.Model.MoleculeProject;
using QbcBackend.Molecules.Services;
using QbcWeb.Models;
using System.Linq;
using System.Threading.Tasks;

namespace QbcWeb.Controllers
{
    public class MoleculesHomeController : Controller
    {
        #region private members

        private IChemicalProjectService ProjectService { get; }

        private IChemicalModelService ModelService { get; }

        #endregion


        public MoleculesHomeController(IChemicalProjectService projectService, IChemicalModelService modelService)
        {
            this.ProjectService = projectService;
            this.ModelService = modelService;
        }



        public async Task<IActionResult> Index()
        {
            ProjectListViewModel model = new ProjectListViewModel();
            ChemicalProjectViewModel projectModel = null;
            foreach (var currentProj in await this.ProjectService.GetAllChemicalProjectAsync())
            {
                projectModel = new ChemicalProjectViewModel()
                {
                    Project = currentProj
                };

                foreach (var m in await this.ModelService.GetModelForProject(currentProj.Id))
                {
                    projectModel.Models.Add(new ChemicalModelViewModel()
                    {
                        Name = m.Name,
                        Description = m.Description,
                        Id = m.Id
                    });
                }

                model.ProjectList.Add(projectModel);
            }
            return View("MoleculesHome", model);
        }



        [HttpPost]
        public async Task<IActionResult> NewProject(ChemicalProject project)
        {
            await this.ProjectService.CreateAsync(project);

            return this.RedirectToAction("Index", "MoleculesProject", new { name = project.Name });
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProject(ChemicalProject project)
        {
            await this.ProjectService.UpdateAsync(project.Id, project);

            return this.RedirectToAction("Index", "MoleculesHome");
        }


        public async Task<IActionResult> DeleteProject(string name)
        {
            var result = await this.ProjectService.SearchChemicalProjectAsync(name);
            foreach (var proj in result)
            {
                await this.ProjectService.DeleteAsync(proj.Id);
            }
            return this.RedirectToAction("Index");
        }


        public async Task<IActionResult> ProjectModels(string name)
        {
            var result = await this.ProjectService.SearchChemicalProjectAsync(name);
            if (result.Count > 0)
            {
                return this.RedirectToAction("Index", "MoleculesProject", new { name = result.FirstOrDefault().Name });
            }
            else
            {
                return this.RedirectToAction("Index");
            }
        }

    }
}