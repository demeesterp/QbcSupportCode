using QbcBackend.Molecules.Entities;
using QbcBackend.Tools.Base.Repo;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace QbcBackend.Molecules.Repo
{
    public class BondRepository: QbcBaseRepo , IBondRepository
    {
        #region private properties

        private QuantumbiochemistryContext DbContext { get; }

        #endregion

        public BondRepository(QuantumbiochemistryContext dbContext)
            :base(dbContext)
        {
            this.DbContext = dbContext;
        }

        public Bond Add(Bond entity)
        {
            this.DbContext.Add(entity);
            return entity;
        }


        public async Task<List<Bond>> GetBondForMolecule(int moleculeId)
        {
            return await (from i in this.DbContext.Bond where i.MoleculeId == moleculeId select i).ToListAsync();
        }

        public void Remove(int bondId)
        {
            var result = this.DbContext.Bond.Find(bondId);
            if ( result != null)
            {
                this.DbContext.Bond.Remove(result);
            }
        }
    }
}
