using Microsoft.EntityFrameworkCore;
using QbcBackend.Molecules.Entities;
using QbcBackend.Tools.Base.Repo;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QbcBackend.Molecules.Repo
{
    public class CategoryRepository : QbcBaseRepo, ICategoryRepository
    {

        #region private properties

        private QuantumbiochemistryContext DbContext { get; }

        #endregion


        public CategoryRepository(QuantumbiochemistryContext dbContext)
            :base(dbContext)
        {
            this.DbContext = dbContext;
        }

        public Category Add(Category entity)
        {
            this.DbContext.Category.Add(entity);
            return entity;
        }

        public void remove(int categoryId)
        {
            var result = this.DbContext.Category.Find(categoryId);
            if ( result != null)
            {
                this.DbContext.Category.Remove(result);
            }
        }

        public async Task<ICollection<Category>> GetByName(string name)
        {
            return await(from i in this.DbContext.Category where i.Code.Contains(name) select i).ToListAsync();
        }

        public async Task<ICollection<Category>> GetByType(int type)
        {
            return await(from i in this.DbContext.Category where i.Type == type select i).ToListAsync();
        }

        public async Task<ICollection<Category>> GetAllAsync()
        {
            return await this.DbContext.Category.ToListAsync();
        }

        public void Remove(int categoryId)
        {
            var result = DbContext.Category.Find(categoryId);
            if ( result != null)
            {
                DbContext.Category.Remove(result);
            }
        }

        public async Task<Category> GetByIdAsync(int Id)
        {
            return await DbContext.Category.FindAsync(Id);
        }
    }
}
