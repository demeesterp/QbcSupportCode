using Microsoft.EntityFrameworkCore;
using QbcBackend.Molecules.Entities;
using QbcBackend.Tools.Base.Repo;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QbcBackend.Molecules.Repo
{
    public class MoleculeElpotRepository : QbcBaseRepo, IMoleculeElpotRepository
    {
        #region private properties

        private QuantumbiochemistryContext DbContext { get; }

        #endregion

        public MoleculeElpotRepository(QuantumbiochemistryContext dbContext)
            :base(dbContext)
        {
            DbContext = dbContext;
        }

        public MoleculeElPot Add(MoleculeElPot entity)
        {
            DbContext.MoleculeElPot.Add(entity);
            return entity;
        }

        public async Task<int> CountByMoleculeAsync(int moleculeId)
        {
            return await (from i in DbContext.MoleculeElPot where i.MoleculeId == moleculeId select i).CountAsync();
        }

        public async Task<MoleculeElPot> GetByIdAsync(int id)
        {
            return await (from i in DbContext.MoleculeElPot where i.Id == id select i).FirstOrDefaultAsync();
        }

        public async Task<ICollection<MoleculeElPot>> GetByMoleculeAsync(int moleculeId)
        {
            return await (from i in DbContext.MoleculeElPot where i.MoleculeId == moleculeId select i).ToListAsync();
        }

        public void Remove(int moleculeElpotId)
        {
            var result = DbContext.MoleculeElPot.Find(moleculeElpotId);
            if (result != null)
            {
                DbContext.Remove(result);
            }
        }
    }
}
