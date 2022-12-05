using QbcBackend.Molecules.Entities;
using QbcBackend.Tools.Base.Repo;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QbcBackend.Molecules.Repo
{
    public interface ICategoryRepository : IQbcBaseRepo
    {
        Category Add(Category entity);

        void Remove(int categoryId);

        Task<ICollection<Category>> GetByName(string name);

        Task<ICollection<Category>> GetByType(int type);

        Task<ICollection<Category>> GetAllAsync();

        Task<Category> GetByIdAsync(int Id);
    }
}
