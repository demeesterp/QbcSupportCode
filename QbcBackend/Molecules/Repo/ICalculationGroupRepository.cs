using QbcBackend.Molecules.Entities;
using QbcBackend.Tools.Base.Repo;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QbcBackend.Molecules.Repo
{
    public interface ICalculationGroupRepository : IQbcBaseRepo
    {
        CalculationGroup Add(CalculationGroup entity);

        void Remove(int calculationGroupId);

        Task<ICollection<CalculationGroup>> GetByName(string name);

        Task<ICollection<CalculationGroup>> GetAllAsync();

        Task<ICollection<CalculationGroup>> GetByParentID(int parentID);

        Task<CalculationGroup> GetByIdAsync(int id);
    }
}
