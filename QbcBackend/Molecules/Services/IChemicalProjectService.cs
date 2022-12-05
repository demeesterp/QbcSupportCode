using QbcBackend.Molecules.Model.MoleculeProject;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QbcBackend.Molecules.Services
{
    public interface IChemicalProjectService
    {
        Task<List<ChemicalProject>> SearchChemicalProjectAsync(string name);

        Task<ChemicalProject> GetChemicalProjectAsync(int id);

        Task<List<ChemicalProject>> GetAllChemicalProjectAsync();

        Task<ChemicalProject> CreateAsync(ChemicalProject project);

        Task UpdateAsync(int id, ChemicalProject prject);

        Task DeleteAsync(int id);

    }
}
