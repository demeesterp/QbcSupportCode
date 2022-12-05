using QbcBackend.Molecules.Entities;
using QbcBackend.Molecules.Repo;
using QbcBackend.Tools.Base.Repo;

namespace QbcBackend.Molecules.Repo
{
    public class BondAtomRepository: QbcBaseRepo, IBondAtomRepository
    {

        #region private properties

        private QuantumbiochemistryContext DbContext { get; }

        #endregion


        public BondAtomRepository(QuantumbiochemistryContext dbContext):
            base(dbContext)
        {
            this.DbContext = dbContext;
        }

        public BondAtom Add(BondAtom entity)
        {
            this.DbContext.BondAtom.Add(entity);
            return entity;
        }

        public void Remove(int bondAtomId)
        {
            var result = this.DbContext.BondAtom.Find(bondAtomId);
            if ( result != null)
            {
                this.DbContext.BondAtom.Remove(result);
            }
        }
    }
}
