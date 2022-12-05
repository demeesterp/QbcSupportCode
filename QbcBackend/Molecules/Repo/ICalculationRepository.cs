using QbcBackend.Molecules.Entities;
using QbcBackend.Tools.Base.Repo;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QbcBackend.Molecules.Repo
{
    public interface ICalculationRepository : IQbcBaseRepo
    {

        Calculation Add(Calculation entity);

        void Remove(int calculationId);

        Task<ICollection<Calculation>> GetByCodeAsync(string code);

        Task<int> CountByCodeAsync(string code);

        Task<ICollection<Calculation>> GetByModelAsync(int modelId);

        Task<int> CountByModelAsync(int modelId);

        Task<Calculation> GetByIdAsync(int calculationId);

    }
}
