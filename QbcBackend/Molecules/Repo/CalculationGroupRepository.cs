using Microsoft.EntityFrameworkCore;
using QbcBackend.Molecules.Entities;
using QbcBackend.Tools.Base.Repo;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QbcBackend.Molecules.Repo
{
    public class CalculationGroupRepository : QbcBaseRepo, ICalculationGroupRepository
    {

        #region private properties

        private QuantumbiochemistryContext DbContext { get; }

        #endregion


        public CalculationGroupRepository(QuantumbiochemistryContext dbContext):
            base(dbContext)
        {
            this.DbContext = dbContext;
        }

        public CalculationGroup Add(CalculationGroup entity)
        {
            this.DbContext.CalculationGroup.Add(entity);
            return entity;
        }

        public void Remove(int calculationGroupId)
        {
            var result = this.DbContext.CalculationGroup.Find(calculationGroupId);
            if ( result != null)
            {
                this.DbContext.CalculationGroup.Remove(result);
            }
        }

        public async Task<ICollection<CalculationGroup>> GetByName(string name)
        {
            return await(from i in this.DbContext.CalculationGroup where i.Name.Contains(name) select i).ToListAsync();
        }

        public async Task<ICollection<CalculationGroup>> GetAllAsync()
        {
            return await this.DbContext.CalculationGroup.ToListAsync();
        }

        public async Task<ICollection<CalculationGroup>> GetByParentID(int parentID)
        {
            return await(from i in this.DbContext.CalculationGroup where i.ParentCalcId == parentID select i).ToListAsync();
        }

        public async Task<CalculationGroup> GetByIdAsync(int id)
        {
            return await DbContext.CalculationGroup.FindAsync(id);
        }
    }
}
