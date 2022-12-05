using QbcBackend.Molecules.Model.Molecule;
using QbcBackend.Molecules.Model.MoleculeModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QbcBackend.Molecules.Services
{
    public interface IChemicalModelService
    {

        Task<List<ChemicalModel>> GetModelForProject(int projectId);

        Task<ChemicalModel> Get(int id);

        Task<ModelInfo> CreateAsync(ModelInfo project);

        Task UpdateAsync(int id, ModelInfo prject);

        Task DeleteAsync(int id);

        Task AddBotanicalNameAsync(int modelId, string botanicalName);

        Task<List<MoleculeInfo>> MergeMolecules(int modelid);

    }
}
