using QbcBackend.Molecules.Entities;
using QbcBackend.Molecules.Repo;
using QbcBackend.Tools.Base.Repo;

namespace QbcBackend.Molecules.Repo
{
    public class MoleculePropertyRepository : QbcBaseRepo, IMoleculePropertyRepository
    {

        #region private properties

        private QuantumbiochemistryContext DbContext { get; }

        #endregion

        public MoleculePropertyRepository(QuantumbiochemistryContext dbContext)
            :base(dbContext)
        {
            DbContext = dbContext;
        }

        public MoleculeProperty Add(MoleculeProperty entity)
        {
            DbContext.MoleculeProperty.Add(entity);
            return entity;
        }

        public void Remove(int moleculePropertyId)
        {
            var result = DbContext.MoleculeProperty.Find(moleculePropertyId);
            if ( result != null)
            {
                DbContext.MoleculeProperty.Remove(result);
            }
        }
    }
}
