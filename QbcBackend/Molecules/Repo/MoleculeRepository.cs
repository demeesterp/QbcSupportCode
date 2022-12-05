using Microsoft.EntityFrameworkCore;
using QbcBackend.Molecules.Entities;
using QbcBackend.Tools.Base.Repo;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QbcBackend.Molecules.Repo
{
    public class MoleculeRepository : QbcBaseRepo, IMoleculeRepository
    {
        #region private properties

        private QuantumbiochemistryContext DbContext { get; }

        #endregion


        public MoleculeRepository(QuantumbiochemistryContext dbContext)
            :base(dbContext)
        {
            this.DbContext = dbContext;
        }

        public Molecule Add(Molecule entity)
        {
            DbContext.Molecule.Add(entity);
            return entity;
        }

        public void Remove(int moleculeId)
        {
            var result = this.DbContext.Molecule.Find(moleculeId);
            if ( result != null)
            {
                DbContext.Molecule.Remove(result);
            }
        }

        public async Task<ICollection<Molecule>> GetByModelAsync(int modelId)
        {
            return await(from i in this.DbContext.Molecule
                         where i.ModelId == modelId
                         select i).ToListAsync();
        }

        public async Task<ICollection<Molecule>> GetByParentCalculation(int calculationID)
        {
            return await(from i in this.DbContext.Molecule.Include(a => a.MoleculeProperty)
                                                  .Include(a => a.ParentCalculation)
                         where i.ParentCalculationId == calculationID
                         select i).ToListAsync();
        }

        public async Task<Molecule> GetByIdAsync(int moleculeid)
        {
            return await(from i in this.DbContext.Molecule.Include(a => a.MoleculeProperty)
                                                    .Include(a => a.ParentCalculation)
                          where i.Id == moleculeid
                          select i).FirstOrDefaultAsync();
        }
    }
}
