using Microsoft.AspNetCore.Mvc;
using QbcBackend.Molecules.Services;
using QbcBackend.Tools.Serialisation;
using QbcWeb.Models;
using QbcWebApp.Models;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QbcWeb.Controllers
{
    public class MoleculesMoleculeController : Controller
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

        public MoleculesMoleculeController(IChemicalModelService modelService, IChemicalProjectService projectService, IMoleculeService moleculeService)
        {
            this.ModelService = modelService;
            this.ProjectService = projectService;
            this.MoleculeService = moleculeService;
        }

        public async Task<IActionResult> Index(int Id)
        {
            MoleculeViewModel model = new MoleculeViewModel();

            var result = await this.MoleculeService.GetById(Id);

            var chemicalModel = await this.ModelService.Get(result.ModelId);

            model.Name = result.NameInfo;
            model.Description = result.Description;
            model.Comment = result.Comment;
            model.Id = result.Id;
            model.ModelId = result.ModelId;
            model.ModelName = chemicalModel.Name;

            model.Hardness = result.Hardness;
            model.HfEnergy = result.HFEnergy;
            model.Multiplicity = result.Multiplicity;
            model.NAlphaOrbitals = result.NAlphaOrbitals;
            model.NBetaOrbitals = result.NBetaOrbitals;
            model.ElectronAffinity = result.ElectronAffinity;
            model.DftEnergy = result.DftEnergy;

            StringBuilder currentGeometry = new StringBuilder();
            currentGeometry.AppendLine(result.Atoms.Count.ToString());
            currentGeometry.AppendLine(" ");
            foreach (var atom in (from i in result.Atoms orderby i.Number ascending select i))
            {
                currentGeometry.AppendLine($"{atom.Symbol} {atom.PosX:0.00000} {atom.PosY:0.00000} {atom.PosZ:0.00000}".Replace(",","."));
            }

            model.Geometry = currentGeometry.ToString();

            model.Atoms.AddRange(result.Atoms.ConvertAll(i => new MoleculeAtomViewModel()
            {
                AtomicWeight = i.AtomicWeight,
                CHelpGCharge = i.CHelpGCharge,
                GeoDiscCharge = i.GeoDiscCharge,
                LowdinCharge = i.AtomicWeight - i.LowdinPopulation,
                LowdinPopulationAcid = i.LowdinPopulationAcid,
                LowdinPopulationBase = i.LowdinPopulationBase,
                MullikenCharge = i.AtomicWeight - i.MullikenPopulation,
                MullikenPopulationAcid = i.MullikenPopulationAcid,
                MullikenPopulationBase = i.MullikenPopulationBase,
                Number = i.Number,
                Position = i.Position,
                Symbol = i.Symbol,
                AtomOrbitals = (from o in i.Orbitals orderby o.Position ascending select new MoleculeAtomOrbitalViewModel()
                {
                    Symbol = o.Symbol,
                    LowdinPopulation = o.LowdinPopulation,
                    LowdinPopulationAcid = o.LowdinPopulationAcid,
                    LowdinPopulationBase = o.LowdinPopulationBase,
                    MullikenPopulation = o.MullikenPopulation,
                    MullikenPopulationAcid = o.MullikenPopulationAcid,
                    MullikenPopulationBase = o.MullikenPopulationBase,
                    Position = o.Position
                }).ToList()
            }));


            foreach (var bond in result.Bonds)
            {
                if (bond.OverlapPopulation > 0.1m || bond.OverlapPopulationAcid > 0.1m || bond.OverlapPopulationBase > 0.1m)
                {
                    var atom1 = result.Atoms.Find(i => i.Position == bond.Atom1Position);
                    var atom2 = result.Atoms.Find(i => i.Position == bond.Atom2Position);
                    model.Bonds.Add(new MoleculeBondViewModel()
                    {
                        BondLabel = atom1.Symbol + atom1.Position.ToString() + "-" + atom2.Symbol + atom2.Position.ToString(),
                        BondOrder = bond.BondOrder,
                        BondOrderAcid = bond.BondOrderAcid,
                        BondOrderBase = bond.BondOrderBase,
                        Distance = bond.Distance,
                        OverlapPopulation = bond.OverlapPopulation,
                        OverlapPopulationAcid = bond.OverlapPopulationAcid,
                        OverlapPopulationBase = bond.OverlapPopulationBase
                    });
                }
            }


            return View("MoleculesMolecule", model);
        }

        public async Task<IActionResult> DeleteMolecule(int Id, int ModelID)
        {
            await this.MoleculeService.DeleteAsync(Id);
            return RedirectToAction("Index", "MoleculesModel", new { Id = ModelID });
        }

        public async Task<IActionResult> DownloadMolecule(int Id)
        {
           var result = await this.MoleculeService.GetById(Id);
           StringBuilder str = new StringBuilder(new QbcJSONFormatter().SerializeObjectToString(result));
           var stream = new MemoryStream();
           var writer = new StreamWriter(stream);
           writer.Write(str);
           writer.Flush();
           stream.Position = 0;
           return File(stream, "application/json");
        }


        public async Task<IActionResult> UpdateMolecule(MoleculeUpdateViewModel toUpdate)
        {
            var result = await this.MoleculeService.GetById(toUpdate.ID);
            result.NameInfo = toUpdate.Name;
            result.Description = toUpdate.Description;
            result.Comment = toUpdate.Comment;
            await this.MoleculeService.UpdateAsync(result.Id, result);
            return RedirectToAction("Index", "MoleculesMolecule", new { Id = toUpdate.ID });
        }
    }
}