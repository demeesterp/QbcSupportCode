using QbcBackend.Molecules.Model.MoleculeCalculation;
using QbcBackend.Molecules.Repo;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QbcBackend.Molecules.Services
{
    public class ChemicalBasissetService : IChemicalBasissetService
    {

        #region private helpers

        private IBasisSetRepository Repo { get; }

        #endregion


        public ChemicalBasissetService(IBasisSetRepository repo)
        {
            this.Repo = repo;
        }

        public async Task<List<BasisSetInfo>> GetAllAsync()
        {
            return (from i in await this.Repo.GetAllAsync()
                        select new BasisSetInfo()
                        {
                            Name = i.Name,
                            Code = i.Code,
                            Id = i.Id
                        }).ToList();
        }
    }
}
