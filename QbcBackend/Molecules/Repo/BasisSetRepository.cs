using Microsoft.EntityFrameworkCore;
using QbcBackend.Molecules.Entities;
using QbcBackend.Tools.Base.Repo;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QbcBackend.Molecules.Repo
{
    public class BasisSetRepository: QbcBaseRepo, IBasisSetRepository
    {
        #region private properties

        private QuantumbiochemistryContext DbContext { get; }

        #endregion


        public BasisSetRepository(QuantumbiochemistryContext dbContext):base(dbContext)
        {
            this.DbContext = dbContext;
        }

        public BasisSet Add(BasisSet entity)
        {
            this.DbContext.BasisSet.Add(entity);
            return entity;
        }

        public void Remove(int basisSetID)
        {
            var result = this.DbContext.BasisSet.Find(basisSetID);
            if ( result != null)
            {
                this.DbContext.BasisSet.Remove(result);
            }
        }

        public async Task<ICollection<BasisSet>> GetAllAsync()
        {
            return await(from i in this.DbContext.BasisSet select i).ToListAsync();
        }

        public async Task<int> CountAllAsync()
        {
            return await(from i in this.DbContext.BasisSet select i).CountAsync();
        }

        public async Task<BasisSet> GetByIdAsync(int basisSetId)
        {
            return await this.DbContext.BasisSet.FindAsync(basisSetId);
        }
    }
}
