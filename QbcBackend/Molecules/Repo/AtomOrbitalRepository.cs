using QbcBackend.Molecules.Entities;
using QbcBackend.Molecules.Repo;
using QbcBackend.Tools.Base.Repo;

namespace QbcBackend.Molecules.Repo
{
    public class AtomOrbitalRepository : QbcBaseRepo, IAtomOrbitalRepository
    {
        #region private properties

        private QuantumbiochemistryContext DbContext { get; }

        #endregion


        public AtomOrbitalRepository(QuantumbiochemistryContext dbContext)
            :base(dbContext)
        {
            this.DbContext = dbContext;
        }

        public AtomOrbital Add(AtomOrbital entity)
        {
            this.DbContext.AtomOrbital.Add(entity);
            return entity;
        }

        public void Remove(int atomOrbitalId)
        {
            var result = this.DbContext.AtomOrbital.Find(atomOrbitalId);
            if ( result != null)
            {
                this.DbContext.AtomOrbital.Remove(result);
            }
        }

        public AtomOrbital Get(int atomOrbitalId)
        {
            return this.DbContext.AtomOrbital.Find(atomOrbitalId);
        }
    }
}
