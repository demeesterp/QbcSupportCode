using QbcBackend.Molecules.Entities;
using QbcBackend.Tools.Base.Repo;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QbcBackend.Molecules.Repo
{
    public interface IConfigurationDataRepository : IQbcBaseRepo
    {
        ConfigurationData Add(ConfigurationData entity);

        void Remove(int configDataId);

        Task<ICollection<ConfigurationData>> GetAllAsync();

        Task<int> CountAllAsync();
    }
}
