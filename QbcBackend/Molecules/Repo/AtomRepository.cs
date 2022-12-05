using Microsoft.EntityFrameworkCore;
using QbcBackend.Molecules.Entities;
using QbcBackend.Tools.Base.Repo;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QbcBackend.Molecules.Repo
{
    public class AtomRepository: QbcBaseRepo, IAtomRepository
    {
        #region dbContext

        private QuantumbiochemistryContext DbContext { get; set; }

        #endregion


        public AtomRepository(QuantumbiochemistryContext dbContext)
            :base(dbContext)
        {
            this.DbContext = dbContext;
        }

        public async Task<ICollection<Atom>> GetAtomByMolecule(int moleculeID)
        {
            return await(from i in  this.DbContext.Atom.Include(a => a.AtomOrbital).Include(a => a.BondAtom).ThenInclude(b => b.Bond) where i.MoleculeId == moleculeID select i).ToListAsync();
        }

        public async Task<int> CountAtomByMolecule(int moleculeID)
        {
            return await(from i in this.DbContext.Atom where i.MoleculeId == moleculeID select i).CountAsync();
        }

        public Atom Add(Atom entity)
        {
            this.DbContext.Atom.Add(entity);
            return entity;
        }

        public void Remove(int atomID)
        {
            var result = this.DbContext.Atom.Find(atomID);
            if ( result != null)
            {
                this.DbContext.Atom.Remove(result);
            }
        }
    }
}
