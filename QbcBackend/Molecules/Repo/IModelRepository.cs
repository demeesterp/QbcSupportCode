using QbcBackend.Molecules.Entities;
using QbcBackend.Tools.Base.Repo;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QbcBackend.Molecules.Repo
{
    public interface IModelRepository : IQbcBaseRepo
    {

        MoleculeModel Add(MoleculeModel entity);

        void Remove(int moleculeModelId);

        Task<ICollection<MoleculeModel>> GetByCategoryAsync(int categoryId);

        Task<MoleculeModel> GetByIdAsync(int id);

        Task<int> CountByCategoryAsync(int categoryId);


    }
}
