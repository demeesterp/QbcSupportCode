using Microsoft.EntityFrameworkCore;
using QbcBackend.Molecules.Entities;
using QbcBackend.Tools.Base.Repo;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QbcBackend.Molecules.Repo
{
    public class AtomInfoRepository : QbcBaseRepo, IAtomInfoRepository
    {

        #region private properties

        private QuantumbiochemistryContext DbContext { get; }

        #endregion


        public AtomInfoRepository(QuantumbiochemistryContext dbContext)
            : base(dbContext)
        {
            DbContext = dbContext;
        }

        public AtomInfo Add(AtomInfo entity)
        {
            DbContext.AtomInfo.Add(entity);
            return entity;
        }

        public void Remove(int atomInfoId)
        {
            var result = DbContext.AtomInfo.Find(atomInfoId);
            if ( result != null)
            {
                DbContext.Remove(result);
            }
        }

        public async Task<ICollection<AtomInfo>> GetAllAsync()
        {
            return await(from i in DbContext.AtomInfo select i).ToListAsync();
        }

        public async Task<int> CountAllAsync()
        {
            return await(from i in DbContext.AtomInfo select i).CountAsync();
        }
    }
}
