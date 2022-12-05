using Microsoft.EntityFrameworkCore;
using QbcBackend.Molecules.Entities;
using QbcBackend.Tools.Base.Repo;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QbcBackend.Molecules.Repo
{
    public class CalculationRepository: QbcBaseRepo, ICalculationRepository
    {
        #region private properties

        private QuantumbiochemistryContext DbContext { get; }

        #endregion


        public CalculationRepository(QuantumbiochemistryContext dbContext)
            :base(dbContext)
        {
            this.DbContext = dbContext;
        }

        public Calculation Add(Calculation entity)
        {
            DbContext.Calculation.Add(entity);
            return entity;
        }

        public void Remove(int calculationId)
        {
            var result = DbContext.Calculation.Find(calculationId);
            if ( result != null)
            {
                DbContext.Calculation.Remove(result);
            }
        }

        public async Task<ICollection<Calculation>> GetByCodeAsync(string code)
        {
            return await(from i in this.DbContext.Calculation.Include(c => c.Model).Include(c => c.BasisSet) where i.Code.Contains(code) select i).ToListAsync();
        }

        public async Task<int> CountByCodeAsync(string code)
        {
            return await(from i in this.DbContext.Calculation where i.Code.Contains(code) select i).CountAsync();
        }

        public async Task<ICollection<Calculation>> GetByModelAsync(int modelId)
        {
            return await(from i in this.DbContext.Calculation.Include(c => c.BasisSet) where i.ModelId == modelId select i).ToListAsync();
        }

        public async Task<int> CountByModelAsync(int modelId)
        {
            return await(from i in this.DbContext.Calculation where i.ModelId == modelId select i).CountAsync();
        }

        public async Task<Calculation> GetByIdAsync(int calculationId)
        {
            return await this.DbContext.Calculation.FindAsync(calculationId);
        }
    }
}
