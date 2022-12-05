using QbcBackend.Molecules.Entities;
using QbcBackend.Tools.Base.Repo;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QbcBackend.Molecules.Repo
{
    public interface IMoleculeRepository : IQbcBaseRepo
    {
        Molecule Add(Molecule entity);

        void Remove(int moleculeId);

        Task<ICollection<Molecule>> GetByModelAsync(int modelId);

        Task<ICollection<Molecule>> GetByParentCalculation(int calculationID);

        Task<Molecule> GetByIdAsync(int moleculeid);
    }
}
