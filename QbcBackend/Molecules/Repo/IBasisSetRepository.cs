using QbcBackend.Molecules.Entities;
using QbcBackend.Tools.Base.Repo;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QbcBackend.Molecules.Repo
{
    public interface IBasisSetRepository : IQbcBaseRepo
    {

        BasisSet Add(BasisSet entity);

        void Remove(int basisSetID);

        Task<ICollection<BasisSet>> GetAllAsync();

        Task<int> CountAllAsync();

        Task<BasisSet> GetByIdAsync(int basisSetId);

    }
}
