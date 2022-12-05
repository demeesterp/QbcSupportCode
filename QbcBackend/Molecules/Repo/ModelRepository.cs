using Microsoft.EntityFrameworkCore;
using QbcBackend.Molecules.Entities;
using QbcBackend.Tools.Base.Repo;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QbcBackend.Molecules.Repo
{
    public class ModelRepository: QbcBaseRepo, IModelRepository
    {

        #region private properties

        private QuantumbiochemistryContext DbContext { get; }

        #endregion

        public ModelRepository(QuantumbiochemistryContext dbContext)
            :base(dbContext)
        {
            this.DbContext = dbContext;
        }

        public MoleculeModel Add(MoleculeModel entity)
        {
            this.DbContext.MoleculeModel.Add(entity);
            return entity;
        }

        public void Remove(int moleculeModelId)
        {
            var result = this.DbContext.MoleculeModel.Find(moleculeModelId);
            if ( result != null)
            {
                this.DbContext.MoleculeModel.Remove(result);
            }
        }

        public async Task<ICollection<MoleculeModel>> GetByCategoryAsync(int categoryId)
        {
            return await(from i in this.DbContext.MoleculeModel.Include(m => m.Category).Include(m => m.Calculation).ThenInclude(b => b.BasisSet) where i.CategoryId == categoryId select i).ToListAsync();
        }

        public async Task<MoleculeModel> GetByIdAsync(int id)
        {
            return await(from i in this.DbContext.MoleculeModel.Include(m => m.Category).Include(p => p.Molecule).Include(m => m.Calculation).ThenInclude(b => b.BasisSet) where i.Id == id select i).FirstOrDefaultAsync();
        }

        public async Task<int> CountByCategoryAsync(int categoryId)
        {
            return await(from i in this.DbContext.MoleculeModel where i.CategoryId == categoryId select i).CountAsync();
        }
    }
}
