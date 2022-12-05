using QbcBackend.Molecules.Model.Molecule;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QbcBackend.Molecules.Services
{
    public interface IMoleculeService
    {
        Task<MoleculeInfo> CreateAsync(MoleculeInfo molecule);

        Task UpdateAsync(int id, MoleculeInfo molecule);

        Task DeleteAsync(int id);

        Task<ICollection<MoleculeInfo>> GetByModelID(int modelid);

        Task<ICollection<MoleculeInfo>> GetByCalculationID(int calculationid);

        Task<MoleculeInfo> GetById(int id);

    }
}
